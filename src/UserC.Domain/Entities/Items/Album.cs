namespace UserC.Domain.Entities.Items;

public class Album
{
    /// <summary>
    /// 商品鏈結 ID
    /// </summary>
    public long ItemId { get; set; }

    /// <summary>
    /// 相簿內容
    /// </summary>
    public List<string> Assets { get; set; }

    /// <summary>
    /// 是否為影片資源
    /// </summary>
    public bool IsVideo { get; set; }
}