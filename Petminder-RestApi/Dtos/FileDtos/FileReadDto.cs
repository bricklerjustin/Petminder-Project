using System;

namespace Petminder_RestApi.Dtos
{
    public partial class FileReadDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid PetId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}