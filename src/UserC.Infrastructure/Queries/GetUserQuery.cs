using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Models;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries;

public class GetUserQuery : IRequest<UserModel>
{
    public long UserId { get; set; }    
}



public class GetUserHandler : IRequestHandler<GetUserQuery, UserModel>
{
    private readonly AppDbContext _dbContext;

    public GetUserHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserModel> HandleAsync(GetUserQuery request)
    {
        var user = await _dbContext.Users.FindAsync(request.UserId);
        if(user == null)
            throw Failure.NotFound();
        return user.ToModel();
    }
}