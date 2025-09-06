using Microsoft.AspNetCore.Mvc;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Infrastructure.Queries.Orders;
using UserC.Presentation.Contracts.Items;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Routes;

public static class OrderRoute
{
    public static void MapOrder(this WebApplication app)
    {
        app.MapGet("/api/item", GetAsync).RequireAuthorization("jwt");
        app.MapPost("/api/item", AddAsync).RequireAuthorization("jwt");
    }
    
    
    private static async Task<IResult> AddAsync(
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        [FromBody]AddOrderReq request)
    {
        var userId = context.HttpContext?.UserID() ?? throw Failure.BadRequest();
        var response = await mediator.SendAsync(request.ToCommand(userId));
        return Results.Ok(response);
    }

    private static async Task<IResult> GetAsync(
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        int size,
        bool isBuyer,
        long? lastId)
    {
        var userId = context.HttpContext?.UserID() ?? throw Failure.BadRequest();
        
        var orders = await mediator.SendAsync(new SellerGetOrdersQuery
        {
            UserId = userId,
            IsBuyer = isBuyer,
            Size = size,
            LastId = lastId,
        });
        return Results.Ok(orders);
    }
}