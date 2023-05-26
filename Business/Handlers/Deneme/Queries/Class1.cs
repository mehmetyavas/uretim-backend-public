
using AutoMapper;
using Business.Handlers.Production.Queries;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Production;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Deneme.Queries
{
    public class GetProductionsByPagingQuery : IRequest<IDataResult<List<GetProducedDto>>>
    {
        public GetProducedPagingDto GetProducedPagingDto { get; set; }
    }
    public class GetProductionsByPagingQueryHandler : IRequestHandler<GetProductionsByPagingQuery, IDataResult<List<GetProducedDto>>>
    {
        IMapper _mapper;
        IProductionRepository _productionRepository;
        public GetProductionsByPagingQueryHandler(IMapper mapper, IProductionRepository productionRepository)
        {
            _mapper = mapper;
            _productionRepository = productionRepository;
        }
        [LogAspect(typeof(MsSqlLogger))]
        public Task<IDataResult<List<GetProducedDto>>> Handle(GetProductionsByPagingQuery request, CancellationToken cancellationToken)
        {

            //var query = _productionRepository.GetQueryable(request.GetProducedPagingDto.Query);

            //int total = query.Count();

            //var getProducedDto = _mapper.ProjectTo<GetProducedDto>(query);

            //return new SuccessDataResult<List<GetProducedDto>>(await getProducedDto.ToListAsync());

            throw new Exception("Arama Yapılacak Alan Boş Olamaz!");
        }
    }
}
