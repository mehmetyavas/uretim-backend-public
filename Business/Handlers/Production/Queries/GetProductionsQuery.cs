using Amazon.Runtime.Internal;
using AutoMapper;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Uretim;
using Entities.Dtos.Production;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Production.Queries
{
    public class GetProductionsQuery : IRequest<IDataResult<List<GetProducedDto>>>
    {
        public string? LotNo { get; set; }
        public int? MachineId { get; set; }
        public int? Type { get; set; }
        public string? IsemriNo { get; set; }
    }
    public class GetProductionsQueryHandler : IRequestHandler<GetProductionsQuery, IDataResult<List<GetProducedDto>>>
    {
        IProductionRepository _productionRepository;
        IMapper _mapper;

        public GetProductionsQueryHandler(IProductionRepository productionRepository, IMapper mapper)
        {
            _productionRepository = productionRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<GetProducedDto>>> Handle(GetProductionsQuery request, CancellationToken cancellationToken)
        {
            if (request.LotNo != null)
            {
                var result = await _productionRepository.GetListAsync(x => x.LotNo == request.LotNo);
                var mappedResult = _mapper.Map<List<GetProducedDto>>(result);
                return new SuccessDataResult<List<GetProducedDto>>(mappedResult);
            }
            if (request.MachineId != null)
            {
                var result = await _productionRepository.GetListAsync(x => x.MakId == request.MachineId);
                var mappedResult = _mapper.Map<List<GetProducedDto>>(result);
                return new SuccessDataResult<List<GetProducedDto>>(mappedResult);
            }
            if (request.Type != null)
            {
                var result = await _productionRepository.GetListAsync(x => x.UretTip == request.Type);
                var mappedResult = _mapper.Map<List<GetProducedDto>>(result);
                return new SuccessDataResult<List<GetProducedDto>>(mappedResult);
            }
            if (request.IsemriNo != null)
            {
                var result = await _productionRepository.GetListAsync(x => x.IsemriNo == request.IsemriNo);
                var mappedResult = _mapper.Map<List<GetProducedDto>>(result);
                return new SuccessDataResult<List<GetProducedDto>>(mappedResult);
            }

            throw new CustomException("Arama Yapılacak Alan Boş Olamaz!", System.Net.HttpStatusCode.NotFound);
        }
    }
}
