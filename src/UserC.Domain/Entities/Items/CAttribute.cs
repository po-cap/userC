namespace UserC.Domain.Entities.Items;

public class CAttribute
{
    /// <summary>
    /// 類目 ID
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// 屬性名稱
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 屬性值
    /// </summary>
    public List<string> Values { get; set; } = [];
}