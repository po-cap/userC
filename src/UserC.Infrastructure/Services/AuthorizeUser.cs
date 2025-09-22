using Microsoft.AspNetCore.Http;
using Po.Api.Response;
using UserC.Application.Services;

namespace UserC.Infrastructure.Services;

public class AuthorizeUser : IAuthorizeUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// 認證用戶的 ID
    /// </summary>
    public long Id { get; init; }

    public AuthorizeUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        var context = httpContextAccessor.HttpContext;
        if (context == null) throw new Exception("Cannot found HttpContext");
        
        var sub = context.User.FindFirst("sub")?.Value;
        if(sub == null)
            throw Failure.Unauthorized();
    
        if(!long.TryParse(sub, out var userId))
            throw Failure.Unauthorized();

        Id = userId;
    }
}