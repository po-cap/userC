using Microsoft.EntityFrameworkCore;
using UserC.Domain.Repositories;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext context;
    private readonly DbSet<T> _dbSet;

    protected Repository(AppDbContext context)
    {
        this.context = context;
        _dbSet = context.Set<T>();
    }

    /// <summary>
    /// 取得單筆資料
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <param name="func">LINQ expression</param>
    /// <returns></returns>
    public async Task<T?> GetByIdAsync(long id, Func<IQueryable<T>, IQueryable<T>>? func = null)
    {
        // 1. 從 DbSet 開始
        IQueryable<T> query = _dbSet;

        // 2. 如果呼叫端提供了自訂義的 include 函數，則應用它
        if (func != null)
        {
            query = func(query);
        }

        // 3. 加上條件並執行查詢
        return await query.FirstOrDefaultAsync(e => EF.Property<long>(e, "Id") == id);
    }

    /// <summary>
    /// 取得多比資料
    /// </summary>
    /// <param name="func">LINQ expression</param>
    /// <returns></returns>
    public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? func = null)
    {
        IQueryable<T> query = _dbSet;
        
        if (func != null)
        {
            query = func(query);
        }
        
        return await query.ToListAsync();
    }
}
