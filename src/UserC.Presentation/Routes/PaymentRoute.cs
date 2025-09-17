using Microsoft.AspNetCore.Mvc;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Domain.Entities.Orders;
using UserC.Infrastructure.Queries.Orders;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Routes;

public static class PaymentRoute
{
    public static void MapPayment(this WebApplication app)
    {
        app.MapGet("/api/payment", GetAsync).RequireAuthorization("jwt");
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