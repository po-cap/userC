using Microsoft.Extensions.Logging;
using Po.Api.Response;
using Po.Media;
using Shared.Mediator.Interface;

namespace UserC.Application.Commands.Assets;

public class UploadVideoCmd: IRequest<Media>, IDisposable
{
    /// <summary> 
    /// 照片檔案
    /// </summary>
    public MemoryStream File { get; set; } = new ();

    /// <summary>
    /// 檔案 extensions
    /// </summary>
    public required string FileExt { get; set; }
    
    public void Dispose()
    {
        File.Dispose();
    }
}

public class UploadVideoHandler : IRequestHandler<UploadVideoCmd, Media>
{
    private readonly IMediaService _mediaService;
    private readonly ILogger<UploadVideoHandler> _logger;

    public UploadVideoHandler(IMediaService mediaService, ILogger<UploadVideoHandler> logger)
    {
        _mediaService = mediaService;
        _logger = logger;
    }
    
    public async Task<Media> HandleAsync(UploadVideoCmd request)
    {
        try
        {
            var media = await _mediaService.UploadAsync(request.File, new UploadOption()
            {
                Type = MediaType.mp4,
                Directory = "videos",
                Name = $"tmp/{Guid.NewGuid()}{request.FileExt}"
            });

            return media;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw Failure.BadRequest();
        }
        
    }
}