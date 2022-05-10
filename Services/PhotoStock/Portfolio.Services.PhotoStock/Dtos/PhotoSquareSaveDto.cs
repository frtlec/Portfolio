using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Services.PhotoStock.Dtos
{
    public class PhotoSquareSaveDto
    {
       public string Title { get; set; }//kaldırıldı
       public IFormFile Image { get; set; }
    }
}
