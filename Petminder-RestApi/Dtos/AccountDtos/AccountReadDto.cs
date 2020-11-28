using System;

namespace Petminder_RestApi.Dtos
{
    public class AccountReadDto
    {
        public Guid Id { get; set; }
        public string ApiKey {get; set;}
    }
}