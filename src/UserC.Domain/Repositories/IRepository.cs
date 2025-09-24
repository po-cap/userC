using System.Data;

namespace UserC.Domain.Repositories;

public interface IRepository<T>
{
    /// <summary>
    /// 設定一個 Transaction
    /// 需要制定 Isolation Level 實在用
    /// 平常使用 SaveChangeAsync 就好
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    IDbTransaction Begin(IsolationLevel level);
    
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

    /// <summary>
    /// 變更被追蹤的 Entity
    /// </summary>
    /// <param name="entity">要被變更的實體</param>
    /// <returns></returns>
    Task SaveChangeAsync(T entity);
}