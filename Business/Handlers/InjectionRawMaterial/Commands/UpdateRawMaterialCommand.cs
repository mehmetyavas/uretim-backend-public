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
    public class UpdateRawMaterialCommand : IRequest<IDataResult<EryHammaddeVarilseri>>
    {
        public EryHammaddeVarilseri Varil { get; set; }

    }
    public class UpdateRawMaterialCommandHandler : IRequestHandler<UpdateRawMaterialCommand, IDataResult<EryHammaddeVarilseri>>
    {
        IEnjeksiyonHammaddeRepository _hammaddeRepository;

        public UpdateRawMaterialCommandHandler(IEnjeksiyonHammaddeRepository hammaddeRepository)
        {
            _hammaddeRepository = hammaddeRepository;
        }

        public async Task<IDataResult<EryHammaddeVarilseri>> Handle(UpdateRawMaterialCommand request, CancellationToken cancellationToken)
        {
            var varils = await _hammaddeRepository.UpdateAsync(request.Varil);

            return new SuccessDataResult<EryHammaddeVarilseri>(varils);
        }
    }
}
