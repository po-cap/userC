using Po.Media;
using Shared.Mediator.Interface;

namespace UserC.Application.Commands.Assets;

public class UploadImageCmd : IRequest<Media>, IDisposable
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


public class UploadImageHandler : IRequestHandler<UploadImageCmd, Media>
{
    private readonly IMediaService _mediaService;

    public UploadImageHandler(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }


    public async Task<Media> HandleAsync(UploadImageCmd request)
    {
        var media = await _mediaService.UploadAsync(request.File, new UploadOption()
        {
            Type = MediaType.image,
            Directory = $"{DateTime.Today:yyyy/MM/dd}/images",
            Name =  $"tmp/{Guid.NewGuid()}{request.FileExt}"
        });

        return media;
    }
}