using UserC.Application.Commands.Assets;

namespace UserC.Presentation.Contracts;

public class UploadImageReq
{
    public required IFormFile File { get; set; }
}

public static partial class ContractExtension
{
    public static UploadImageCmd ToCommand(this UploadImageReq request)
    {
        var stream = new MemoryStream();
        request.File.CopyTo(stream);

        var ext = Path.GetExtension(request.File.FileName);

        return new UploadImageCmd()
        {
            File = stream,
            FileExt = ext
        };
    }
}