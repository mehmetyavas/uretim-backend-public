using AutoMapper;
using Business.Constants;
using Business.Handlers.WorkOrders.Rules;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.WorkOrder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.WorkOrders.Queries
{
    public class GetWorkOrderListQuery : IRequest<IDataResult<IEnumerable<WorkOrderDTO>>>
    {
        public int MachineId { get; set; }
    }
    public class GetWorkOrderListQueryHandler : IRequestHandler<GetWorkOrderListQuery, IDataResult<IEnumerable<WorkOrderDTO>>>
    {
        IWorkOrderRepository _workOrderRepository;
        IMachineRepository _machineRepository;
        IMapper _mapper;
        WorkOrderRules _rules;
        public GetWorkOrderListQueryHandler(IWorkOrderRepository workOrderRepository, IMapper mapper, IMachineRepository machineRepository)
        {
            _workOrderRepository = workOrderRepository;
            _machineRepository = machineRepository;
            _mapper = mapper;
            _rules = new(
                _machineRepository,
                _workOrderRepository);
        }

        [LogAspect(typeof(MsSqlLogger))]
        public async Task<IDataResult<IEnumerable<WorkOrderDTO>>> Handle(GetWorkOrderListQuery request, CancellationToken cancellationToken)
        {
            var rulesResult = BusinessRules.Run(await _rules.CheckIfMachineExists(request.MachineId));
            if (rulesResult != null)
                throw new Exception(rulesResult.Message);

            var workOrder = await _workOrderRepository
                .GetListAsync(x =>
                    x.DepoKodu == request.MachineId &&
                    x.Kapali == "H");

            var mappedWorkOrder = _mapper.Map<List<WorkOrderDTO>>(workOrder);

            return workOrder != null
                ? new SuccessDataResult<List<WorkOrderDTO>>(mappedWorkOrder)
                : throw new CustomException(Messages.WorkOrderDoesntExist, HttpStatusCode.NotFound); 
        }
    }
}
