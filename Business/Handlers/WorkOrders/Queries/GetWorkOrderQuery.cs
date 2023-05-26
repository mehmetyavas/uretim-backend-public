using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Netsis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.WorkOrders.Queries
{
    public class GetWorkOrderQuery : IRequest<IDataResult<IEnumerable<Tblisemrirec>>>
    {
        public string WorkOrder { get; set; }
    }
    public class GetWorkOrderQueryHandler : IRequestHandler<GetWorkOrderQuery, IDataResult<IEnumerable<Tblisemrirec>>>
    {
        IIsemriRecRepository _isemriRecRepository;

        public GetWorkOrderQueryHandler(IIsemriRecRepository isemriRecRepository)
        {
            _isemriRecRepository = isemriRecRepository;
        }

        public async Task<IDataResult<IEnumerable<Tblisemrirec>>> Handle(GetWorkOrderQuery request, CancellationToken cancellationToken)
        {
            var result = await _isemriRecRepository.GetListAsync(x => x.Isemrino == request.WorkOrder);

            return new SuccessDataResult<IEnumerable<Tblisemrirec>>(result);
        }
    }
}
