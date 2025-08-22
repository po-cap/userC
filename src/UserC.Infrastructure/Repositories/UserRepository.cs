using Po.Api.Response;
using UserC.Domain.Entities;
using UserC.Domain.Repositories;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async  Task<User> GetByIdAsync(long id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if(user == null) throw Failure.NotFound();
        return user;
    }
}