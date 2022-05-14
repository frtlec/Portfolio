using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Porfolio.Services.Setting.API.Models.DbModels
{
  public class AboutPage: CommonAbout
  {
    public string PortreFileName { get; set; }
    public string Slogan { get; set; }
    public string Summary { get; set; }
    public List<AboutSoftware> Softwares { get; set; }
    public List<AboutBusiness> Businesses { get; set; }
    public List<AboutEducation> Educations { get; set; }
    public List<AboutCertifacate> Certifacates { get; set; }
    public List<AboutProject> Projects { get; set; }
  }
  public class AboutSoftware : CommonAboutAggregate
  {
    public string SvgPath { get; set; }
    public string SoftwareName { get; set; }
  }
  public class AboutBusiness : CommonAboutAggregate
  {
    public string Head { get; set; }
    public string Value { get; set; }
    public string Foot { get; set; }
  }
  public class AboutEducation : CommonAboutAggregate
  {
    public string Head { get; set; }
    public string Value { get; set; }
    public string Foot { get; set; }
  }
  public class AboutCertifacate : CommonAboutAggregate
  {
    public string Head { get; set; }
    public string Value { get; set; }
  }
  public class AboutProject: CommonAboutAggregate
  {
    public string Head { get; set; }
    public string Value { get; set; }
    public string Link { get; set; }
  }
  public class CommonAboutAggregate
  {
    public Guid RowId { get; set; }
    public bool Active { get; set; }
  }
  public class CommonAbout
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int CreatedUserId { get; set; }
    public bool? Active { get; set; }
    public int UpdatedUserId { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? CreatedDate { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? UpdatedDate { get; set; }
  }
}
