using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Po.Api.Response;
using Po.Media;
using Shared.Mediator.Interface;
using UserC.Application;
using UserC.Presentation.Contracts;


var builder = WebApplication.CreateBuilder(args);

var dir    = Environment.GetEnvironmentVariable("ASPNETCORE_DIRECTORY");
var env    = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") 
             ?? throw new Exception("Set \"ASPNETCORE_ENVIRONMENT\"");;

builder.Configuration
    .SetBasePath(dir ?? Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


var OIDC = builder.Configuration["OIDC"];

builder.Services.AddApplication(builder.Configuration);

builder.Services.AddAuthentication("cookie")
    .AddCookie("cookie")
    .AddOAuth("xiao_hong_mao", o =>
    {
        o.ClientId = "7U6bcdfd9dd2d3c0ba9bd682274d831eef";
        o.ClientSecret = "7f859ffaba9cee6c3815e867a89b6d35";
        o.UsePkce = true;

        o.AuthorizationEndpoint = $"{OIDC}/oauth/authorize";
        o.TokenEndpoint = $"{OIDC}/oauth/token";
        o.UserInformationEndpoint = $"{OIDC}/oauth/information";

        o.Scope.Clear();
        o.Scope.Add("profile");
        o.Scope.Add("openid");
        o.Scope.Add("email");

        o.CallbackPath = "/api/xiao_hong_mao/auth-cb";
        o.SaveTokens = true;
        // Description - 
        //     如果是在安全級別比較強的 API SERVER，這裏應該是要將 access token 存起來，
        //     然後準備一個 session token，不管用 cookie 還是 REST 的方式，回傳
        //     給使用者這個 session token
        o.Events.OnCreatingTicket = ctx =>
        {
            ctx.Identity?.AddClaim(new Claim("access_token", ctx.AccessToken ?? throw new Exception()));
            ctx.Identity?.AddClaim(new Claim("refresh_token", ctx.RefreshToken ?? throw new Exception()));

            return Task.CompletedTask;
        };
        o.Events.OnTicketReceived = ctx =>
        {
            // 停止默認的重定向行為
            ctx.HandleResponse();

            ctx.HttpContext.AuthenticateAsync("xiao_hong_mao");

            var accessToken = ctx.Properties?.GetTokenValue("access_token");
            var refreshToken = ctx.Properties?.GetTokenValue("refresh_token");

            ctx.Response.StatusCode = 200;
            ctx.Response.ContentType = "application/json";

            return ctx.Response.WriteAsJsonAsync(new
            {
                access_token = accessToken,
                refresh_token = refreshToken,
                expries_in = ctx.Properties?.ExpiresUtc?.Subtract(DateTimeOffset.UtcNow).Seconds,
                token_type = "Bearer"
            });
        };
        o.Events.OnRemoteFailure = ctx =>
        {
            ctx.HandleResponse();

            ctx.Response.StatusCode = 401;
            ctx.Response.ContentType = "application/json";

            return ctx.Response.WriteAsJsonAsync(new { });
        };

    })
    .AddOAuth("line", o =>
    {
        o.ClientId = "7U6bcdfd9dd2d3c0ba9bd682274d831eef";
        o.ClientSecret = "7f859ffaba9cee6c3815e867a89b6d35";
        o.UsePkce = true;

        o.AuthorizationEndpoint = $"{OIDC}/oauth/line/authorize";
        o.TokenEndpoint = $"{OIDC}/oauth/token";
        o.UserInformationEndpoint = $"{OIDC}/oauth/information";

        o.Scope.Clear();
        o.Scope.Add("profile");
        o.Scope.Add("openid");
        o.Scope.Add("email");

        o.CallbackPath = "/api/line/auth-cb";
        o.SaveTokens = true;
        // Description - 
        //     如果是在安全級別比較強的 API SERVER，這裏應該是要將 access token 存起來，
        //     然後準備一個 session token，不管用 cookie 還是 REST 的方式，回傳
        //     給使用者這個 session token
        o.Events.OnCreatingTicket = ctx =>
        {
            ctx.Identity?.AddClaim(new Claim("access_token", ctx.AccessToken ?? throw new Exception()));
            ctx.Identity?.AddClaim(new Claim("refresh_token", ctx.RefreshToken ?? throw new Exception()));

            return Task.CompletedTask;
        };
        o.Events.OnTicketReceived = ctx =>
        {
            // 停止默認的重定向行為
            ctx.HandleResponse();

            ctx.HttpContext.AuthenticateAsync("line");

            var accessToken = ctx.Properties?.GetTokenValue("access_token");
            var refreshToken = ctx.Properties?.GetTokenValue("refresh_token");

            ctx.Response.StatusCode = 200;
            ctx.Response.ContentType = "application/json";

            return ctx.Response.WriteAsJsonAsync(new
            {
                access_token = accessToken,
                refresh_token = refreshToken,
                expries_in = ctx.Properties?.ExpiresUtc?.Subtract(DateTimeOffset.UtcNow).Seconds,
                token_type = "Bearer"
            });
        };
        o.Events.OnRemoteFailure = ctx =>
        {
            ctx.HandleResponse();

            ctx.Response.StatusCode = 401;
            ctx.Response.ContentType = "application/json";

            return ctx.Response.WriteAsJsonAsync(new { });
        };

    });

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("jwt", b =>
    {
        b.RequireAuthenticatedUser()
            .AddAuthenticationSchemes("jwt")
            .RequireClaim("sub");
    });
});




var app = builder.Build();
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor   |
                           ForwardedHeaders.XForwardedHost  | 
                           ForwardedHeaders.XForwardedProto, 
        
        KnownProxies = { IPAddress.Parse("127.0.0.1") }
    });    

    app.UseAuthentication();   // 確認身份
    app.UseAuthorization();    // 確認權限
    
    app.MapGet("/api/login/line", () =>
    {
        return Results.Challenge(
            new AuthenticationProperties(),
            new List<string>() { "line" });
    });

    app.MapGet("/api/login/xiao_hong_mao", () =>
    {
        return Results.Challenge(
            new AuthenticationProperties(),
            new List<string>() { "xiao_hong_mao" });
    });

    app.MapGet("/api/jwt", () => { Results.Ok("Hello Worlds"); }).RequireAuthorization("jwt");

    app.MapPost("/api/image", async (
        IMediator mediator,
        [FromForm] UploadImageReq request) =>
    {
        using var command = request.ToCommand();
        var media = await mediator.SendAsync(command);
        return Results.Ok(media);
    }).DisableAntiforgery();
    
    app.Run();
}
