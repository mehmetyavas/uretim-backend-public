using Business.Constants;
using Business.Constants.Netsis;
using Business.Handlers.InjectionRawMaterial.Queries;
using Business.Handlers.WorkOrders.Queries;
using Core.Utilities.Results;
using MediatR;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Production.Queries
{
    public class GetInjectionMaterialsControlQuery : IRequest<IResult>
    {
        public string WorkOrder { get; set; }
        public int ProductId { get; set; }
    }
    class GetInjectionMaterialsControlQueryHandler : IRequestHandler<GetInjectionMaterialsControlQuery, IResult>
    {

        IMediator _mediator;

        public GetInjectionMaterialsControlQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IResult> Handle(GetInjectionMaterialsControlQuery request, CancellationToken cancellationToken)
        {

            var workOrderReceipt = await _mediator.Send(new GetWorkOrderQuery { WorkOrder = request.WorkOrder });


            var getproductionMaterials = await _mediator.Send(new GetInjectionMaterialsQuery { UretId = request.ProductId });

            foreach (var receipt in workOrderReceipt.Data.Where(x => x.HamKodu.Contains(NetsisConsts.InjectionStockSearch)))
            {
                if (getproductionMaterials.Data.Where(x => x.VsStokKodu.Contains(receipt.HamKodu)).Count() == 0)
                    return new ErrorResult(Messages.RawMaterialError);

            }

            return new SuccessResult();
        }

    }
}
