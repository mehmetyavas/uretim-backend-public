using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Netsis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Netsis.Seritra.Queries
{
    public class GetStockRemainingAmountQuery : IRequest<IDataResult<List<PackageDto>>>
    {
        public string StokKodu { get; set; }
        public string SeriNo { get; set; }
        public int DepoKod { get; set; }
    }
    public class MyClassGetStockRemainingAmountQueryHandler : IRequestHandler<GetStockRemainingAmountQuery, IDataResult<List<PackageDto>>>
    {
        ISeritraRepository _seritraRepository;

        public MyClassGetStockRemainingAmountQueryHandler(ISeritraRepository seritraRepository)
        {
            _seritraRepository = seritraRepository;
        }

        public async Task<IDataResult<List<PackageDto>>> Handle(GetStockRemainingAmountQuery request, CancellationToken cancellationToken)
        {
            var result = await _seritraRepository.GetStockRemainingAmount(request.StokKodu, request.DepoKod, request.SeriNo);

            if (result.Count == 0)
                return new ErrorDataResult<List<PackageDto>>(Messages.NotEnoughRawMaterial + "-" + request.StokKodu);

            return new SuccessDataResult<List<PackageDto>>(result);
        }
    }
}
