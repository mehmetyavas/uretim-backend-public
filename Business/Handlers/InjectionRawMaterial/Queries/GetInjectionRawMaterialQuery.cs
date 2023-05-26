using Amazon.Runtime.Internal;
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

namespace Business.Handlers.InjectionRawMaterial.Queries
{
    public class GetInjectionRawMaterialQuery : IRequest<List<EryHammaddeVarilseri>>
    {
        public string WorkOrder { get; set; }
    }
    public class GetInjectionRawMaterialQueryHandler : IRequestHandler<GetInjectionRawMaterialQuery, List<EryHammaddeVarilseri>>
    {


        IEnjeksiyonHammaddeRepository _hammaddeRepository;

        public GetInjectionRawMaterialQueryHandler(IEnjeksiyonHammaddeRepository hammaddeRepository)
        {
            _hammaddeRepository = hammaddeRepository;
        }

        public async Task<List<EryHammaddeVarilseri>> Handle(GetInjectionRawMaterialQuery request, CancellationToken cancellationToken)
        {
            var getBarrels = await _hammaddeRepository
                .GetListAsync(x =>
                    x.Isemrino == request.WorkOrder &&
                    x.StharGcmik > x.Harcanan);

            return new List<EryHammaddeVarilseri>(getBarrels.ToList().OrderBy(x=>x.VarilId));
        }
    }
}
