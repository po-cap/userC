using UserC.Domain.Entities;

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
    /// <returns></returns>
    Item WithoutSPU(string userId, string description, List<string> album, List<SKU> skus);
}