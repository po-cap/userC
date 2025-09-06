using System.Text.Json;
using UserC.Domain.Entities;
using UserC.Domain.Entities.Orders;

namespace UserC.Domain.Factories;

public interface IOrderFactory
{
    /// <summary>
    /// 建立訂單
    /// </summary>
    /// <param name="sellerId">賣家 ID</param>
    /// <param name="buyerId">買家 ID</param>
    /// <param name="unitPrice">單價</param>
    /// <param name="quantity">數量</param>
    /// <param name="discountAmount">折扣價</param>
    /// <param name="shippingFee">運費</param>
    /// <param name="itemId">商品鏈結 ID</param>
    /// <param name="snapshot">快照</param>
    /// <param name="recipientName">收貨者名稱</param>
    /// <param name="recipientPhone">收貨者電話</param>
    /// <param name="address">收貨地址</param>
    /// <returns></returns>
    Order New(
        long sellerId,
        long buyerId,
        double unitPrice,
        int quantity,
        double discountAmount,
        double shippingFee,
        long itemId,
        JsonDocument snapshot,
        string recipientName,
        string recipientPhone,
        string address
    );
}