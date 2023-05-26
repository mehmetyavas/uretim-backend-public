using Amazon.Runtime.Internal;
using Business.Handlers.WorkOrders.Rules;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Uretim;
using Entities.Dtos.WorkOrder;
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
    public class GetWorkOrderInfoQuery : IRequest<IDataResult<WorkOrderInfoDTO>>
    {
        public int MachineId { get; set; }
        public string IsemriNo { get; set; }
    }

    public class GetWorkOrderInfoQueryHandler : IRequestHandler<GetWorkOrderInfoQuery, IDataResult<WorkOrderInfoDTO>>
    {
        IMachineRepository _machineRepository;
        IWorkOrderRepository _workOrderRepository;
        IAssemblyRawMRepository _assemblyRawMRepository;
        WorkOrderRules _rules;

        public GetWorkOrderInfoQueryHandler(
            IMachineRepository machineRepository,
            IWorkOrderRepository workOrderRepository,
            IAssemblyRawMRepository assemblyRawMRepository)
        {
            _machineRepository = machineRepository;
            _workOrderRepository = workOrderRepository;
            _rules = new WorkOrderRules(
                _machineRepository
                , _workOrderRepository);
            _assemblyRawMRepository = assemblyRawMRepository;
        }

        public async Task<IDataResult<WorkOrderInfoDTO>> Handle(GetWorkOrderInfoQuery request, CancellationToken cancellationToken)
        {
            var rulesResult = BusinessRules.Run(
                await _rules.CheckIfMachineExists(request.MachineId),
                await _rules.CheckIfWorkOrderExists(request.IsemriNo),
                await _rules.CheckIfMachineHasWorkOrder(request.MachineId, request.IsemriNo),
                await _rules.CheckIfWorkOrderIsNotClosed(request.IsemriNo)
                );
            if (rulesResult != null)
                throw new CustomException(rulesResult.Message, HttpStatusCode.BadRequest);

            //dictionary
            var workOrderDetail = await _workOrderRepository.GetIsemriInfo(request.MachineId, request.IsemriNo);

            // gövde ve üst kapak bilgileri
            var assemblyInfo = await _assemblyRawMRepository.GetAssemblyRawMateriaInfo(request.IsemriNo);

            //makine bilgisi
            var machineType = await _machineRepository.GetAsync(x => x.MachineCode == request.MachineId.ToString());

            return machineType.Description2 == "E"
                ? new SuccessDataResult<WorkOrderInfoDTO>
                {
                    Data = new WorkOrderInfoDTO
                    {
                        WorkOrderInfo = workOrderDetail,
                        AssemblyBody = null,
                        AssemblyTop = null
                    },
                    Message = "Enjeksiyon Bilgileri"
                }
                : new SuccessDataResult<WorkOrderInfoDTO>
                {
                    Data = new WorkOrderInfoDTO
                    {
                        WorkOrderInfo = workOrderDetail,
                        AssemblyBody = assemblyInfo.RawMaterialBody,
                        AssemblyTop = assemblyInfo.RawMaterialTop
                    },
                    Message = "Montaj bilgileri"
                };
        }
    }
}
