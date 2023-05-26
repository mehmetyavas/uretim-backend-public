using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Netsis.stsabit.Queries
{
    public class GetBagOrPackageQuery : IRequest<IDataResult<List<string>>>
    {
        public string Code1 { get; set; }
    }

    public class GetBagOrPackageQueryHandler : IRequestHandler<GetBagOrPackageQuery, IDataResult<List<string>>>
    {
        IStSabitRepository _sabitRepository;

        public GetBagOrPackageQueryHandler(IStSabitRepository sabitRepository)
        {
            _sabitRepository = sabitRepository;
        }

        public async Task<IDataResult<List<string>>> Handle(GetBagOrPackageQuery request, CancellationToken cancellationToken)
        {
            var result = await _sabitRepository.GetStockCodeListAsync(request.Code1);

            return new SuccessDataResult<List<string>>(result.ToList());
        }
    }
}
