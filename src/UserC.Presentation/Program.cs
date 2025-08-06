using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application;
using UserC.Application.Models;
using UserC.Infrastructure;
using UserC.Infrastructure.Queries;
using UserC.Presentation.Contracts;


var builder = WebApplication.CreateBuilder(args);

var dir    = Environment.GetEnvironmentVariable("ASPNETCORE_DIRECTORY");
var env    = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") 
             ?? throw new Exception("Set \"ASPNETCORE_ENVIRONMENT\"");

builder.Configuration
    .SetBasePath(dir ?? Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


var OIDC = builder.Configuration["OIDC"];

builder.Services
       .AddApplication(builder.Configuration)
       .AddInfrastructure(builder.Configuration);

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

    })
    .AddJwtBearer("jwt", o =>
    {
        // Description - 
        //     告訴 framework，不要把 claim type 變成 Microsoft 自定義的 Type 
        o.MapInboundClaims = false;
            
        // Description - 
        //     定義 openid 的 endpoint 
        o.Authority = $"{OIDC}/oauth";
            
        // Description - 
        //     定義 Validate 過程中要 validate 哪些資料
        o.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
                
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5),
            RequireExpirationTime = true
        };
            
            
        // 啟用詳細錯誤訊息
        o.IncludeErrorDetails = true;
        
        // 事件處理器用於記錄詳細錯誤
        o.Events = new JwtBearerEvents
        {
            // 當認證失敗時
            OnChallenge = async context =>
            {
                context.HandleResponse();
        
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail = context.ErrorDescription ?? "无效的认证令牌",
                    Instance = context.Request.Path
                };
                
                var traceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
                problemDetails.Extensions["traceId"] = traceId;
        
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/problem+json";
        
                await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
            },
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

    app.MapPost("/api/item", async (
        HttpContext ctx,
        IMediator mediator,
        AddItemReq req) =>
    {
        var userId = ctx.User.FindFirst("sub")?.Value;
        if (userId == null)
        {
            throw Failure.Unauthorized();
        }

        var command = req.ToCommand(userId);

        var item = await mediator.SendAsync(command);

        return Results.Ok(item);
    }).RequireAuthorization("jwt");
    
    app.MapGet("/api/item", async (
        IMediator mediator,
        List<long>? id,
        long? userId,
        int size,
        long? lastId) =>
    {
        IEnumerable<ItemModel> items;
        if (userId == null)
            items = await mediator.SendAsync(
                new GetNewItemsQuery()
                {
                    Size = size,
                    LastId = lastId
                });
        else if(id != null)
            items = await mediator.SendAsync(
                new GetItemsQuery()
                {
                    Ids = id,
                });
        else
            items = await mediator.SendAsync(
                new GetUserItemsQuery()
                {
                    UserId = userId.Value,
                    Size = size,
                    LastId = lastId
                });
        
        return Results.Ok(items);
    });
    
    app.Run();
}
