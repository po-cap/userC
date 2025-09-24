using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Commands.Orders;
using UserC.Domain.Enums;
using UserC.Infrastructure.Queries.Orders;
using UserC.Presentation.Contracts.Items;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Routes;

public static class OrderRoute
{
    public static void MapOrder(this WebApplication app)
    {
        app.MapGet("/api/order", GetAsync).RequireAuthorization("jwt");
        
        app.MapPost("/api/order",        AddAsync).RequireAuthorization("jwt");
        app.MapPut( "/api/order/ship",   ShipAsync).RequireAuthorization("jwt");
        app.MapPut( "/api/order/receive",ReceiveAsync).RequireAuthorization("jwt");
        app.MapPut( "/api/order/review", ReviewAsync).RequireAuthorization("jwt");
        
        
        app.MapPut("/api/order/cancel", () => { });
        app.MapPut("/api/order/refund", () => { });
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
    /// <param name="id">訂單 ID，如果這不為空，代表要訂單細節</param>
    /// <param name="size"></param>
    /// <param name="isBuyer"></param>
    /// <param name="lastId"></param>
    /// <param name="status">訂單狀態</param>
    /// <returns></returns>
    /// <exception cref="Failure"></exception>
    private static async Task<IResult> GetAsync(
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        long? id,
        int? size,
        bool isBuyer,
        long? lastId,
        OrderStatus? status)
    {
        var userId = context.HttpContext?.UserID() ?? throw Failure.BadRequest();

        if (id != null)
        {
            var order = await mediator.SendAsync(new GetOrderDetailQuery()
            {
                OrderId = id.Value,
                UserId = userId,
                IsBuyer = isBuyer
            });

            return Results.Ok(order);
        }
        else
        {
            var orders = await mediator.SendAsync(new GetOrdersQuery
            {
                UserId = userId,
                IsBuyer = isBuyer,
                Size = size ?? 10,
                LastId = lastId,
                Status = status
            });
            return Results.Ok(orders);    
        }
        
    }
}