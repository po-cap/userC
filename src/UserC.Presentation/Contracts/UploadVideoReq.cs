using UserC.Application.Commands.Assets;

namespace UserC.Presentation.Contracts;

public class UploadVideoReq
{
    public required IFormFile File { get; set; }
}

public static partial class ContractExtension
{
    public static UploadVideoCmd ToCommand(this UploadVideoReq request)
    {
        var stream = new MemoryStream();
        request.File.CopyTo(stream);

        var ext = Path.GetExtension(request.File.FileName);

        return new UploadVideoCmd()
        {
            File = stream,
            FileExt = ext
        };
    }
}