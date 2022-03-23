using Microsoft.AspNetCore.Http;

namespace Portfolio.Services.PhotoStock.Dtos
{
    public class PhotoWithTitleSaveDto
    {
        public string Title { get; set; }
        public IFormFile Image { get; set; }

    }
}
