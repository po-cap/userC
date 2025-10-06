using Microsoft.AspNetCore.Mvc;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Models.Brief;
using UserC.Infrastructure.Queries.Items;
using UserC.Presentation.Contracts;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Routes;

public static class ItemRoute
{
    public static void MapItem(this WebApplication app)
    {
        app.MapPost("/api/item", AddAsync);
        app.MapGet("/api/item", GetAsync).AllowAnonymous();
    }
    
    
    private static async Task<IResult> AddAsync(
        [FromServices]IHttpContextAccessor httpCtxAccessor,
        [FromServices]IMediator mediator,
        [FromBody]AddItemReq req)
    {
        var ctx = httpCtxAccessor.HttpContext ??
                  throw Failure.BadRequest();
        var userId = ctx.UserID();
        var command = req.ToCommand(userId);
        var item = await mediator.SendAsync(command);
        return Results.Ok(item);
    }

    private static async Task<IResult> GetAsync(
        [FromServices]IMediator mediator,
        [FromQuery]long? userId,
        [FromQuery]long? id,
        [FromQuery]int? size,
        [FromQuery]long? lastId)
    {
        // 獲取傷品鏈結細節
        if (id != null)
        {
            var item = await mediator.SendAsync(
                new GetDetailItemQuery()
                {
                    ItemId = id.Value
                });
            return Results.Ok(item);
        }
        
        // 獲取商品鏈結列表
        IEnumerable<BriefItemModel> items;
        // 獲取最新商品
        if (userId == null)
            items = await mediator.SendAsync(
                new GetNewItemsQuery()
                {
                    Size = size ?? 20,
                    LastId = lastId
                });
        // 會取個人商品
        else
            items = await mediator.SendAsync(
                new GetUserItemsQuery()
                {
                    UserId = userId.Value,
                    Size = size ?? 20,
                    LastId = lastId
                });
        
        return Results.Ok(items);
    }
}