namespace UserC.Presentation.Routes;

public static class FavoriteRoute
{
    public static void MapFavorite(this WebApplication app)
    {
        // 获取我的收藏列表(或是否已收藏某商品)
        app.MapGet("/api/favorite", () => { }).RequireAuthorization("jwt");
        // 收藏商品
        app.MapPost("/api/favorite", () => { }).RequireAuthorization("jwt");
        // 取消收藏
        app.MapDelete("/api/favorite", () => { }).RequireAuthorization("jwt");
    }
}