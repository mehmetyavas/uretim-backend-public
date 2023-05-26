using Business.Constants.Netsis;
using Business.Handlers.Production.Queries;
using Business.Handlers.WorkOrders.Queries;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Uretim;
using Entities.Dtos.Netsis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.InjectionRawMaterial.Commands
{
    public class CreatePackageCommand : IRequest<IDataResult<RpUretimSeriTakip>>
    {
        public string SeriNo { get; set; }
        public string Code1 { get; set; }
        public bool IsFire { get; set; } = false;
    }
    public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommand, IDataResult<RpUretimSeriTakip>>
    {
        IMediator _mediator;
        IEnjeksiyonRepository _enjeksiyonRepository;
        public CreatePackageCommandHandler(IMediator mediator, IEnjeksiyonRepository enjeksiyonRepository)
        {
            _mediator = mediator;
            _enjeksiyonRepository = enjeksiyonRepository;
        }

        public async Task<IDataResult<RpUretimSeriTakip>> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
        {
            var productQuery = await _mediator.Send(new GetProductionQuery { SeriNo = request.SeriNo });
            if (productQuery == null)
            {
                throw new Exception("Hata Oluştu! ( CREATE_PACKAGE )");
            }

            var product = productQuery.Data;


            var packagesStockCode = await _mediator.Send(new GetWorkOrderReceiptQuery { IsemriNo = product.IsemriNo, Code1 = request.Code1 });
            List<PackageDto> getPackages = null!;

            if (request.IsFire)
                getPackages = await _mediator.Send(new GetPackageRemainingQuery { StokKodu = NetsisConsts.ShrinkagebagStockCode });
            else
                getPackages = await _mediator.Send(new GetPackageRemainingQuery { StokKodu = packagesStockCode.Data.HamKodu });

            var minPackage = getPackages.FirstOrDefault();

            var packageToAdd = new RpUretimSeriTakip
            {
                UretId = product.Id,
                DepoKodu = 599,
                VsSeriNo = minPackage.SeriNo,
                VsStokKodu = minPackage.StokKodu,
                Harcanan = 1
            };

            _enjeksiyonRepository.Add(packageToAdd);
            await _enjeksiyonRepository.SaveChangesAsync();

            return new SuccessDataResult<RpUretimSeriTakip>(packageToAdd);
        }
    }
}
