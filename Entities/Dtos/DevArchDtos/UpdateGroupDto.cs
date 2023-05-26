using Core.Entities;

namespace Entities.Dtos.DevArchDtos
{
    public class UpdateGroupDto : IDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
    }
}