using Core.Entities;

namespace Entities.Dtos.DevArchDtos
{
    public class UpdateUserGroupByGroupIdDto : IDto
    {
        public int GroupId { get; set; }
        public int[] UserIds { get; set; }
    }
}