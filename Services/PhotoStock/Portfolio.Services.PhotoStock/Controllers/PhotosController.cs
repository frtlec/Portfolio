using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Services.PhotoStock.Dtos;
using Portfolio.Shared.ControllerBases;
using Portfolio.Shared.Dtos;
using Portfolio.Shared.Helper;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost("PhotoSave")]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            try
            {
                if (photo == null || photo.Length < 1)
                {
                    return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty", 400));
                }
            
                string name = photo.FileName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);

                var returnPath = name;

                PhotoDto photoDto = new() { Url = returnPath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }
            catch (System.Exception ex)
            {

                return CreateActionResultInstance(Response<PhotoDto>.Fail("error", 500));
            }

        }
        [HttpPost("PhotoSaveWithPName")]
        public async Task<IActionResult> PhotoSaveWithPName([FromForm] PhotoWithTitleSaveDto dto, CancellationToken cancellationToken)
        {
            try
            {
                if (dto.Image == null || dto.Image.Length < 1)
                {
                    return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty", 400));
                }
                using (var image = Image.FromStream(dto.Image.OpenReadStream()))
                {
                    short acceptedWith = 2160;
                    if (image.Width> acceptedWith)
                    {
                        return CreateActionResultInstance(Response<PhotoDto>.Fail($"Maximum limit for photo width exceeded.Accepted {acceptedWith}", 400));
                    }
                }
                string photoName = StringConverter.ConvertSpaceToDashAndTrToEnd(dto.Title) + Path.GetExtension(dto.Image.FileName); ;


                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoName);
                using var stream = new FileStream(path, FileMode.Create);
                await dto.Image.CopyToAsync(stream, cancellationToken);


                PhotoDto photoDto = new() { Url = photoName };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }
            catch (System.Exception ex)
            {

                return CreateActionResultInstance(Response<PhotoDto>.Fail("error", 500));
            }

        }
        [HttpPost("PhotoSquareSave")]
        public async Task<IActionResult> PhotoSquareSave([FromForm]PhotoSquareSaveDto dto,CancellationToken cancellationToken)
        {
            try
            {
                if (dto.Image == null || dto.Image.Length < 1)
                {
                    return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty", 400));
                }

                using (var image = Image.FromStream(dto.Image.OpenReadStream()))
                {
                    if (image.Width != image.Height)
                    {
                        return CreateActionResultInstance(Response<PhotoDto>.Fail("Only square photo", 400));
                    }
                }

                string name = StringConverter.ConvertSpaceToDashAndTrToEnd(dto.Title) + Path.GetExtension(dto.Image.FileName); ;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", name);
                using var stream = new FileStream(path, FileMode.Create);
               
                await dto.Image.CopyToAsync(stream, cancellationToken);

                var returnPath = name;

                PhotoDto photoDto = new() { Url = returnPath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }
            catch (System.Exception ex)
            {

                return CreateActionResultInstance(Response<PhotoDto>.Fail("error", 500));
            }

        }
        [HttpDelete("PhotoDelete/{photoUrl}")]
        public IActionResult PhotoDelete(string photoUrl)
        {
            if (string.IsNullOrEmpty(photoUrl))
            {
                return CreateActionResultInstance(Response<NoContent>.Success(204));
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
                return CreateActionResultInstance(Response<NoContent>.Fail("photo not found", 404));
            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
