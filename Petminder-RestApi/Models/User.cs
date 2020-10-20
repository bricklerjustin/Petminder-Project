using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace Petminder_RestApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string ApiKey { get; set; }
        public string GoogleAuth {get; set;}

        [ForeignKey("AccountId")]
        public virtual Account Account {get; set; }
    }
}