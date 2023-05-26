using AutoMapper;
using Business.Constants;
using Business.Handlers.Giris.rules;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Giris;
using Entities.Dtos.Giris;
using MediatR;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Giris.Commands
{
    public class CreateHygieneAnswerCommand : IRequest<IResult>
    {
        public int Staff { get; set; }

        public int Machine { get; set; }

        public List<HygieneDto> Hygiene { get; set; } = new List<HygieneDto>();
    }

    public class CreateHygieneAnswerCommandHandler : IRequestHandler<CreateHygieneAnswerCommand, IResult>
    {
        IHygieneRepository _hygieneRepository;
        IMapper _mapper;
        IMediator _mediator;
        HygieneRules _rules;

        public CreateHygieneAnswerCommandHandler(IHygieneRepository hygieneRepository, IMapper mapper, IMediator mediator)
        {
            _hygieneRepository = hygieneRepository;
            _mapper = mapper;
            _mediator = mediator;
            _rules = new HygieneRules(_mediator);
        }

        public async Task<IResult> Handle(CreateHygieneAnswerCommand request, CancellationToken cancellationToken)
        {

            if (request.Hygiene.Count == 0)
                return new ErrorResult(Messages.HygieneAnswerIsNull);

            BusinessRules.Run(await _rules.CheckIfMachineAndStaffIsExist(request.Machine, request.Staff));


            var mappedrecords = _mapper.Map<List<EryMakineTemizlik>>(request.Hygiene);

            foreach (var record in mappedrecords)
            {
                record.Staff = request.Staff;
                record.Machine = request.Machine;
                _hygieneRepository.Add(record);
            }

            await _hygieneRepository.SaveChangesAsync();

            return new SuccessResult(Messages.HygieneAnswerAdded);

        }
    }
}
