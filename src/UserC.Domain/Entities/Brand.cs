namespace UserC.Domain.Entities;

public class Brand
{
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 名稱
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// 別名
    /// </summary>
    public List<string>? Alias { get; set; }

    /// <summary>
    /// LOGO
    /// </summary>
    public required string Logo { get; set; }
}