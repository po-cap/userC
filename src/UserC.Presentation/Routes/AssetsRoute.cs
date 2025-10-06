using Microsoft.AspNetCore.Mvc;
using Shared.Mediator.Interface;
using UserC.Application.Commands.Assets;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Routes;

public static class AssetsRoute
{
    public static void MapAssets(this WebApplication app)
    {
        app.MapPost("/api/image",ImageAsync).DisableAntiforgery();
        app.MapPost("/api/video",VideoAsync).DisableAntiforgery();
    }
    
    private static async Task<IResult> ImageAsync( 
        [FromServices]IHttpContextAccessor httpContextAccessor,
        [FromServices]IMediator mediator,
        [FromForm] IFormFile file,
        [FromQuery] bool? temporary)
    {
        var context = httpContextAccessor.HttpContext;
        if (context == null) throw new Exception("Please register HttpContextAccessor");
        
        var stream = new MemoryStream();
        await file.CopyToAsync(stream);

        var ext = Path.GetExtension(file.FileName);

        var command = new UploadImageCommand()
        {
            UserId = context.UserID(),
            Temporary = temporary ?? false,
            File = stream,
            FileExt = ext,
        };
        
        var media = await mediator.SendAsync(command);
        return Results.Ok(media);
    }

    private static async Task<IResult> VideoAsync( 
        [FromServices]IHttpContextAccessor httpContextAccessor,
        [FromServices]IMediator mediator,
        [FromForm] IFormFile video,
        [FromForm] IFormFile thumbnail,
        [FromQuery] bool? temporary)
    {
        var context = httpContextAccessor.HttpContext;
        if (context == null) throw new Exception("Please register HttpContextAccessor");
        
        var videoStream = new MemoryStream();
        await video.CopyToAsync(videoStream);
        var videoExt = Path.GetExtension(video.FileName);
        
        var thumbnailStream = new MemoryStream();
        await thumbnail.CopyToAsync(thumbnailStream);
        var thumbnailExt = Path.GetExtension(thumbnail.FileName);

        var command = new UploadVideoCmd()
        {
            UserId = context.UserID(),
            Temporary = temporary ?? false,
            Video = videoStream,
            VideoFileExt = videoExt,
            Thumbnail = thumbnailStream,
            ThumbnailFileExt = thumbnailExt,
        };
        
        var media = await mediator.SendAsync(command);
        return Results.Ok(media);
    }
}