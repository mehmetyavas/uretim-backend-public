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

namespace Business.Handlers.Prints.Queries
{
    public class GetProductPrintQuery : IRequest<IDataResult<List<PrintLog>>>
    {
        public string? SerialNo { get; set; }
    }
    public class GetProductPrintQueryHandler : IRequestHandler<GetProductPrintQuery, IDataResult<List<PrintLog>>>
    {
        IPrintlogsRepository _printlogsRepository;

        public GetProductPrintQueryHandler(IPrintlogsRepository printlogsRepository)
        {
            _printlogsRepository = printlogsRepository;
        }

        public async Task<IDataResult<List<PrintLog>>> Handle(GetProductPrintQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.SerialNo))
            {
                var allREsults = await _printlogsRepository.GetListAsync();

                return new SuccessDataResult<List<PrintLog>>(allREsults.ToList());
            }
            var getResut = await _printlogsRepository.GetListAsync(x => x.SerialNo == request.SerialNo);

            return new SuccessDataResult<List<PrintLog>>(getResut.ToList());

        }
    }
}
