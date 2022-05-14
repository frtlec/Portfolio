using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Porfolio.Services.Setting.API.Models.DbModels
{
  public class CommonDbModel
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int CreatedUserId { get; set; }
    public int UpdatedUserId { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? CreatedDate { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? UpdatedDate { get; set; }
  }
}
