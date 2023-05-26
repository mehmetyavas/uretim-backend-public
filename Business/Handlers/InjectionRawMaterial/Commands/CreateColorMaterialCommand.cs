using Business.Constants;
using Business.Handlers.InjectionRawMaterial.Queries;
using Business.Handlers.Production.Commands;
using Business.Handlers.Production.Queries;
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

namespace Business.Handlers.InjectionRawMaterial.Commands
{
    public class CreateColorMaterialCommand : IRequest<IDataResult<List<RpUretimSeriTakip>>>
    {
        public string SeriNo { get; set; }
    }
    public class CreateColorMaterialCommandHandler : IRequestHandler<CreateColorMaterialCommand, IDataResult<List<RpUretimSeriTakip>>>
    {
        IMediator _mediator;
        IEnjeksiyonRepository _enjeksiyonRepository;

        public CreateColorMaterialCommandHandler(IMediator mediator, IEnjeksiyonRepository enjeksiyonRepository)
        {
            _mediator = mediator;
            _enjeksiyonRepository = enjeksiyonRepository;
        }

        public async Task<IDataResult<List<RpUretimSeriTakip>>> Handle(CreateColorMaterialCommand request, CancellationToken cancellationToken)
        {

            var getProduct = await _mediator.Send(new GetProductionQuery { SeriNo = request.SeriNo });

            if (getProduct == null)
                return new ErrorDataResult<List<RpUretimSeriTakip>>("product bulunamadı ( CREATE_COLOR ) ");

            var product = getProduct.Data;

            var getInjection = await _mediator.Send(new GetInjectionColorQuery { WorkOrder = product.IsemriNo });

            if (getInjection.Count == 0)
            {
                return new ErrorDataResult<List<RpUretimSeriTakip>>(Messages.NotEnoughColorMaterial);
            }

            var currentBarrel = getInjection.Where(x => x.VarilId == getInjection.Min(x => x.VarilId));

            var nextBarrel = getInjection.Where(x => x.VarilId == getInjection.Min(x => x.VarilId) + 1);

            bool nextBarrelFlag = false;

            decimal? remainingHarcanan = 0;

            var listOfColorMat = new List<RpUretimSeriTakip>();

            foreach (var material in currentBarrel)
            {
                material.Harcanan = material.Harcanan == null ? 0 : material.Harcanan;
                if (material.StharGcmik >= material.Harcanan + product.Net * (material.Oran / 100))
                {
                    listOfColorMat.Add(new RpUretimSeriTakip
                    {
                        UretId = product.Id,
                        DepoKodu = product.MakId,
                        VsStokKodu = material.StokKodu,
                        VsSeriNo = material.SeriNo,
                        Harcanan = (decimal)(product.Net * (material.Oran / 100))
                    });

                    material.Harcanan += product.Net * (material.Oran / 100);
                    await _mediator.Send(new UpdateColorMaterialCommand { Varil = material });
                }
                else
                {
                    if (nextBarrel == null)
                    {
                        return new ErrorDataResult<List<RpUretimSeriTakip>>(Messages.NotEnoughColorMaterial);
                    }

                    listOfColorMat.Add(new RpUretimSeriTakip
                    {
                        UretId = product.Id,
                        DepoKodu = product.MakId,
                        VsStokKodu = material.StokKodu,
                        VsSeriNo = material.SeriNo,
                        Harcanan = (decimal)(material.StharGcmik - material.Harcanan)
                    });

                    remainingHarcanan = product.Net * (material.Oran / 100) - (decimal)(material.StharGcmik - material.Harcanan);

                    material.Harcanan = material.StharGcmik;

                    await _mediator.Send(new UpdateColorMaterialCommand { Varil = material });

                    nextBarrelFlag = true;
                }
            }
            // kontrol edilmesi gerekiyor!!!!
            if (nextBarrelFlag)
            {
                foreach (var nextMaterial in nextBarrel)
                {
                    listOfColorMat.Add(new RpUretimSeriTakip
                    {
                        UretId = product.Id,
                        DepoKodu = product.MakId,
                        VsStokKodu = nextMaterial.StokKodu,
                        VsSeriNo = nextMaterial.SeriNo,
                        Harcanan = (decimal)remainingHarcanan
                    });
                    nextMaterial.Harcanan = nextMaterial.Harcanan == null ? 0 : nextMaterial.Harcanan;
                    nextMaterial.Harcanan += remainingHarcanan;
                    await _mediator.Send(new UpdateColorMaterialCommand { Varil = nextMaterial });
                }
            }

            // burası daha düzgün olabilir!!!
            foreach (var item in listOfColorMat)
            {
                _enjeksiyonRepository.Add(item);
            }
            await _enjeksiyonRepository.SaveChangesAsync();

            return new SuccessDataResult<List<RpUretimSeriTakip>>(listOfColorMat);
        }
    }
}
