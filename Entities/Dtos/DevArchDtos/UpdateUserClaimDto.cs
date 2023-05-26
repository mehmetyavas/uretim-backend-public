using Core.Entities;

namespace Entities.Dtos.DevArchDtos
{
    public class UpdateUserClaimDto : IDto
    {

        public int UserId { get; set; }
        public int[] ClaimIds { get; set; }
    }
}