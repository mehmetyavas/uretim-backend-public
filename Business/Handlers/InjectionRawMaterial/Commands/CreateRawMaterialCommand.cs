using Business.Constants;
using Business.Handlers.InjectionRawMaterial.Queries;
using Business.Handlers.Production.Commands;
using Business.Handlers.Production.Queries;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Uretim;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;

namespace Business.Handlers.InjectionRawMaterial.Commands
{
    public class CreateRawMaterialCommand : IRequest<IDataResult<List<RpUretimSeriTakip>>>
    {
        public string SeriNo { get; set; }
        public bool IsColorTransparent { get; set; }
    }
    public class CreateRawMaterialCommandHandler : IRequestHandler<CreateRawMaterialCommand, IDataResult<List<RpUretimSeriTakip>>>
    {
        IMediator _mediator;
        IEnjeksiyonRepository _enjeksiyonRepository;

        public CreateRawMaterialCommandHandler(IMediator mediator, IEnjeksiyonRepository enjeksiyonRepository)
        {
            _mediator = mediator;
            _enjeksiyonRepository = enjeksiyonRepository;
        }

        public async Task<IDataResult<List<RpUretimSeriTakip>>> Handle(CreateRawMaterialCommand request, CancellationToken cancellationToken)
        {
            //içeriği girilecek üretim
            var getAddedProduct = await _mediator.Send(new GetProductionQuery { SeriNo = request.SeriNo });

            if (getAddedProduct.Data == null)
                return new ErrorDataResult<List<RpUretimSeriTakip>>("product data is null");

            var product = getAddedProduct.Data;


            //sadece Hammadde Ağırlığı 
            decimal rawMaterialKg = 0;

            if (!request.IsColorTransparent)
                rawMaterialKg = (product.Net * 1000 - ((product.BAgirlik / 100) * product.Adet)) / 1000;
            else
                rawMaterialKg = product.Net;


            //işemrine göre hammadde
            var getinjection = await _mediator.Send(new GetInjectionRawMaterialQuery { WorkOrder = product.IsemriNo });


            var listOfRawMaterial = new List<RpUretimSeriTakip>();

            // varilId ye  göre bakiyesi   mininmum varil 
            var minBarrelNo = getinjection.Where(x => x.VarilId == getinjection.Min(x => x.VarilId));

            // bir sonraki varil
            var nextBarrel = getinjection.Where(x => x.VarilId == getinjection.Min(x => x.VarilId) + 1);


            decimal? remainingHarcanan = 0;

            // eğer sonraki varil id ye geçiyorsa kod bloğunu calıstırıyor!
            bool nextBarrelFlag = false;

            //hammadde
            foreach (var material in minBarrelNo)
            {
                //var colorRatioPerMaterial = colorRatio / minBarrelNo.ToList().Count;
                var calculateHarcanan = rawMaterialKg * (material.Oran / 100);

                if (material.StharGcmik >= material.Harcanan + calculateHarcanan)
                {
                    listOfRawMaterial.Add(new RpUretimSeriTakip
                    {
                        UretId = product.Id,
                        DepoKodu = product.MakId,
                        VsStokKodu = material.StokKodu,
                        VsSeriNo = material.SeriNo,
                        Harcanan = (decimal)calculateHarcanan
                    });

                    material.Harcanan += (decimal)calculateHarcanan;
                    await _mediator.Send(new UpdateRawMaterialCommand { Varil = material });
                }
                else
                {
                    if (nextBarrel == null)
                    {
                        // sonraki varil yoksa  oluşturulan seriyi geri siliyor
                        await _mediator.Send(new DeleteProductionCommand { SeriNo = request.SeriNo });
                        return new ErrorDataResult<List<RpUretimSeriTakip>>(Messages.NotEnoughRawMaterial);
                    }

                    listOfRawMaterial.Add(new RpUretimSeriTakip
                    {
                        UretId = product.Id,
                        DepoKodu = product.MakId,
                        VsStokKodu = material.StokKodu,
                        VsSeriNo = material.SeriNo,
                        Harcanan = (decimal)(material.StharGcmik - material.Harcanan)
                    });

                    //önceki varilden kalan miktar- kg * oran/100
                    var deneme = (decimal)(material.StharGcmik - material.Harcanan);
                    remainingHarcanan += rawMaterialKg * (material.Oran / 100) - (decimal)(material.StharGcmik - material.Harcanan);

                    material.Harcanan = material.StharGcmik;
                    await _mediator.Send(new UpdateRawMaterialCommand { Varil = material });

                    nextBarrelFlag = true;
                }
            }
            // kontrol edilmesi gerekiyor!!!!
            if (nextBarrelFlag)
            {
                foreach (var nextMaterial in nextBarrel)
                {
                    var remainingPerMaterial = Math.Round((decimal)(remainingHarcanan * (nextMaterial.Oran / 100)), 8);

                    listOfRawMaterial.Add(new RpUretimSeriTakip
                    {
                        UretId = product.Id,
                        DepoKodu = product.MakId,
                        VsStokKodu = nextMaterial.StokKodu,
                        VsSeriNo = nextMaterial.SeriNo,
                        Harcanan = Math.Round(remainingPerMaterial, 8)
                    });
                    nextMaterial.Harcanan += Math.Round(remainingPerMaterial, 8);
                    await _mediator.Send(new UpdateRawMaterialCommand { Varil = nextMaterial });
                }
            }

            // burası daha düzgün olabilir!!!
            foreach (var item in listOfRawMaterial)
                _enjeksiyonRepository.Add(item);

            await _enjeksiyonRepository.SaveChangesAsync();

            return new SuccessDataResult<List<RpUretimSeriTakip>>(listOfRawMaterial);
        }
    }
}
