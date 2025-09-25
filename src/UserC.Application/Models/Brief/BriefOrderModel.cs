using UserC.Domain.Entities.Orders;

namespace UserC.Application.Models.Brief;

public class BriefOrderModel
{
    /// <summary>
    /// 交易 ID
    /// </summary>
    public required long Id { get; set; }

    /// <summary>
    /// 對方的頭像
    /// </summary>
    public required string Avatar { get; set; }
    
    /// <summary>
    /// 對方的名稱
    /// </summary>
    public required string DisplayName { get; set; }

    /// <summary>
    /// 商品照片
    /// </summary>
    public required string Cover { get; set; }

    /// <summary>
    /// 商品簡介
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// 交易價格
    /// </summary>
    public required double TotalAmount { get; set; }

    /// <summary>
    /// 訂單狀態
    /// </summary>
    public required int Status { get; set; }
}

public static partial class Convertor
{
    public static BriefOrderModel ToBriefModel(this Order entity, bool isBuyer)
    {
        var metadata = entity.Snapshot.RootElement;
        
        return new BriefOrderModel
        {
            Id = entity.Id,
            Avatar = isBuyer ? entity.Seller.Avatar : entity.Buyer.Avatar,
            DisplayName = isBuyer ? entity.Seller.DisplayName : entity.Buyer.DisplayName,
            Cover = metadata.GetProperty("cover").GetString() ?? "",
            Description = metadata.GetProperty("description").GetString() ?? "",
            TotalAmount = metadata.GetProperty("totalAmount").GetDouble(),
            Status = (int)entity.Status
        };
    }
}