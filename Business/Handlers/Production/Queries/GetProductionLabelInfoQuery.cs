using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Uretim;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Production.Queries
{
    public class GetProductionLabelInfoQuery : IRequest<IDataResult<RpEtiketTbl>>
    {
        public string SerialNo { get; set; }
        public int? ProductType { get; set; }
    }

    public class GetProductionLabelInfoQueryHandler : IRequestHandler<GetProductionLabelInfoQuery, IDataResult<RpEtiketTbl>>
    {
        Peksan23Context _context;

        public GetProductionLabelInfoQueryHandler(Peksan23Context context)
        {
            _context = context;
        }

        public async Task<IDataResult<RpEtiketTbl>> Handle(GetProductionLabelInfoQuery request, CancellationToken cancellationToken)
        {
            if (request.ProductType == 1)
            {
                var sampleResult = await _context
               .Set<RpEtiketTbl>()
               .FromSqlInterpolated($"EXEC _RP_ETIKET_NUMUNE {request.SerialNo}")
               .ToListAsync();


                return new SuccessDataResult<RpEtiketTbl>(sampleResult.FirstOrDefault());
            }

            var result = await _context
                .Set<RpEtiketTbl>()
                .FromSqlInterpolated($"EXEC _RP_ETIKET {request.SerialNo}")
                .ToListAsync();


            return new SuccessDataResult<RpEtiketTbl>(result.FirstOrDefault());

        }
    }
}
