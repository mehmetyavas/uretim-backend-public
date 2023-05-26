using AutoMapper;
using Business.Constants;
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

namespace Business.Handlers.Products.Queries
{
    public class GetMaterialsQuery : IRequest<IDataResult<List<GetProducedMaterialsDto>>>
    {
        public int UretId { get; set; }
    }

    public class GetMaterialsQueryHandler : IRequestHandler<GetMaterialsQuery, IDataResult<List<GetProducedMaterialsDto>>>
    {
        IEnjeksiyonRepository _enjeksiyonRepository;
        IAssemblyRepository _assemblyRepository;
        IMapper _mapper;

        public GetMaterialsQueryHandler(IEnjeksiyonRepository enjeksiyonRepository, IAssemblyRepository assemblyRepository, IMapper mapper)
        {
            _enjeksiyonRepository = enjeksiyonRepository;
            _assemblyRepository = assemblyRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<GetProducedMaterialsDto>>> Handle(GetMaterialsQuery request, CancellationToken cancellationToken)
        {
            var injectionMaterials = await _enjeksiyonRepository.GetListAsync(x => x.UretId == request.UretId);

            if (injectionMaterials.ToList().Count > 0)
            {
                var mappedResult = _mapper.Map<List<GetProducedMaterialsDto>>(injectionMaterials);
                return new SuccessDataResult<List<GetProducedMaterialsDto>>(mappedResult);
            }


            var assemblyMaterials = await _assemblyRepository.GetListAsync(x => x.UretId == request.UretId);

            if (assemblyMaterials.ToList().Count > 0)
            {
                var mappedResult = _mapper.Map<List<GetProducedMaterialsDto>>(assemblyMaterials);
                return new SuccessDataResult<List<GetProducedMaterialsDto>>(mappedResult);
            }


            throw new Exception(Messages.ProductMaterialsListError);

        }
    }
}
