using Microsoft.AspNetCore.Mvc;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Commands.Orders;
using UserC.Domain.Entities.Orders;
using UserC.Infrastructure.Queries.Orders;
using UserC.Presentation.Contracts.Orders;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Routes;

public static class PaymentRoute
{
    public static void MapPayment(this WebApplication app)
    {
        app.MapGet("/api/payment", GetAsync).RequireAuthorization("jwt");
        app.MapPut("/api/payment/set", SetAsync).RequireAuthorization("jwt");
        app.MapPut("/api/payment/pay", PayAsync).RequireAuthorization("jwt");
        app.MapPut("/api/payment/confirm", ConfirmAsync).RequireAuthorization("jwt");
    }

    private static async Task<IResult> ConfirmAsync(
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        [FromBody]ConfirmPaymentReq request)
    {
        await mediator.SendAsync(request.ToCommand(context));
        return Results.Ok();
    }
    
    private static async Task<IResult> PayAsync(
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        [FromBody]PayReq request)
    {
        await mediator.SendAsync(request.ToCommand(context));
        return Results.Ok();
    }  
    
    private static async Task<IResult> SetAsync(
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        [FromBody]SetPaymentReq request)
    {
        await mediator.SendAsync(request.ToCommand(context));
        return Results.Ok();
    }

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
}