using Core.DataAccess;
using Entities.Concrete.Uretim;
using Entities.Dtos.WorkOrder;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAssemblyRawMRepository:IEntityRepository<RpMontajHmBilgi1>
    {
        Task<AssemblyInfoDTO> GetAssemblyRawMateriaInfo(string isemriNo);
    }
}