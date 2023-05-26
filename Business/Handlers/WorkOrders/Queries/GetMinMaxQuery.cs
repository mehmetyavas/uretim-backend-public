using Amazon.Runtime.Internal;
using Business.Constants;
using Business.Handlers.WorkOrders.Rules;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Uretim;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.WorkOrders.Queries
{
    public class GetMinMaxQuery : IRequest<IDataResult<RpIsemriBilgi>>
    {
        public string IsemriNo { get; set; }
    }

    public class GetMinMaxQueryHandler : IRequestHandler<GetMinMaxQuery, IDataResult<RpIsemriBilgi>>
    {
        IWorkOrderInfoRepository _workOrderInfoRepository;
        WorkOrderRules _rules;

        //kurallar için gerekli
        IWorkOrderRepository _workOrderRepository;

        public GetMinMaxQueryHandler(IWorkOrderInfoRepository workOrderInfoRepository, IWorkOrderRepository workOrderRepository)
        {
            _workOrderRepository = workOrderRepository;
            _workOrderInfoRepository = workOrderInfoRepository;
            _rules = new WorkOrderRules(_workOrderRepository);
        }
        public async Task<IDataResult<RpIsemriBilgi>> Handle(GetMinMaxQuery request, CancellationToken cancellationToken)
        {
            var rulesResult = BusinessRules.Run(
                await _rules.CheckIfWorkOrderExists(request.IsemriNo)
                );


            if (rulesResult != null)
                throw new CustomException(rulesResult.Message, HttpStatusCode.NotFound);

            var result = await _workOrderInfoRepository.GetListAsync(x => x.Isemrino == request.IsemriNo);

           
            var getLastInfo = result.ToList().OrderByDescending(x => x.Tarih).FirstOrDefault();

            return new SuccessDataResult<RpIsemriBilgi>(getLastInfo);

        }
    }
}
