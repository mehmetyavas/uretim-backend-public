using Business.Constants;
using Business.Handlers.InjectionRawMaterial.Queries;
using Business.Handlers.Netsis.Sthar.Queries;
using Business.Handlers.Production.Queries;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Netsis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Netsis.Seritra.Commands
{
    public class CreateSeritraCommand : IRequest<IResult>
    {
        public int UretId { get; set; }
        public string UretsonFisNo { get; set; }
    }
    public class CreateSeritraCommandHandler : IRequestHandler<CreateSeritraCommand, IResult>
    {
        ISeritraRepository _seritraRepository;
        IMediator _mediator;

        public CreateSeritraCommandHandler(ISeritraRepository seritraRepository, IMediator mediator)
        {
            _seritraRepository = seritraRepository;
            _mediator = mediator;
        }

        public async Task<IResult> Handle(CreateSeritraCommand request, CancellationToken cancellationToken)
        {
            var getInjectionProduct = await _mediator.Send(new GetProductionQuery { Id = request.UretId });

            var product = getInjectionProduct.Data;

            var GetInjectionRawMaterials = await _mediator.Send(new GetInjectionMaterialsQuery { UretId = request.UretId });

            var getStharRecords = await _mediator.Send(new GetStharRecordsByReceiptQuery { FisNo = request.UretsonFisNo });

            //inckeyno Gc kodu G olan
            var stharProduct = getStharRecords.Data.Where(x => x.StokKodu == product.StokKodu && x.StharGckod == "G").FirstOrDefault().Inckeyno;

            _seritraRepository.Add(new Tblseritra
            {
                KayitTipi = "A",
                SeriNo = product.SeriNo,
                StokKodu = product.StokKodu,
                StraInc = stharProduct,
                Tarih = DateTime.Now,
                Acik2 = product.IsemriNo,
                Gckod = "G",
                Miktar = product.Adet,
                Miktar2 = product.Net,
                Belgeno = request.UretsonFisNo,
                Belgetip = "C",
                Haracik = "Uretim",
                SubeKodu = 0,
                Depokod = 599,
                Sipno = product.IsemriNo,
                Onaytipi = "A",
                Onaynum = 0,
                InitMiktar = product.Adet
            });



            foreach (var injectionMaterial in GetInjectionRawMaterials.Data)
            {
                var getMaterials = getStharRecords.Data
                     .Where(x => x.SYedek1 == injectionMaterial.Id.ToString());

                foreach (var material in getMaterials)
                {
                    _seritraRepository.Add(new Tblseritra
                    {
                        KayitTipi = "A",
                        SeriNo = injectionMaterial.VsSeriNo,
                        StokKodu = injectionMaterial.VsStokKodu,
                        StraInc = material.Inckeyno,
                        Tarih = DateTime.Now,
                        Acik2 = product.IsemriNo,
                        Gckod = "C",
                        Miktar = injectionMaterial.Harcanan,
                        Belgeno = request.UretsonFisNo,
                        Belgetip = "C",
                        Haracik = material.StharAciklama,
                        SubeKodu = 0,
                        Depokod = (short?)injectionMaterial.DepoKodu,
                        Sipno = product.IsemriNo,
                        Onaytipi = "A",
                        Onaynum = 0,
                        InitMiktar = injectionMaterial.Harcanan

                    });
                }
            }




            await _seritraRepository.SaveChangesAsync();

            return new SuccessResult(Messages.SeritraAdded);
        }
    }
}
