namespace UserC.Domain.Entities.Items;

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
    /// 狀態
    /// </summary>
    public int Level { get; set; }
    
    /// <summary>
    /// Foreign Key - 父類目
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    
    /// <summary>
    /// 更新時間
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; set; }
    
    /// <summary>
    /// 子類別 - Navigation Property
    /// </summary>
    public ICollection<Category> Children { get; set; } = [];

    /// <summary>
    /// 父類別 - Navigation Property
    /// </summary>
    public Category? Parent { get; set; }
    
}