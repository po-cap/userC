using Microsoft.AspNetCore.Mvc;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Infrastructure.Queries;
using UserC.Presentation.Contracts;

namespace UserC.Presentation.Routes;

public static class ProfileRoute
{
    public static void MapProfile(this WebApplication app)
    {
        app.MapGet("/api/profile/{userId:long}", GetAsync).RequireAuthorization("jwt");
        app.MapPut("/api/profile", EditAsync).RequireAuthorization("jwt");
    }

    /// <summary>
    /// 取得 - 使用者簡介
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    private static async Task<IResult> GetAsync(
        [FromServices]IMediator mediator, 
        [FromRoute]long userId)
    {
        var response = await mediator.SendAsync(new GetUserQuery()
        {
            UserId = userId
        });
        return Results.Ok(response);
    }
    
    /// <summary>
    /// 編輯 - 使用者資訊
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    private static async Task<IResult> EditAsync(
        [FromServices]IHttpContextAccessor httpContextAccessor,
        [FromServices]IMediator mediator,
        [FromBody]EditProfileReq request)
    {
        var context = httpContextAccessor.HttpContext ?? 
                      throw Failure.BadRequest();
        var response = await mediator.SendAsync(request.ToCommand(context));
        return Results.Ok(response);
    }
}