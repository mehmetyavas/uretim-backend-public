using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Production.Commands
{
    public class DeleteProductionCommand:IRequest<IResult>
    {
        public string SeriNo { get; set; }
    }
    public class DeleteProductionCommandHandler : IRequestHandler<DeleteProductionCommand, IResult>
    {
        IProductionRepository _productionRepository;

        public DeleteProductionCommandHandler(IProductionRepository productionRepository)
        {
            _productionRepository = productionRepository;
        }

        public async Task<IResult> Handle(DeleteProductionCommand request, CancellationToken cancellationToken)
        {
            var getPRoduction = await _productionRepository.GetAsync(x => x.SeriNo == request.SeriNo);

            _productionRepository.Delete(getPRoduction);
            await _productionRepository.SaveChangesAsync();
            return new SuccessResult(Messages.ProductDeleted);
        }
    }
}
