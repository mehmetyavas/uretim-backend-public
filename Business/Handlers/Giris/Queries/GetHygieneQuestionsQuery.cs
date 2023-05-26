using Amazon.Runtime.Internal;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Giris;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Giris.Queries
{
    public class GetHygieneQuestionsQuery : IRequest<IDataResult<List<EryMakineTemizlikSoru>>>
    {
        public int WorkingArea { get; set; }
    }

    public class GetHygieneQuestionsQueryHandler : IRequestHandler<GetHygieneQuestionsQuery, IDataResult<List<EryMakineTemizlikSoru>>>
    {
        
        IHygieneRepository _hygieneRepository;

        public GetHygieneQuestionsQueryHandler(IHygieneRepository hygieneRepository)
        {
            _hygieneRepository = hygieneRepository;
        }
        [CacheAspect(60)]
        public async Task<IDataResult<List<EryMakineTemizlikSoru>>> Handle(GetHygieneQuestionsQuery request, CancellationToken cancellationToken)
        {
            var result = await _hygieneRepository.GetListOfQuestionsAsync(x=>x.CalismaYeri == request.WorkingArea);

            return new SuccessDataResult<List<EryMakineTemizlikSoru>>(result.ToList());
        }
    }
}
