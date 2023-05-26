using Business.Handlers.Netsis.IsemriRec;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.AssemblyRawMaterial.Queries
{
    public class GetAssemblyProductQuery : IRequest<IDataResult<string>>
    {
        public string SerialNo { get; set; }
        public string WorkOrder { get; set; }
    }
    public class GetAssemblyProductQueryHandler : IRequestHandler<GetAssemblyProductQuery, IDataResult<string>>
    {
        IMediator _mediator;
        ISeritraRepository _seritraRepository;
        IStharRepository _stharRepository;
        IStokListeRepository _stokListeRepository;

        public GetAssemblyProductQueryHandler(
            IStokListeRepository stokListeRepository,
            IMediator mediator,
            ISeritraRepository seritraRepository,
            IStharRepository stharRepository)
        {
            _stokListeRepository = stokListeRepository;
            _mediator = mediator;
            _seritraRepository = seritraRepository;
            _stharRepository = stharRepository;
        }

        public async Task<IDataResult<string>> Handle(GetAssemblyProductQuery request, CancellationToken cancellationToken)
        {
            //GetIsemriRecQuery
            


            throw new NotImplementedException();
        }
    }
}
