using System.ComponentModel.DataAnnotations;

namespace Petminder_RestApi.Models
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }
        [Required]
        public string name { get; set; }
        public int age  { get; set; }
        public double weight { get; set; }
        public string gender { get; set; }
        public string type { get; set; }
        public string breed { get; set; }
    }
}