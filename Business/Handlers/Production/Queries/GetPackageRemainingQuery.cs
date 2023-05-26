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

namespace Business.Handlers.Production.Queries
{
    public class GetPackageRemainingQuery : IRequest<List<PackageDto>>
    {
        public string StokKodu { get; set; }
    }
    public class GetPackageRemainingQueryHandler : IRequestHandler<GetPackageRemainingQuery, List<PackageDto>>
    {
        readonly IAssemblyRepository _assemblyRepository;
        readonly IEnjeksiyonRepository _enjeksiyonRepository;
        readonly ISeritraRepository _seritraRepository;

        public GetPackageRemainingQueryHandler(
            IAssemblyRepository assemblyRepository,
            IEnjeksiyonRepository enjeksiyonRepository,
            ISeritraRepository seritraRepository)
        {
            _assemblyRepository = assemblyRepository;
            _enjeksiyonRepository = enjeksiyonRepository;
            _seritraRepository = seritraRepository;
        }

        public async Task<List<PackageDto>> Handle(GetPackageRemainingQuery request, CancellationToken cancellationToken)
        {
            var resultEnjeksiyon = await _enjeksiyonRepository.GetEnjeksiyonPackages(request.StokKodu);

            var resultMontaj = await _assemblyRepository.GetAssemblyPackage(request.StokKodu);

            var resultSeritra = await _seritraRepository.GetPackageRemainingAmount(request.StokKodu);

            //enjeksiyon ve montaj kolilerini bir listeye topluyor
            var resultCombined = resultEnjeksiyon.Concat(resultMontaj)
                    .GroupBy(x => new { x.StokKodu, x.SeriNo })
                    .Select(g => new
                    {
                        g.Key.StokKodu,
                        g.Key.SeriNo,
                        Bakiye = g.Sum(t => t.Bakiye)
                    })
                    .ToList();

            //dictionary'e dönüştürüyor
            var resultSeritraDict = resultSeritra
              .GroupBy(x => new { x.StokKodu, x.SeriNo })
              .ToDictionary(g => new { g.Key.StokKodu, g.Key.SeriNo }, g => g.Sum(b => b.Bakiye));

            //takip verilerini dictionary'e dönüştürüyor
            var resultCombinedDictionary = resultCombined
                .GroupBy(r => new { r.StokKodu, r.SeriNo })
                .ToDictionary(g => new { g.Key.StokKodu, g.Key.SeriNo }, c => c.Sum(r => r.Bakiye));


            var result = resultSeritraDict
                .Select(r => new PackageDto
                {
                    StokKodu = r.Key.StokKodu,
                    SeriNo = r.Key.SeriNo,
                    Bakiye = r.Value - (resultCombinedDictionary.ContainsKey(r.Key) ? resultCombinedDictionary[r.Key] : 0)
                })
                .Where(r => r.Bakiye >= 1)
                .OrderBy(r => r.SeriNo)
                .ToList();

            return result;
        }
    }
}
