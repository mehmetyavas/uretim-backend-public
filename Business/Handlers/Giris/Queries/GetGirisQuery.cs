using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Giris;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Machines.Queries
{
    public class GetGirisQuery : IRequest<IDataResult<GirisDTO>>
    {

    }
    public class GetMachinesQueryHandler : IRequestHandler<GetGirisQuery, IDataResult<GirisDTO>>
    {
        IMachineRepository _machineRepository;
        IStaffRepository _staffRepository;

        public GetMachinesQueryHandler(
            IMachineRepository machineRepository,
            IStaffRepository staffRepository)
        {
            _machineRepository = machineRepository;
            _staffRepository = staffRepository;
        }
        [CacheAspect(60)]
        public async Task<IDataResult<GirisDTO>> Handle(GetGirisQuery request, CancellationToken cancellationToken)
        {

            var machines = await _machineRepository.GetListAsync();
            var staffs = await _staffRepository.GetListAsync();
            var girisDto = new GirisDTO
            {
                Machines = machines.ToList(),
                Staffs = staffs.ToList()
            };

            return new SuccessDataResult<GirisDTO>(girisDto);
        }
    }
}
