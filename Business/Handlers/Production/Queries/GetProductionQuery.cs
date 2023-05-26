using Amazon.Runtime.Internal;
using Core.Extensions;
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

namespace Business.Handlers.Production.Queries
{
    public class GetProductionQuery : IRequest<IDataResult<RpUretimSeri>>
    {
        public int? Id { get; set; }
        public string? SeriNo { get; set; }
        public string? Ciid { get; set; }
    }
    public class GetProductionQueryHandler : IRequestHandler<GetProductionQuery, IDataResult<RpUretimSeri>>
    {
        IProductionRepository _productionRepository;

        public GetProductionQueryHandler(IProductionRepository productionRepository)
        {
            _productionRepository = productionRepository;
        }

        public async Task<IDataResult<RpUretimSeri>> Handle(GetProductionQuery request, CancellationToken cancellationToken)
        {
            if (request.Id != null)
            {
                var result = await _productionRepository.GetAsync(x => x.Id == request.Id);
                return new SuccessDataResult<RpUretimSeri>(result);
            }
            if (request.SeriNo != null)
            {
                var result = await _productionRepository.GetAsync(x => x.SeriNo == request.SeriNo);
                return new SuccessDataResult<RpUretimSeri>(result);
            }

            if (request.Ciid != null)
            {
                var result = await _productionRepository.GetAsync(x => x.Ciid == request.Ciid.ToString());
                return new SuccessDataResult<RpUretimSeri>(result);
            }
            throw new CustomException("Arama Yapılacak Alan Boş Olamaz!", System.Net.HttpStatusCode.NotFound);
        }
    }
}
