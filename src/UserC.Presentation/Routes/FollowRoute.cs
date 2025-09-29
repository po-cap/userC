namespace UserC.Presentation.Routes;

public static class FollowRoute
{
    public static void MapFollow(this WebApplication app)
    {
        // 获取我的关注列表(或是否以關注某人)
        app.MapGet("/api/follow", () => { }).RequireAuthorization("jwt");
        // 关注用户
        app.MapPost("/api/follow", () => { }).RequireAuthorization("jwt");
        // 取消关注
        app.MapDelete("/api/follow", () => { }).RequireAuthorization("jwt");
    }
}