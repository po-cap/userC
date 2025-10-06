namespace UserC.Presentation.Routes;

public static class FollowRoute
{
    public static void MapFollow(this WebApplication app)
    {
        // 获取我的关注列表(或是否以關注某人)
        app.MapGet("/api/follow", () => { });
        // 关注用户
        app.MapPost("/api/follow", () => { });
        // 取消关注
        app.MapDelete("/api/follow", () => { });
    }
}