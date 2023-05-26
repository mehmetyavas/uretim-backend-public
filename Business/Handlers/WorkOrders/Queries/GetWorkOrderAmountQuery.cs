using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.WorkOrders.Queries
{
    public class GetWorkOrderAmountQuery : IRequest<IDataResult<decimal>>
    {
        public string WorkOrder { get; set; }
    }
    public class GetWorkOrderAmountQueryHandler : IRequestHandler<GetWorkOrderAmountQuery, IDataResult<decimal>>
    {
        IWorkOrderRepository _workOrderRepository;

        public GetWorkOrderAmountQueryHandler(IWorkOrderRepository workOrderRepository)
        {
            _workOrderRepository = workOrderRepository;
        }

        public async Task<IDataResult<decimal>> Handle(GetWorkOrderAmountQuery request, CancellationToken cancellationToken)
        {
            var workOrder = await _workOrderRepository.GetAsync(x => x.Isemrino == request.WorkOrder);
            return new SuccessDataResult<decimal>((decimal)workOrder.Miktar);
        }
    }
}
