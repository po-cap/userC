using System.Text.Json;
using UserC.Domain.Entities.Items;
using UserC.Domain.Enums;

namespace UserC.Domain.Entities.Orders;

public class Order
{
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 買家 ID
    /// </summary>
    public long BuyerId { get; set; }
    
    /// <summary>
    /// 賣家 ID
    /// </summary>
    public long SellerId { get; set; }
    
    /// <summary>
    /// Item ID
    /// </summary>
    public long ItemId { get; set; }
    
    /// <summary>
    /// 狀態
    /// </summary>
    public OrderStatus Status { get; set; }
    
    /// <summary>
    /// 快照
    /// </summary>
    public JsonDocument Snapshot { get; set; }

    /// <summary>
    /// Navigation Property - 明細
    /// </summary>
    public OrderAmount Amount { get; set; }

    /// <summary>
    /// Navigation Property - 記事
    /// </summary>
    public OrderRecord Record { get; set; }

    /// <summary>
    /// Navigation Property - 貨運資訊
    /// </summary>
    public OrderShipment Shipment { get; set; }
    
    /// <summary>
    /// Navigation Property - 買家
    /// </summary>
    public User Buyer { get; set; }
    
    /// <summary>
    /// Navigation Property - 賣家
    /// </summary>
    public User Seller { get; set; }
    
    /// <summary>
    /// Navigation Property - 商品鏈結
    /// </summary>
    public Item Item { get; set; }
}