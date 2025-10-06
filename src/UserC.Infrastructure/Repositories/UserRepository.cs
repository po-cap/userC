using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Po.Api.Response;
using UserC.Domain.Entities;
using UserC.Domain.Repositories;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly ILogger<UserRepository> _logger;
    
    public UserRepository(
        AppDbContext context, 
        ILogger<UserRepository> logger) : base(context)
    {
        _logger = logger;
    }

    public override async Task SaveChangeAsync(User entity)
    {
        try
        {
            await base.SaveChangeAsync(entity);
        }
        // 重複 insert 可能發生在收藏或追蹤操作
        catch (InvalidOperationException ex) when (
            ex.Message.Contains("cannot be tracked") && 
            ex.Message.Contains("another instance")) 
        { }
    }
}