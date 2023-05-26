using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Netsis;
using Entities.Dtos.Netsis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Netsis.Sthar.Queries
{
    public class GetStharRecordsByReceiptQuery : IRequest<IDataResult<List<Tblsthar>>>
    {
        public string FisNo { get; set; }
    }
    public class GetStharRecordsByReceiptQueryHandler : IRequestHandler<GetStharRecordsByReceiptQuery, IDataResult<List<Tblsthar>>>
    {
        IStharRepository _stharRepository;

        public GetStharRecordsByReceiptQueryHandler(IStharRepository stharRepository)
        {
            _stharRepository = stharRepository;
        }

        public async Task<IDataResult<List<Tblsthar>>> Handle(GetStharRecordsByReceiptQuery request, CancellationToken cancellationToken)
        {
            var record = await _stharRepository.GetListAsync(x => x.Fisno == request.FisNo);

            return new SuccessDataResult<List<Tblsthar>>(record.ToList());
        }
    }
}
