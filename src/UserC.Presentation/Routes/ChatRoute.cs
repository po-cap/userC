using Microsoft.AspNetCore.Mvc;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Infrastructure.Queries.Chats;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Routes;

public static class ChatRoute
{
    public static void MapChat(this WebApplication app)
    {
        app.MapGet("/api/chat",GetAsync).RequireAuthorization("jwt");
    }
    
    private static async Task<IResult> GetAsync(
        [FromServices]IHttpContextAccessor context,
        [FromServices]IMediator mediator,
        [FromQuery]string uri)
    {
        var userId = context.HttpContext?.UserID() ?? throw Failure.Unauthorized();

        var chat = await mediator.SendAsync(new ChatQuery()
        {
            UserId = userId,
            Uri = uri
        });

        return Results.Ok(chat);
    }
}