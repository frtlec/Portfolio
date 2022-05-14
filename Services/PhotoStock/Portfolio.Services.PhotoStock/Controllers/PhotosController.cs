using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Services.PhotoStock.Dtos;
using Portfolio.Shared.ControllerBases;
using Portfolio.Shared.Dtos;
using Portfolio.Shared.Helper;
using System;
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
    [Authorize(Policy = "WriteEditWork")]
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

        return CreateActionResultInstance(Response<PhotoDto>.Fail("error:" + ex.Message, 500));
      }

    }
    [HttpPost("PhotoSaveWithPName")]
    [Authorize(Policy = "WriteEditWork")]
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
          if (image.Width > acceptedWith)
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

        return CreateActionResultInstance(Response<PhotoDto>.Fail("error:" + ex.Message, 500));
      }

    }
    [HttpPost("PhotoSquareSave")]
    [Authorize(Policy = "WriteEditWork")]
    public async Task<IActionResult> PhotoSquareSave(IFormFile photo, CancellationToken cancellationToken)
    {
      try
      {
        if (photo == null || photo.Length < 1)
        {
          return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty", 400));
        }

        using (var image = Image.FromStream(photo.OpenReadStream()))
        {
          if (image.Width != image.Height)
          {
            return CreateActionResultInstance(Response<PhotoDto>.Fail("Only square photo", 400));
          }
        }

        string name = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName); ;

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", name);
        using var stream = new FileStream(path, FileMode.Create);

        await photo.CopyToAsync(stream, cancellationToken);

        var returnPath = name;

        PhotoDto photoDto = new() { Url = returnPath };

        return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
      }
      catch (System.Exception ex)
      {

        return CreateActionResultInstance(Response<PhotoDto>.Fail("error:" + ex.Message, 500));
      }

    }
    [HttpPost("PhotoSaveRectangle")]
    [Authorize(Policy = "WriteEditWork")]
    public async Task<IActionResult> PhotoSaveRectangle(IFormFile photo, CancellationToken cancellationToken)
    {
      try
      {
        if (photo == null || photo.Length < 1)
        {
          return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty", 400));
        }
        using (var image = Image.FromStream(photo.OpenReadStream()))
        {
          short acceptedWith = 2160;
          if (image.Width > acceptedWith)
          {
            return CreateActionResultInstance(Response<PhotoDto>.Fail($"Maximum limit for photo width exceeded.Accepted {acceptedWith}", 400));
          }
        }
        string photoName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName); ;


        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoName);
        using var stream = new FileStream(path, FileMode.Create);
        await photo.CopyToAsync(stream, cancellationToken);


        PhotoDto photoDto = new() { Url = photoName };

        return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
      }
      catch (System.Exception ex)
      {

        return CreateActionResultInstance(Response<PhotoDto>.Fail("error:" + ex.Message, 500));
      }

    }
    [HttpPost("PhotoSquareSaveWithPName")]
    [Authorize(Policy = "WriteEditWork")]
    public async Task<IActionResult> PhotoSquareSave([FromForm] PhotoSquareSaveDto dto, CancellationToken cancellationToken)
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

        return CreateActionResultInstance(Response<PhotoDto>.Fail("error:" + ex.Message, 500));
      }

    }
    [HttpDelete("PhotoDelete/{photoUrl}")]
    [Authorize(Policy = "ReadAndWrite")]
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

    [HttpPost("SvgSave")]
    [Authorize(Policy = "ReadAndWrite")]
    public async Task<IActionResult> SvgSaveAsync(IFormFile svgFile, CancellationToken cancellationToken)
    {
      try
      {
        if (svgFile == null || svgFile.Length < 1)
        {
          return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty", 400));
        }

        if (Path.GetExtension(svgFile.FileName)!=".svg")
        {
          return CreateActionResultInstance(Response<PhotoDto>.Fail("only svg file", 400));
        }


        string photoName = Guid.NewGuid().ToString() + Path.GetExtension(svgFile.FileName); ;


        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/svg", photoName);
        using var stream = new FileStream(path, FileMode.Create);
        await svgFile.CopyToAsync(stream, cancellationToken);


        PhotoDto photoDto = new() { Url = photoName };

        return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
      }
      catch (System.Exception ex)
      {

        return CreateActionResultInstance(Response<PhotoDto>.Fail("error:" + ex.Message, 500));
      }
    }
  }
}
