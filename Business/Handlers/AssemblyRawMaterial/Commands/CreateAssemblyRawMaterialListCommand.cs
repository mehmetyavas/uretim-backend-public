using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Netsis;
using Entities.Dtos.Production.Assembly;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.AssemblyRawMaterial.Commands
{
    public class CreateAssemblyRawMaterialListCommand : IRequest<IDataResult<GetAssemblyRawMaterialQueryDto>>
    {
        public string SerialNo { get; set; }
    }
    public class CreateAssemblyRawMaterialListCommandHandler : IRequestHandler<CreateAssemblyRawMaterialListCommand, IDataResult<GetAssemblyRawMaterialQueryDto>>
    {
        IStokListeRepository _stokListeRepository;

        public CreateAssemblyRawMaterialListCommandHandler(IStokListeRepository stokListeRepository)
        {
            _stokListeRepository = stokListeRepository;
        }

        public async Task<IDataResult<GetAssemblyRawMaterialQueryDto>> Handle(CreateAssemblyRawMaterialListCommand request, CancellationToken cancellationToken)
        {

            var result = await _stokListeRepository.GetListAsync();

            throw new Exception();
        }
    }
}
