namespace UserC.Domain.Entities.Rating;

public class Reviews
{
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 被評價者 ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 被評價傷品 ID
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// 被評價的人是否為買家
    /// </summary>
    public bool IsBuyer { get; set; }

    /// <summary>
    /// 評語者頭像
    /// </summary>
    public string ReviewerAvatar { get; set; }

    /// <summary>
    /// 評語者顯示名稱
    /// </summary>
    public string ReviewerDisplayName { get; set; }

    /// <summary>
    /// 評分
    /// 1: 差評
    /// 2: 不好不壞
    /// 3: 好評
    /// </summary>
    public int rating { get; set; }

    /// <summary>
    /// 評價
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// 評價時間
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
}

//id                    bigserial primary key,
//    user_id               bigint not null references users(id)  on delete set null, -- 被評價的人
//order_id              bigint not null references orders(id) on delete set null, -- 被評價的商品
//is_buyer              boolean not null,                                         -- 是否為買家
//receiver_avatar       text not null,                                        -- 用户头像
//receiver_display_name text not null ,                                       -- 用户名
//rating                smallint not null check (rating >= 1 AND rating <= 3),    -- 综合评分，1-3星
//comment               text,                                                     -- 评价内容
//created_at            timestamptz default now()                                 -- 評價時間 