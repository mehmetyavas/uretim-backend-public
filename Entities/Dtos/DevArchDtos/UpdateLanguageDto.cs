using Core.Entities;

namespace Entities.Dtos.DevArchDtos
{
    public class UpdateLanguageDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}