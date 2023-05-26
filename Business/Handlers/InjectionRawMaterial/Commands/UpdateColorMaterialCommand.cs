using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Uretim;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.InjectionRawMaterial.Commands
{
    public class UpdateColorMaterialCommand : IRequest<IDataResult<EryBoyaSeri>>
    {
        public EryBoyaSeri Varil { get; set; }
    }
    public class UpdateColorMaterialCommandHandler : IRequestHandler<UpdateColorMaterialCommand, IDataResult<EryBoyaSeri>>
    {
        IEnjeksiyonBoyaRepository _boyaRepository;

        public UpdateColorMaterialCommandHandler(IEnjeksiyonBoyaRepository boyaRepository)
        {
            _boyaRepository = boyaRepository;
        }

        public async Task<IDataResult<EryBoyaSeri>> Handle(UpdateColorMaterialCommand request, CancellationToken cancellationToken)
        {

            var varil = await _boyaRepository.UpdateAsync(request.Varil);

            return new SuccessDataResult<EryBoyaSeri>(varil);
        }
    }
}
