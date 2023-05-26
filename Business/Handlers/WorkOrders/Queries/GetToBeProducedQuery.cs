using Business.Constants;
using Business.Handlers.WorkOrders.Rules;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.WorkOrder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.WorkOrders.Queries
{
    public class GetToBeProducedQuery : IRequest<IDataResult<ProducedDTO>>
    {
        public int Ciid { get; set; }
    }

    public class GetToBeProducedQueryHandler : IRequestHandler<GetToBeProducedQuery, IDataResult<ProducedDTO>>
    {
        IWorkOrderRepository _workOrderRepository;
        IEflowRepository _eflowRepository;
        IProductionRepository _productionRepository;

        public GetToBeProducedQueryHandler(
            IWorkOrderRepository workOrderRepository,
            IEflowRepository eflowRepository,
            IProductionRepository productionRepository)
        {
            _workOrderRepository = workOrderRepository;
            _eflowRepository = eflowRepository;
            _productionRepository = productionRepository;
        }

        public async Task<IDataResult<ProducedDTO>> Handle(GetToBeProducedQuery request, CancellationToken cancellationToken)
        {
            var toBeProducedResult = await _eflowRepository.GetAsync(x => x.Ciid == request.Ciid);
            if (toBeProducedResult == null)
                throw new Exception(Messages.EflowException);

            var producedResult = await _productionRepository.ProducedItem(request.Ciid.ToString());

            var remaining = toBeProducedResult.Value - producedResult;

            return new SuccessDataResult<ProducedDTO>(new ProducedDTO
            {
                ToBeProducedItem = toBeProducedResult,
                Produced = producedResult,
                Remaining = remaining
            });
        }
    }
}
