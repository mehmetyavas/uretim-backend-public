using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.WorkOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.WorkOrders.Rules
{
    public class WorkOrderRules
    {
        IMachineRepository _machineRepository;
        IWorkOrderRepository _workOrderRepository;
        IWorkOrderInfoRepository _workOrderInfoRepository;

        public WorkOrderRules(IWorkOrderRepository workOrderRepository)
        {
            _workOrderRepository = workOrderRepository;
        }

        public WorkOrderRules(
            IMachineRepository machineRepository,
            IWorkOrderRepository workOrderRepository)
        {
            _machineRepository = machineRepository;
            _workOrderRepository = workOrderRepository;
        }

        public async Task<IResult> CheckIfMachineExists(int machineId)
        {
            var machine = await _machineRepository.GetAsync(x => x.MachineCode == machineId.ToString());

            return machine != null
                ? new SuccessResult()
                : new ErrorResult(Messages.MachineDoesntExist);
        }
        public async Task<IResult> CheckIfWorkOrderExists(string workOrder)
        {
            var workOrderResult = await _workOrderRepository.GetAsync(x => x.Isemrino == workOrder);

            return workOrderResult != null
                ? new SuccessResult()
                : new ErrorResult(Messages.WorkOrderDoesntExist);
        }

        public async Task<IResult> CheckIfMachineHasWorkOrder(int machineId, string workOrder)
        {
            var workOrderResult = await _workOrderRepository.GetAsync(x => x.DepoKodu == machineId && x.Kapali == "H");
            if (workOrderResult == null)
                return new ErrorResult(Messages.WorkOrderDoesntExist);
            if (workOrderResult.Isemrino != workOrder)
                return new ErrorResult(Messages.WorkOrderDoesntBelongsToThisMachine);
            return new SuccessResult();
        }

        public async Task<IResult> CheckIfWorkOrderIsNotClosed(string workOrder)
        {
            var workOrderResult = await _workOrderRepository.GetAsync(x => x.Isemrino == workOrder && x.Kapali == "H");
            return workOrderResult != null
                ? new SuccessResult()
                : new ErrorResult(Messages.ThisWorkOrderIsClosed);
        }
    }
}
