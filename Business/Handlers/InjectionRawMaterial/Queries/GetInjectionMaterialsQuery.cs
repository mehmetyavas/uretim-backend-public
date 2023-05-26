using AutoMapper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Uretim;
using Entities.Dtos.Production.enjeksiyon;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.InjectionRawMaterial.Queries
{
    public class GetInjectionMaterialsQuery : IRequest<IDataResult<List<EnjeksiyonHammaddeDto>>>
    {
        public int UretId { get; set; }
    }
    public class GetInjectionMaterialsQueryHandler : IRequestHandler<GetInjectionMaterialsQuery, IDataResult<List<EnjeksiyonHammaddeDto>>>
    {
        IEnjeksiyonRepository _enjeksiyonRepository;
        IMapper _mapper;

        public GetInjectionMaterialsQueryHandler(IEnjeksiyonRepository enjeksiyonRepository, IMapper mapper)
        {
            _enjeksiyonRepository = enjeksiyonRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<EnjeksiyonHammaddeDto>>> Handle(GetInjectionMaterialsQuery request, CancellationToken cancellationToken)
        {
            var result = await _enjeksiyonRepository.GetListAsync(x => x.UretId == request.UretId);
            var mappedResult = _mapper.Map<List<EnjeksiyonHammaddeDto>>(result);

            return new SuccessDataResult<List<EnjeksiyonHammaddeDto>>(mappedResult);
        }
    }
}
