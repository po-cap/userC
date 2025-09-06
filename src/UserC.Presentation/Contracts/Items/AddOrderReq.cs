using System.Text.Json;
using UserC.Application.Commands.Orders;

namespace UserC.Presentation.Contracts.Items;

public class AddOrderReq
{
    /// <summary>
    /// 賣家 ID
    /// </summary>
    public long SellerId { get; set; }
    
    /// <summary>
    /// 單價
    /// </summary>
    public double UnitPrice { get; set; }

    /// <summary>
    /// 數量
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// 折扣價
    /// </summary>
    public double DiscountAmount { get; set; }

    /// <summary>
    /// 運費
    /// </summary>
    public double ShippingFee { get; set; }

    /// <summary>
    /// 商品鏈結 ID
    /// </summary>
    public long ItemId { get; set; }
    
    /// <summary>
    /// 庫存單元快照
    /// </summary>
    public JsonDocument Snapshot { get; set; }

    /// <summary>
    /// 收貨者名稱
    /// </summary>
    public string RecipientName { get; set; }

    /// <summary>
    /// 收貨者電話
    /// </summary>
    public string RecipientPhone { get; set; }

    /// <summary>
    /// 收貨地址
    /// </summary>
    public string Address { get; set; }
}

public static partial class ContractExtension
{
    public static AddOrderCommand ToCommand(this AddOrderReq request, long userId)
    {
        return new AddOrderCommand
        {
            SellerId       = request.SellerId,
            BuyerId        = userId,
            UnitPrice      = request.UnitPrice,
            Quantity       = request.Quantity,
            DiscountAmount = request.DiscountAmount,
            ShippingFee    = request.ShippingFee,
            ItemId         = request.ItemId,
            Snapshot       = request.Snapshot,
            RecipientName  = request.RecipientName,
            RecipientPhone = request.RecipientPhone,
            Address        = request.Address
        };

    }
}