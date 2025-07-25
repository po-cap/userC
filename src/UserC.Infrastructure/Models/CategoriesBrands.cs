using UserC.Domain.Entities;

namespace UserC.Infrastructure.Models;

public class CategoriesBrands
{
    /// <summary>
    /// Foreign Key - 類目
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// Navigation Property - 類目
    /// </summary>
    public required Category Category { get; set; }

    /// <summary>
    /// Foreign Key - 品牌
    /// </summary>
    public long BrandId { get; set; }

    /// <summary>
    /// Navigation Key - 品牌
    /// </summary>
    public required Brand Brand { get; set; }
}