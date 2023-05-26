using Business.Handlers.Production.Queries;
using Core.Utilities.Results;
using Entities.Concrete.Uretim;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Products.Queries
{
    public class GetProductLabelQuery : IRequest<IDataResult<RpEtiketTbl>>
    {

        public string SerialNo { get; set; }
        public int UretTip { get; set; }
    }
    public class GetProductLabelQueryHandler : IRequestHandler<GetProductLabelQuery, IDataResult<RpEtiketTbl>>
    {
        IMediator _mediator;

        public GetProductLabelQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IDataResult<RpEtiketTbl>> Handle(GetProductLabelQuery request, CancellationToken cancellationToken)
        {
            var labelInfoResult = await _mediator.Send(new GetProductionLabelInfoQuery { SerialNo = request.SerialNo, ProductType = request.UretTip });

            if (labelInfoResult.Data == null || !labelInfoResult.Success)
                throw new Exception("Fiş Hatası");

            return labelInfoResult;



        }
    }
}
