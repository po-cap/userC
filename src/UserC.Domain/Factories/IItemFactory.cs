using System.Text.Json;
using UserC.Domain.Entities;
using UserC.Domain.Entities.Items;

namespace UserC.Domain.Factories;

public interface IItemFactory
{
    /// <summary>
    /// 建立連接 (不提供 SPU 的情況)
    /// </summary>
    /// <param name="userId">商品擁有者 ID</param>
    /// <param name="description">商品描述</param>
    /// <param name="album">相側</param>
    /// <param name="skus">庫存單元</param>
    /// <param name="assets">規格（價格，關鍵數性，擴展屬性，等等）</param>
    /// <param name="shippingFee">運費</param>
    /// <param name="isVideo">Assets 是否為 video</param>
    /// <returns></returns>
    Item WithoutSPU(
        long userId, 
        string description, 
        List<string> album, 
        List<SKU> skus,
        JsonDocument assets,
        double shippingFee,
        bool isVideo);
}