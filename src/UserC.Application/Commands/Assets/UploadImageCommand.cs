using Po.Media;
using Shared.Mediator.Interface;
using UserC.Application.Services;

namespace UserC.Application.Commands.Assets;

public class UploadImageCommand : IRequest<Media>, IDisposable
{
    /// <summary>
    /// 使用者 ID
    /// </summary>
    public long UserId { get; set; }
    
    /// <summary>
    /// 是否是暫時性的
    /// </summary>
    public bool Temporary { get; set; }
    
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


public class UploadImageHandler : IRequestHandler<UploadImageCommand, Media>
{
    private readonly IMediaService _mediaService;
    private readonly Snowflake _snowflake;

    public UploadImageHandler(
        IMediaService mediaService,
        Snowflake snowflake)
    {
        _mediaService = mediaService;
        _snowflake = snowflake;
    }


    public async Task<Media> HandleAsync(UploadImageCommand request)
    {
        string directory;
        string name;

        if (request.Temporary)
        {
            directory = $"{DateTime.Today:yyyy-MM-dd}";
            name = $"image/{_snowflake.Get()}{request.FileExt}";
        }
        else
        {
            directory = "image";
            name = $"{request.UserId}/{_snowflake.Get()}{request.FileExt}";
        }
        
        var media = await _mediaService.UploadAsync(request.File, new UploadOption()
        {
            Type = MediaType.image,
            Directory = directory,
            Name =  name
        });

        return media;
    }
}