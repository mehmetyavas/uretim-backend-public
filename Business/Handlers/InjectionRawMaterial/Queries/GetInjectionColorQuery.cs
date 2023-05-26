using Amazon.Runtime.Internal;
using DataAccess.Abstract;
using Entities.Concrete.Uretim;
using MediatR;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.InjectionRawMaterial.Queries
{
    public class GetInjectionColorQuery : IRequest<List<EryBoyaSeri>>
    {
        public string WorkOrder { get; set; }

    }
    public class GetInjectionColorQueryHandler : IRequestHandler<GetInjectionColorQuery, List<EryBoyaSeri>>
    {
        IEnjeksiyonBoyaRepository _boyaRepository;

        public GetInjectionColorQueryHandler(IEnjeksiyonBoyaRepository boyaRepository)
        {
            _boyaRepository = boyaRepository;
        }

        public async Task<List<EryBoyaSeri>> Handle(GetInjectionColorQuery request, CancellationToken cancellationToken)
        {
            var color = await _boyaRepository
                .GetListAsync(x =>
                    x.Isemrino == request.WorkOrder &&
                    x.StharGcmik > (x.Harcanan == null ? 0 : x.Harcanan));

            return new List<EryBoyaSeri>(color.ToList().OrderBy(x => x.VarilId));
        }
    }
}
