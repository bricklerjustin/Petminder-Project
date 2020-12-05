using System;
using System.ComponentModel.DataAnnotations;

namespace Petminder_RestApi.Dtos
{
    public class FavoriteUpdateCreateBaseDto
    {
        public Guid AccountId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }
        public string LocationType { get; set; }
        public string UrlType { get; set; }
    }
}