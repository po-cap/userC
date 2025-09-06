namespace UserC.Domain.Enums;

public enum MessageType
{
    /// <summary>
    /// 文字訊息
    /// </summary>
    text = 1,
    
    /// <summary>
    /// 貼圖
    /// </summary>
    sticker = 2,
    
    /// <summary>
    /// 照片
    /// </summary>
    image = 3,
    
    /// <summary>
    /// 影片
    /// </summary>
    video = 4,
    
    /// <summary>
    /// 進入聊天室
    /// </summary>
    join = 5,
    
    /// <summary>
    /// 退出聊天室
    /// </summary>
    exit = 6,
    
}