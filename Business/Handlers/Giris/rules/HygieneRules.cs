using Business.Constants;
using Business.Handlers.Machines.Queries;
using Core.Utilities.Results;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Giris.rules
{
    public class HygieneRules
    {
        IMediator _mediator;

        public HygieneRules(IMediator mediator)
        {
            _mediator = mediator;
        }



        public async Task<IResult> CheckIfMachineAndStaffIsExist(int machineId, int staffId)
        {
            var giris = await _mediator.Send(new GetGirisQuery());


            var checkMachine = giris.Data.Machines.Where(x => x.MachineCode == machineId.ToString());

            var checkStaff = giris.Data.Staffs.Where(x => x.StaffCode == staffId.ToString());

            if (!checkMachine.Any() || !checkStaff.Any())
                return new ErrorResult(Messages.ThereIsNoMachineOrStaffOnThisCode);

            return new SuccessResult();
        }



    }
}
