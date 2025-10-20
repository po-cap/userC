using Microsoft.AspNetCore.Mvc;
using Shared.Mediator.Interface;
using UserC.Application.Commands.Users;
using UserC.Application.Queries;

namespace UserC.Presentation.Routes;

public static class FollowRoute
{
    public static void MapFollow(this WebApplication app)
    {
        app.MapGet ("/api/follow", GetAsync); // 获取我的关注列表(或是否以關注某人)
        app.MapPost("/api/follow", AddAsync); // (取消)關注用戶
    }

    /// <summary>
    /// (取消)關注
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    private static async Task<IResult> AddAsync(
        [FromServices]IMediator mediator,
        [FromBody]FollowCmd command)
    {
        await mediator.SendAsync(command);
        return Results.Ok();
    }
    
    /// <summary>
    /// 取得關注列表
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="cursorTime"></param>
    /// <param name="size"></param>
    /// <param name="followingId"></param>
    /// <returns></returns>
    private static async Task<IResult> GetAsync(
        [FromServices]IMediator mediator,
        [FromQuery]DateTimeOffset? cursorTime,
        [FromQuery]int? size,
        [FromQuery]long? followingId)
    {
        if (followingId != null)
        {
            var response = await mediator.SendAsync(new FollowStatusQuery
            {
                FollowingId = followingId.Value
            });
            return Results.Ok(response);
        }
        else
        {
            var response = await mediator.SendAsync(new FollowQuery()
            {
                CursorTime = cursorTime,
                Size = size
            });
            return Results.Ok(response);
        }
    }
}