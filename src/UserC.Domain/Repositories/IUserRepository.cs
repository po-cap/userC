using UserC.Domain.Entities;

namespace UserC.Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(long id);
}