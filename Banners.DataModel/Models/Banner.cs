using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Banners.DataModel.Models
{
    public class Banner
    {
        public int Id { get; set; }
        public string Html { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Created { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Modified { get; set; }
    }
}
