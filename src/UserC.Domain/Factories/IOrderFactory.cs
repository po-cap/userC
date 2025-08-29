using System.Text.Json;
using UserC.Domain.Entities;

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
    /// <param name="skuId">庫存單元 ID</param>
    /// <param name="itemSpec">商品快照</param>
    /// <param name="skuSpec">庫存單元快照</param>
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
        long skuId,
        JsonDocument itemSpec,
        JsonDocument skuSpec,
        string recipientName,
        string recipientPhone,
        string address
    );
}