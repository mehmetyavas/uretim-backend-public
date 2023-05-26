using Business.Constants;
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

namespace Business.Handlers.Production.Commands
{
    public class UpdateProductionCommand : IRequest<IResult>
    {
        public RpUretimSeri RpUretimSeri { get; set; }
    }
    public class UpdateProductionCommandHandler : IRequestHandler<UpdateProductionCommand, IResult>
    {
        IProductionRepository _productionRepository;

        public UpdateProductionCommandHandler(IProductionRepository productionRepository)
        {
            _productionRepository = productionRepository;
        }

        public async Task<IResult> Handle(UpdateProductionCommand request, CancellationToken cancellationToken)
        {

            _productionRepository.Update(request.RpUretimSeri);
            await _productionRepository.SaveChangesAsync();
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
