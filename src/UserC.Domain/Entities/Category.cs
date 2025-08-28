using UserC.Domain.Enums;

namespace UserC.Domain.Entities;

public class Category
{
    /// <summary>
    /// 名稱
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 類目層級
    /// </summary>
    public required string Note { get; set; }

    /// <summary>
    /// 類目狀態
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// 狀態
    /// </summary>
    public int Level { get; set; }
    
    /// <summary>
    /// Foreign Key - 父類目
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 子類別 - Navigation Property
    /// </summary>
    public ICollection<Category> Children { get; set; } = [];

    /// <summary>
    /// 父類別 - Navigation Property
    /// </summary>
    public Category? Parent { get; set; }

    /// <summary>
    /// 品牌
    /// </summary>
    public ICollection<Brand> Brands { get; set; } = [];
}