using Business.Constants;
using Business.Constants.Netsis;
using Business.Handlers.InjectionRawMaterial.Queries;
using Business.Handlers.Netsis.stsabit.Queries;
using Business.Handlers.Production.Queries;
using Business.Handlers.WorkOrders.Queries;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Netsis;
using Entities.Concrete.Netsis;
using MediatR;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Production.Rules
{
    public class ProductionRules
    {
        readonly IProductionRepository _productionRepository;

        readonly IIsemriRecRepository _isemriRecRepository;
        readonly ISeritraRepository _seritraRepository;
        IMediator _mediator;
        public ProductionRules(
            IProductionRepository productionRepository,
            IIsemriRecRepository isemriRecRepository,
            ISeritraRepository seritraRepository,
            IMediator mediator)
        {
            _productionRepository = productionRepository;
            _isemriRecRepository = isemriRecRepository;
            _seritraRepository = seritraRepository;
            _mediator = mediator;
        }


        public async Task<IResult> CheckIfSerialNoExists(string serialNo)
        {
            var result = await _productionRepository.GetAsync(x => x.SeriNo == serialNo);

            return result == null
                ? new SuccessResult()
                : new ErrorResult(Messages.SerialNoExist);
        }

        //buraya minimum değerin üstündeyse kontrolu atlaması için bir kontrol koyulacak
        public async Task<IResult> CheckValueIsBetweenMaxMin(
            string workOrder,
            decimal kg,
            decimal quantity,
            int uretTip)
        {
            //min max kontrolü iççin gerekli datayı alıyor
            var result = await _mediator.Send(new GetMinMaxQuery { IsemriNo = workOrder });

            if (result.Data == null)
                return new ErrorResult(Messages.MinMaxDoesntExist);

            if ((kg >= result.Data.Minkg && kg <= result.Data.Maxkg) &&
                (quantity >= result.Data.Minad && quantity <= result.Data.Maxad))
            {
                return new SuccessResult();
            }
            // üret tip üretim değilse kontrole girmeyecek
            if (uretTip != 0)
                return new SuccessResult();

            return new ErrorResult(Messages.MaxMinControl);
        }



        public async Task<IResult> CheckIfPackageOrBagIsEnough(string workOrder, string packageOrBagCode, int uretTip)
        {

            var packageStockCode = await _mediator.Send(new GetBagOrPackageQuery { Code1 = packageOrBagCode });

            var getPackageCodeByWorkOrder = await _isemriRecRepository
                .GetAsync(x =>
                    x.Isemrino == workOrder &&
                    packageStockCode.Data.Contains(x.HamKodu));

            //dene
            if ((uretTip == 2 || uretTip == 3) && packageOrBagCode == NetsisConsts.BagCode)
            {
                var packageShrinkage = await _mediator.Send(new GetPackageRemainingQuery { StokKodu = NetsisConsts.ShrinkagebagStockCode });

                return packageShrinkage.Count == 0
                    ? new ErrorResult(Messages.NotEnoughPackageException)
                    : new SuccessResult();
            }

            var package = await _mediator.Send(new GetPackageRemainingQuery { StokKodu = getPackageCodeByWorkOrder.HamKodu });

            return package.Count == 0
                ? new ErrorResult(Messages.NotEnoughPackageException)
                : new SuccessResult();

        }


        //buraya montaj hammaddesi uyarlanabilir mi ?
        public async Task<IResult> CheckIfRawMaterialIsEnough(string isemriNo, decimal kg, string macType)
        {
            if (macType == "E")
            {
                var getRawMaterial = await _mediator.Send(new GetInjectionRawMaterialQuery { WorkOrder = isemriNo });

                if (getRawMaterial.Count < 1)
                    return new ErrorResult(Messages.NotEnoughRawMaterial);

                var rawMaterialWeight = getRawMaterial
                    .Sum(x => x.StharGcmik - (x.Harcanan == null ? 0 : x.Harcanan));


                if (rawMaterialWeight < kg)
                    return new ErrorResult(Messages.NotEnoughRawMaterial);
            }
            else
            {

            }

            return new SuccessResult();
        }

        public async Task<IResult> CheckIfColorIsEnough(string isemriNo, decimal kg)
        {

            var getColor = await _mediator.Send(new GetInjectionColorQuery { WorkOrder = isemriNo });

            if (getColor.Count < 1)
                return new ErrorResult(Messages.NotEnoughColorMaterial);

            //ilk harcanacak varil
            var getMinBarrel = getColor.Where(x => x.VarilId == getColor.Min(x => x.VarilId)).FirstOrDefault();

            var getBarrelWeight = getColor.Sum(x => x.StharGcmik - (x.Harcanan == null ? 0 : x.Harcanan));

            if (getBarrelWeight < kg * getMinBarrel.Oran / 100)
                return new ErrorResult(Messages.NotEnoughColorMaterial);

            return new SuccessResult();
        }

        public async Task<IResult> CheckIfWorkOrderAmountIsEnough(string isemriNo, decimal adet)
        {
            var getworkOrderAmount = await _mediator.Send(new GetWorkOrderAmountQuery { WorkOrder = isemriNo });

            var getproductions = await _mediator.Send(new GetProductionsQuery { IsemriNo = isemriNo });

            var productionAmount = getproductions.Data.Where(x => x.UretTip == 0 || x.UretTip == 3).Sum(x => x.Adet);

            if (getworkOrderAmount.Data < productionAmount + adet)
                return new ErrorResult(Messages.ProductAmountIsBiggerThanWorkOrderAmount);

            return new SuccessResult();
        }







    }
}
