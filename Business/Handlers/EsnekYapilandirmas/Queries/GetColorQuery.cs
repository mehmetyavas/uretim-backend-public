using Business.Constants;
using Business.Constants.Product;
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

namespace Business.Handlers.EsnekYapilandirmas.Queries
{
    public class GetColorQuery : IRequest<IDataResult<Esnekyapilandirma>>
    {
        public string StockCode { get; set; }
        public string YapKod { get; set; }

    }
    public class GetColorQueryHandler : IRequestHandler<GetColorQuery, IDataResult<Esnekyapilandirma>>
    {
        IEsnekYapilandirmaRepository _esnekRepository;

        public GetColorQueryHandler(IEsnekYapilandirmaRepository esnekRepository)
        {
            _esnekRepository = esnekRepository;
        }

        public async Task<IDataResult<Esnekyapilandirma>> Handle(GetColorQuery request, CancellationToken cancellationToken)
        {
            var getColorRecord = await _esnekRepository
               .GetListAsync(x =>
                   x.Yapkod == request.YapKod &&
                   x.StokKodu == request.StockCode);

            if (!getColorRecord.Any())
                return new ErrorDataResult<Esnekyapilandirma>(Messages.WrongYapkod);

            var getColor = getColorRecord.Where(x => x.Ozkod == ProductConsts.BottomColorCode).FirstOrDefault()
                ?? getColorRecord.Where(x => x.Ozkod == ProductConsts.TopColorCode).FirstOrDefault();



            return new SuccessDataResult<Esnekyapilandirma>(data: getColor);

        }
    }
}
