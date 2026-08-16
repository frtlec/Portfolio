using Microsoft.AspNetCore.Http;

namespace Portfolio.Api.PhotoStock.Dtos
{
    public class PhotoWithTitleSaveDto
    {
        public string Title { get; set; } //kaldırıldı
        public IFormFile Image { get; set; }

    }
}
