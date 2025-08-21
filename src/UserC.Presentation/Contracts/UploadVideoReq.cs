using UserC.Application.Commands.Assets;

namespace UserC.Presentation.Contracts;

public class UploadVideoReq
{
    public required IFormFile Video { get; set; }

    public required IFormFile Thumbnail { get; set; }
}

public static partial class ContractExtension
{
    public static UploadVideoCmd ToCommand(this UploadVideoReq request)
    {
        var videoStream = new MemoryStream();
        request.Video.CopyTo(videoStream);
        var videoExt = Path.GetExtension(request.Video.FileName);
        
        var thumbnailStream = new MemoryStream();
        request.Video.CopyTo(videoStream);
        var thumbnailExt = Path.GetExtension(request.Thumbnail.FileName);

        return new UploadVideoCmd()
        {
            Video = videoStream,
            VideoFileExt = videoExt,
            Thumbnail = thumbnailStream,
            ThumbnailFileExt = thumbnailExt
        };
    }
}