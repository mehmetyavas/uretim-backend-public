using Core.Entities;

namespace Entities.Dtos.DevArchDtos
{
    public class UpdateGroupClaimDto : IDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int[] ClaimIds { get; set; }
    }
}