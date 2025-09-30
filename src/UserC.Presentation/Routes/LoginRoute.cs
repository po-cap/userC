using Microsoft.AspNetCore.Authentication;

namespace UserC.Presentation.Routes;

public static class LoginRoute
{
    public static void MapLogin(this WebApplication app)
    {
        app.MapGet("/api/login/xiao_hong_mao", XiaoHongMao).AllowAnonymous();
        app.MapGet("/api/login/line", Line).AllowAnonymous();
    }
    
    
    private static IResult XiaoHongMao()
    {
        return Results.Challenge(
            new AuthenticationProperties(),
            new List<string>() { "xiao_hong_mao" });
    }
    
    private static IResult Line()
    {
        return Results.Challenge(
            new AuthenticationProperties(),
            new List<string>() { "line" });
    }
}