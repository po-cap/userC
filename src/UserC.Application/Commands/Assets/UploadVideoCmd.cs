using Microsoft.Extensions.Logging;
using Po.Api.Response;
using Po.Media;
using Shared.Mediator.Interface;
using UserC.Application.Models;
using UserC.Application.Services;

namespace UserC.Application.Commands.Assets;

public class UploadVideoCmd: IRequest<VideoModel>, IDisposable
{
    /// <summary>
    /// 使用者 ID
    /// </summary>
    public long UserId { get; set; }
    
    /// <summary>
    /// 是否是暫時性的
    /// </summary>
    public required bool Temporary { get; set; }
    
    /// <summary> 
    /// 影片檔案
    /// </summary>
    public MemoryStream Video { get; set; } = new ();

    /// <summary>
    /// 封面照
    /// </summary>
    public MemoryStream Thumbnail { get; set; } = new();

    /// <summary>
    /// 影片 extensions
    /// </summary>
    public required string VideoFileExt { get; set; }
    
    /// <summary>
    /// 封面照 extensions
    /// </summary>
    public required string ThumbnailFileExt { get; set; }
    
    public void Dispose()
    {
        Video.Dispose();
        Thumbnail.Dispose();
    }
}

public class UploadVideoHandler : IRequestHandler<UploadVideoCmd, VideoModel>
{
    private readonly Snowflake _snowflake;
    private readonly IMediaService _mediaService;
    private readonly ILogger<UploadVideoHandler> _logger;

    public UploadVideoHandler(
        Snowflake snowflake, 
        IMediaService mediaService, 
        ILogger<UploadVideoHandler> logger)
    {
        _mediaService = mediaService;
        _logger = logger;
        _snowflake = snowflake;
    }
    
    public async Task<VideoModel> HandleAsync(UploadVideoCmd request)
    {
        try
        {
            var id = _snowflake.Get();
            string directoryForThumbnail, directoryForVideo;
            string nameForThumbnail, nameForVideo;

            if (request.Temporary)
            {
                directoryForThumbnail = $"{DateTime.Today:yyyy-MM-dd}";
                directoryForVideo = $"{DateTime.Today:yyyy-MM-dd}";
                nameForThumbnail = $"video/{id}{request.ThumbnailFileExt}";
                nameForVideo = $"video/{id}{request.VideoFileExt}";
            }
            else
            {
                directoryForThumbnail = $"video";
                directoryForVideo = $"video";
                nameForThumbnail = $"{request.UserId}/{id}{request.ThumbnailFileExt}";
                nameForVideo = $"{request.UserId}/{id}{request.VideoFileExt}";
            }
            
            var videoMedia = await _mediaService.UploadAsync(request.Video, new UploadOption()
            {
                Type = MediaType.mp4,
                Directory = directoryForVideo,
                Name = nameForVideo
            });
            
            var thumbnailMedia = await _mediaService.UploadAsync(request.Thumbnail, new UploadOption()
            {
                Type = MediaType.image,
                Directory = directoryForThumbnail,
                Name = nameForThumbnail
            });

            return new VideoModel()
            {
                Video = videoMedia,
                Thumbnail = thumbnailMedia
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw Failure.BadRequest();
        }
        
    }
}