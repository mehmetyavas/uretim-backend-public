using Business.Constants;
using Business.Handlers.Netsis.IsemriRec;
using Business.Handlers.Production.Queries;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.AssemblyRawMaterial.Rules
{
    public class AssemblyRules
    {
        IMediator _mediator;
        ISeritraRepository _seritraRepository;

        public AssemblyRules(IMediator mediator)
        {
            _mediator = mediator;
            _seritraRepository = (ISeritraRepository)ServiceTool.ServiceProvider.GetService(typeof(ISeritraRepository));
        }


        public async Task<IResult> CheckRawCodeIsCorrect(string workOrder, string stockCode)
        {
            var getWorkOrder = await _mediator.Send(new GetIsemriRecQuery { WorkOrder = workOrder });

            var RawCode = getWorkOrder.Data.Where(x => x.HamKodu == stockCode).FirstOrDefault();

            if (RawCode == null) return new ErrorResult(message: Messages.RawCodeDoesntMatch);

            return new SuccessResult();
        }

        public async Task<IResult> CheckIfRawMaterialAmountIsEnough(string serialNo, string stockCode)
        {

            var result = await _seritraRepository.GetStockRemainingAmount(stockCode, new List<int> { 500, 501, 599 }, serialNo);

            if (result.Count < 1)
                return new ErrorResult(Messages.NotEnoughAssemblyRawMaterial + "-" + serialNo + "-" + stockCode);

            return new SuccessResult();
        }

    }
}
