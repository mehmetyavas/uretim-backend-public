using AutoMapper;
using Business.Handlers.InjectionRawMaterial.Queries;
using Business.Handlers.Production.Queries;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Pagination;
using Entities.Dtos.Production;
using MediatR;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Products.Queries
{
    public class GetProducedItemsByPagingQuery : IRequest<IDataResult<List<GetProducedDto>>>
    {
        public string WorkOrder { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
    }

    public class GetProducedItemsByPagingQueryHandler : IRequestHandler<GetProducedItemsByPagingQuery, IDataResult<List<GetProducedDto>>>
    {
        IProductionRepository _productionRepository;
        IMapper _mappper;
        IMediator _mediator;

        public GetProducedItemsByPagingQueryHandler(IProductionRepository productionRepository, IMapper mappper, IMediator mediator)
        {
            _productionRepository = productionRepository;
            _mappper = mappper;
            _mediator = mediator;
        }

        public async Task<IDataResult<List<GetProducedDto>>> Handle(GetProducedItemsByPagingQuery request, CancellationToken cancellationToken)
        {



            var count = await _productionRepository.GetCountAsync(x => x.IsemriNo == request.WorkOrder);


            var skip = request.Page == 0 ? 0 : (request.Page - 1) * request.Limit;

            var productsByWorkOrder = await _productionRepository
                .GetListByPagingAsync(request.WorkOrder, (int)skip, request.Limit == 0 ? count : (int)request.Limit);

            var mappedProducts = _mappper.Map<List<GetProducedDto>>(productsByWorkOrder);

          
            var result = new CustomPaginationResult<List<GetProducedDto>>(mappedProducts);

            result.TotalRecords = count;

            return result;
        }
    }
}
