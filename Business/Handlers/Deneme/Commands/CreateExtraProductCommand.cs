using Business.Handlers.Netsis.Sthar.Commands;
using Business.Handlers.Production.Commands;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Netsis;
using MediatR;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Deneme.Commands
{
    public class CreateExtraProductCommand : IRequest<IDataResult<string>>
    {
        public string SerialNo { get; set; }
        public string StokKodu { get; set; }
        public string YapKod { get; set; }
        public decimal miktar { get; set; }
    }
    public class CreateExtraProductCommandHandler : IRequestHandler<CreateExtraProductCommand, IDataResult<string>>
    {
        IProductionRepository _productionRepository;
        ISeritraRepository _seritraRepository;
        public CreateExtraProductCommandHandler(
            IProductionRepository productionRepository, ISeritraRepository seritraRepository)
        {
            _productionRepository = productionRepository;
            _seritraRepository = seritraRepository;
        }


        public async Task<IDataResult<string>> Handle(CreateExtraProductCommand request, CancellationToken cancellationToken)
        {
            request.StokKodu = request.StokKodu.ToUpper();
            _productionRepository.Add(new Entities.Concrete.Uretim.RpUretimSeri
            {
                SeriNo = request.SerialNo,
                Adet = request.miktar,
                BAgirlik = 1,
                Brut = 1,
                Ciid = "1",
                Created = DateTime.Now,
                Dara = 1,
                IsemriNo = "1",
                LotNo = "1",
                MakId = 1,
                Net = 1,
                PersonelId = 1,
                StokKodu = request.StokKodu,
                Tarih = DateTime.Now,
                UretTip = 0,
                Uretildi = true,
                Vardiya = "1",
                SuskNo = "URETIMBCKND",
                YapKod = request.YapKod

            });
            await _productionRepository.SaveChangesAsync();

            _seritraRepository.Add(new Tblseritra
            {
                KayitTipi = "A",
                SeriNo = request.SerialNo,
                Depokod = 599,
                StokKodu = request.StokKodu,
                Tarih = DateTime.Now,
                Gckod = "G",
                Miktar = request.miktar,
                SubeKodu = 0
            });
            await _seritraRepository.SaveChangesAsync();

            return new SuccessDataResult<string>(request.SerialNo);

        }
    }
}
