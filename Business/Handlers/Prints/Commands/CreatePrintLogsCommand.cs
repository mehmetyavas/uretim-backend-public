using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Uretim;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Prints.Commands
{
    public class CreatePrintLogsCommand : IRequest<IResult>
    {
        public string SerialNo { get; set; }
        public string MachineId { get; set; }
        public string StaffId { get; set; }
    }
    public class CreatePrintLogsCommandHandler : IRequestHandler<CreatePrintLogsCommand, IResult>
    {
        IPrintlogsRepository _repository;

        public CreatePrintLogsCommandHandler(IPrintlogsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreatePrintLogsCommand request, CancellationToken cancellationToken)
        {

            _repository.Add(new PrintLog
            {
                SerialNo = request.SerialNo,
                MachineId = request.MachineId,
                StaffId = request.StaffId
            });
            await _repository.SaveChangesAsync();


            return new SuccessResult("Kaydedildi!");
        }
    }
}
