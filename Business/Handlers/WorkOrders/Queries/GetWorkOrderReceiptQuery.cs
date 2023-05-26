using Business.Handlers.Netsis.stsabit.Queries;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Netsis;
using Entities.Concrete.Netsis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.WorkOrders.Queries
{
    public class GetWorkOrderReceiptQuery : IRequest<IDataResult<Tblisemrirec>>
    {
        public string Code1 { get; set; }
        public string IsemriNo { get; set; }
    }
    public class GetWorkOrderReceiptQueryHandler : IRequestHandler<GetWorkOrderReceiptQuery, IDataResult<Tblisemrirec>>
    {
        IMediator _mediator;
        IIsemriRecRepository _isemriRecRepository;

        public GetWorkOrderReceiptQueryHandler(IMediator mediator, IIsemriRecRepository isemriRecRepository)
        {
            _mediator = mediator;
            _isemriRecRepository = isemriRecRepository;
        }

        public async Task<IDataResult<Tblisemrirec>> Handle(GetWorkOrderReceiptQuery request, CancellationToken cancellationToken)
        {

            var packageOrBagStockCode = await _mediator.Send(new GetBagOrPackageQuery { Code1 = request.Code1 });

            var isemriRepo = await _isemriRecRepository
               .GetAsync(x =>
                   x.Isemrino == request.IsemriNo &&
                    packageOrBagStockCode.Data.Contains(x.HamKodu));

            return new SuccessDataResult<Tblisemrirec>(isemriRepo);
        }
    }
}
