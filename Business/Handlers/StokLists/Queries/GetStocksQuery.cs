using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Netsis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.StokLists.Queries
{
    public class GetStocksQuery : IRequest<IDataResult<EryStokListe>>
    {
        public string RawCode { get; set; }
    }
    public class GetStocksQueryHandler : IRequestHandler<GetStocksQuery, IDataResult<EryStokListe>>
    {
        IStokListeRepository _listeRepository;

        public GetStocksQueryHandler(IStokListeRepository listeRepository)
        {
            _listeRepository = listeRepository;
        }

        public async Task<IDataResult<EryStokListe>> Handle(GetStocksQuery request, CancellationToken cancellationToken)
        {
            var result = await _listeRepository.GetAsync(x => x.StokKodu == request.RawCode);
            return new SuccessDataResult<EryStokListe>(result);
        }
    }
}
