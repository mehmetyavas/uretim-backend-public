using AutoMapper;
using Business.Constants.Product;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Uretim;
using Entities.Dtos.Production;
using Entities.Dtos.Production.Assembly;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.AssemblyRawMaterial.Queries
{
    public class GetAssemblyRawMaterialQuery : IRequest<IDataResult<GetAssemblyRawMaterialQueryDto>>
    {
        public string WorkOrder { get; set; }
    }
    public class GetAssemblyRawMaterialQueryHandle : IRequestHandler<GetAssemblyRawMaterialQuery, IDataResult<GetAssemblyRawMaterialQueryDto>>
    {

        readonly IAssemblyMaterialRepository _materialRepository;
        IMapper _mapper;

        public GetAssemblyRawMaterialQueryHandle(IAssemblyMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<GetAssemblyRawMaterialQueryDto>> Handle(GetAssemblyRawMaterialQuery request, CancellationToken cancellationToken)
        {
            var result = await _materialRepository
                .GetListAsync(x =>
                    x.WorkOrder == request.WorkOrder &&
                    x.Quantity > x.Spent);

            var materialQuery = new GetAssemblyRawMaterialQueryDto
            {
                BodyMaterials = result.Where(x => x.ProductType == ProductConsts.AssemblyMaterialBody).OrderByDescending(x => x.Spent).ToList(),
                TopMaterials = result.Where(x => x.ProductType == ProductConsts.AssemblyMaterialTop).OrderByDescending(x => x.Spent).ToList(),
            };


            return new SuccessDataResult<GetAssemblyRawMaterialQueryDto>(materialQuery);
        }
    }
}
