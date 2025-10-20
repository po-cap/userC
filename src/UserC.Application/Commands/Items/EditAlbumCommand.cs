using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Items;

public class EditAlbumCommand : IRequest
{
    /// <summary>
    /// 商品 ID
    /// </summary>
    public long ItemId { get; set; }
    
    /// <summary>
    /// 相簿
    /// </summary>
    public List<string> Album { get; set; }
}



public class EditAlbumHandler : IRequestHandler<EditAlbumCommand>
{
    /// <summary>
    /// 認證用戶
    /// </summary>
    private readonly IAuthorizeUser _user;
    
    /// <summary>
    /// 商品倉儲
    /// </summary>
    private readonly IItemRepository _repository;
    
    public EditAlbumHandler(
        IAuthorizeUser user, 
        IItemRepository repository)
    {
        _user = user;
        _repository = repository;
    }
    
    public async Task HandleAsync(EditAlbumCommand request)
    {
        // 取出商品資料
        var item = await _repository.GetByIdAsync(
            request.ItemId,
            q => q.Include(x => x.Album));
        if(item == null)
            throw Failure.NotFound();

        // 是否有權限變更商品
        if (item.UserId == _user.Id)
            throw Failure.Forbidden();

        // 變更封面照
        item.Extra = UpdateCover(item.Extra, request.Album[0]);

        // 變更相簿順序
        item.Album.Assets = request.Album;

        // 處存變更
        await _repository.SaveChangeAsync(item);
    }
    
    public static JsonDocument UpdateCover(JsonDocument doc, string newCoverValue)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);

        writer.WriteStartObject();

        // 使用 RootElement 遍歷所有屬性
        foreach (var property in doc.RootElement.EnumerateObject())
        {
            writer.WritePropertyName(property.Name);
        
            if (property.Name == "cover")
            {
                // 修改 cover 屬性
                writer.WriteStringValue(newCoverValue);
            }
            else
            {
                // 其他屬性保持原樣
                property.Value.WriteTo(writer);
            }
        }

        writer.WriteEndObject();
        writer.Flush();

        return JsonDocument.Parse(stream.ToArray());
    }
}