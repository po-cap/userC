namespace UserC.Domain.Repositories;

public interface IRepository<T>
{
    /// <summary>
    /// 取得單筆資料
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <param name="func">LINQ expression</param>
    /// <returns></returns>
    Task<T?> GetByIdAsync(
        long id,
        Func<IQueryable<T>, IQueryable<T>>? func = null);

    /// <summary>
    /// 取得多比資料
    /// </summary>
    /// <param name="func">LINQ expression</param>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? func = null);
}