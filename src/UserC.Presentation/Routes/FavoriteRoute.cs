using Microsoft.AspNetCore.Mvc;
using Shared.Mediator.Interface;
using UserC.Application.Commands.Users;
using UserC.Application.Queries;

namespace UserC.Presentation.Routes;

public static class FavoriteRoute
{
    public static void MapFavorite(this WebApplication app)
    {
        app.MapGet("/api/favorites",    GetAsync);                 // 获取我的收藏列表(或是否已收藏某商品)
        app.MapPost("/api/favorites",   AddAsync);                 // (取消)收藏商品
        app.MapGet("/api/favorites/{itemId}/status", StatusAsync); // 查看商品是否收藏過
    }

    /// <summary>
    /// 查看商品是否收藏過
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    private static async Task<IResult> StatusAsync(
        [FromServices]IMediator mediator,
        [FromRoute]long itemId)
    {
        var result = await mediator.SendAsync(new FavoriteStatusQuery() { ItemId = itemId });
        return Results.Ok(result);
    }
    
    
    /// <summary>
    /// 获取我的收藏列表(或是否已收藏某商品)
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    private static async Task<IResult> GetAsync(
        [FromServices]IMediator mediator,
        [AsParameters]FavoriteQuery query)
    { 
        var items = await mediator.SendAsync(query);
        return Results.Ok(items);
    }
    
    
    /// <summary>
    /// 收藏
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    private static async Task<IResult> AddAsync(
        [FromServices]IMediator mediator,
        [AsParameters]FavoriteCmd command)
    {
        await mediator.SendAsync(command);
        return Results.Ok();
    }
}