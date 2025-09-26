using Microsoft.AspNetCore.Mvc;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Commands.Orders;
using UserC.Application.Models.Brief;
using UserC.Application.Models.Detailed;
using UserC.Application.Queries.Orders;
using UserC.Presentation.Contracts.Items;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Routes;

public static class OrderRoute
{
    public static void MapOrder(this WebApplication app)
    {
        app.MapGet("/api/order", GetAsync).RequireAuthorization("jwt");
        app.MapDelete("/api/order", DeleteAsync).RequireAuthorization("jwt");
        
        app.MapPost("/api/order",        AddAsync).RequireAuthorization("jwt");
        app.MapPut( "/api/order/ship",   ShipAsync).RequireAuthorization("jwt");
        app.MapPut( "/api/order/receive",ReceiveAsync).RequireAuthorization("jwt");
        app.MapPut( "/api/order/review", ReviewAsync).RequireAuthorization("jwt");
        
        
        app.MapPut("/api/order/cancel", () => { });
        app.MapPut("/api/order/refund", () => { });
    }

    private static async Task<IResult> DeleteAsync(
        [FromServices]IMediator mediator,
        [AsParameters]DeleteOrderCommand command)
    {
        await mediator.SendAsync(command);
        return Results.Ok();
    }

    /// <summary>
    /// 使用者對訂單做評價
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    private static async Task<IResult> ReviewAsync(
        [FromServices]IMediator mediator,
        [FromBody] ReviewCommand command)
    {
        var review = await mediator.SendAsync(command);
        return Results.Ok(review);
    }
    
    /// <summary>
    /// 買家確認收貨
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    private static async Task<IResult> ReceiveAsync(
        [FromServices]IMediator mediator,
        [AsParameters]ReceivedCommand command)
    {
        await mediator.SendAsync(command);
        return Results.Ok();
    }
    
    /// <summary>
    /// 增加一筆購買記錄
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="Failure"></exception>
    private static async Task<IResult> AddAsync(
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        [FromBody]AddOrderReq request)
    {
        var userId = context.HttpContext?.UserID() ?? throw Failure.BadRequest();
        var response = await mediator.SendAsync(request.ToCommand(userId));
        return Results.Ok(response);
    }
    
    /// <summary>
    /// 設定發貨信息
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mediator"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    private static async Task<IResult> ShipAsync(        
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        [FromBody]SetTrackingNumberCommand command)
    {
        await mediator.SendAsync(command);
        return Results.Ok();
    }

    /// <summary>
    /// 搜尋購買記錄
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mediator"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    /// <exception cref="Failure"></exception>
    private static async Task<IResult> GetAsync(
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        [AsParameters]OrdersQuery query)
    {
        var orders = await mediator.SendAsync(query);
        
        if (query.Id == null)
            return Results.Ok(from order in orders select order.ToBriefModel(query.IsBuyer));
        else
            return Results.Ok(orders.First().ToDetailModel(query.IsBuyer));

    }
}