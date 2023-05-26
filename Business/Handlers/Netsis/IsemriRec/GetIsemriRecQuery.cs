using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Netsis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Netsis.IsemriRec
{
    public class GetIsemriRecQuery : IRequest<IDataResult<List<Tblisemrirec>>>
    {
        public string WorkOrder { get; set; }
    }
    public class GetIsemriRecQueryHandler : IRequestHandler<GetIsemriRecQuery, IDataResult<List<Tblisemrirec>>>
    {
        IIsemriRecRepository _recRepository;

        public GetIsemriRecQueryHandler(IIsemriRecRepository recRepository)
        {
            _recRepository = recRepository;
        }

        public async Task<IDataResult<List<Tblisemrirec>>> Handle(GetIsemriRecQuery request, CancellationToken cancellationToken)
        {
            var result = await _recRepository.GetListAsync(x => x.Isemrino == request.WorkOrder);
            if (result.ToList().Count == 0)
                return new ErrorDataResult<List<Tblisemrirec>>(Messages.WorkOrderDoesntExist);

            return new SuccessDataResult<List<Tblisemrirec>>(result.ToList());
        }
    }

}
