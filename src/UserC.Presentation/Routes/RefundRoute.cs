using Microsoft.AspNetCore.Mvc;
using Shared.Mediator.Interface;
using UserC.Application.Commands.Orders.Refunds;
using UserC.Application.Queries.Orders;

namespace UserC.Presentation.Routes;

public static class RefundRoute
{
    public static void MapRefund(this WebApplication app)
    {
        app.MapGet("/api/refund",  GetAsync);
        app.MapPost("/api/refund", SetAsync);
        app.MapPut("/api/refund",  EditAsync);
    }

    private static async Task<IResult> GetAsync(
        [FromServices] IMediator mediator,
        [AsParameters] RefundQuery query)
    {
        var data = await mediator.SendAsync(query);
        return Results.Ok(data);
    }
    
    private static async Task<IResult> SetAsync(
        [FromServices] IMediator mediator,
        [AsParameters] RefundCommand command)
    {
        await mediator.SendAsync(command);
        return Results.Ok();
    }
    
    private static async Task<IResult> EditAsync(
        [FromServices] IMediator mediator,
        [FromBody] EditRefundCommand command)
    {
        var refund = await mediator.SendAsync(command);
        return Results.Ok(refund);
    }
}