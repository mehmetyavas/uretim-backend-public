using Business.Helpers;
using DataAccess.Abstract;
using Entities.Concrete.Uretim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Production.Rules
{
    public class ProductionControlClass
    {

        IProductionRepository _productionRepository;
        IMachineRepository _machineRepository;
        IEsnekYapilandirmaRepository _esnekYapilandirmaRepository;
        public ProductionControlClass(IProductionRepository productionRepository,
            IMachineRepository machineRepository,
            IEsnekYapilandirmaRepository esnekYapilandirmaRepository)
        {
            _productionRepository = productionRepository;
            _machineRepository = machineRepository;
            _esnekYapilandirmaRepository = esnekYapilandirmaRepository;
        }

        public async Task<string> GenerateNewSerialNo(RpUretimSeri mappedReq,string code)
        {
            //database'deki o makine koduyla eşleşen son kayıt edilmiş seri numarası
            var lasttSerialNo = await _productionRepository.GetLastSerialNumber(mappedReq.MakId);
            


            // yeni seri numarası oluşturup Map'lenen result'un içine atılıyor
            return GenerateSerialNo
                .GenerateProductSerialNumber(code!, lasttSerialNo);
        }

        public async Task<bool> CheckColorIsTransParent(string yapKod)
        {
            var color = await _esnekYapilandirmaRepository
                .GetAsync(x =>
                    x.Yapkod == yapKod &&
                    (x.Ozkod == "1" ||
                     x.Ozkod == "2") &&
                     x.Degeracik.StartsWith("Şeffa"));

            return color == null ? false : true;
        }
    }
}
