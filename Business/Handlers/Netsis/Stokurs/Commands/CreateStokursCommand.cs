using Business.Constants;
using Business.Handlers.Netsis.Seritra.Commands;
using Business.Handlers.Netsis.Sthar.Commands;
using Business.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Netsis;
using Entities.Dtos.Production;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Netsis.Stokurs.Commands
{
    public class CreateStokursCommand : IRequest<IDataResult<string>>
    {
        public string MachineCode { get; set; }
        public string WorkOrder { get; set; }
        public string StockCode { get; set; }
        public decimal Miktar { get; set; }
        public string Yapkod { get; set; }
        public int UretId { get; set; }
    }
    public class CreateStokursCommandHandler : IRequestHandler<CreateStokursCommand, IDataResult<string>>
    {
        ISeritraRepository _seritraRepository;
        IStokUrsRepository _stokUrsRepository;
        IMediator _mediator;
        public CreateStokursCommandHandler(IStokUrsRepository stokUrsRepository, IMediator mediator, ISeritraRepository seritraRepository)
        {
            _stokUrsRepository = stokUrsRepository;
            _mediator = mediator;
            _seritraRepository = seritraRepository;
        }

        public async Task<IDataResult<string>> Handle(CreateStokursCommand request, CancellationToken cancellationToken)
        {



            string messages = null!;

            //var getcurrentSerialNo = await _stokUrsRepository.GetLastFisNoByMachineCode(request.MachineCode);
            var getcurrentSerialNo = await _seritraRepository.GetLastFisNoByMachineCode(request.MachineCode);
            var getReceiptNo = GenerateSerialNo.GenerateStokUrsSerialNo(getcurrentSerialNo);
            var record = new Tblstokur
            {
                UretsonFisno = getReceiptNo,
                UretsonTarih = DateTime.Now,
                UretsonSipno = request.WorkOrder,
                UretsonDepo = 599,
                UretsonMamul = request.StockCode,
                UretsonMiktar = request.Miktar,
                SubeKodu = 0,
                IYedek1 = 599,
                ReceteTarihi = DateTime.Now,
                Oncelik = 0,
                Kayityapankul = "NETSIS",
                Kayittarihi = DateTime.Now,
                Yapkod = request.Yapkod,
                KayitEdilsin = 1,
                MaliyetCarpilsin = 0,
                MamulOlcuBirimi = 0,
                BakiyeDepo = 0,
                OtoYmamStokKullan = 0,
                Eksibakiye = "H",
                Mamulparcala = "H",
                BelgeTipi = "D",
                Firedepo = 0
            };

            _stokUrsRepository.Add(record);
            await _stokUrsRepository.SaveChangesAsync();

            messages += Messages.StokursAdded + " - ";

            var stharRecord = await _mediator.Send(new CreateStharCommand
            { UretId = request.UretId, UretsonFisNo = getReceiptNo });

            if (!stharRecord.Success)
                return new ErrorDataResult<string>(Messages.StharError + " - " + stharRecord.Message);
            messages += stharRecord.Message + " - ";


            //seritra kaydı
            var seritraRecord = await _mediator.Send(new CreateSeritraCommand { UretId = request.UretId, UretsonFisNo = getReceiptNo });
            if (!seritraRecord.Success)
                return new ErrorDataResult<string>(Messages.SeritraError);
            messages += seritraRecord.Message;

            return new SuccessDataResult<string>(getReceiptNo, messages);
        }
    }
}
