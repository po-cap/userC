using Microsoft.AspNetCore.Mvc;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Commands.Orders.Payments;
using UserC.Infrastructure.Queries.Orders;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Routes;

public static class PaymentRoute
{
    public static void MapPayment(this WebApplication app)
    {
        app.MapGet("/api/payment",         GetAsync);
        app.MapPut("/api/payment/set",     SetAsync);
        app.MapPut("/api/payment/pay",     PayAsync);
        app.MapPut("/api/payment/confirm", ConfirmAsync);
    }
    
    /// <summary>
    /// 取得付款資訊
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mediator"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="Failure"></exception>
    private static async Task<IResult> GetAsync(
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        [FromQuery]long orderId)
    {
        var userId = context.HttpContext?.UserID() ?? throw Failure.BadRequest();

        var payment = await mediator.SendAsync(new GetPaymentQuery()
        {
            OrderId = orderId,
            UserId = userId
        });

        return Results.Ok(payment);
    }
    
    /// <summary>
    /// 設定收款碼（或帳戶）
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mediator"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    private static async Task<IResult> SetAsync(
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        [FromBody]SetPayCommand command)
    {
        await mediator.SendAsync(command);
        return Results.Ok();
    }
    
    /// <summary>
    /// 付款
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mediator"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    private static async Task<IResult> PayAsync(
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        [FromBody]PayCommand command)
    {
        await mediator.SendAsync(command);
        return Results.Ok();
    }  
    
    /// <summary>
    /// 確認收款
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mediator"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    private static async Task<IResult> ConfirmAsync(
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        [AsParameters]ReceivePayCommand command)
    {
        await mediator.SendAsync(command);
        return Results.Ok();
    }
    
}