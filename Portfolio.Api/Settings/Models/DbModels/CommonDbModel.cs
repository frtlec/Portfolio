using System;

namespace Portfolio.Api.Settings.Models.DbModels
{
  public class CommonDbModel
  {
    public string Id { get; set; }
    public int CreatedUserId { get; set; }
    public int UpdatedUserId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
  }
}
