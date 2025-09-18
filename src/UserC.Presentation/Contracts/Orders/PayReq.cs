using Po.Api.Response;
using UserC.Application.Commands.Orders;
using UserC.Domain.Enums;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Contracts.Orders;

public class PayReq
{
    /// <summary>
    /// 付款證明截圖
    /// </summary>
    public string? ConfirmImage { get; set; }

    /// <summary>
    /// 付款方式
    /// </summary>
    public PaymentMethod Method { get; set; }
}

public static partial class ContractExtension
{
    public static PayCommand ToCommand(
        this PayReq req,
        IHttpContextAccessor accessor)
    {
        var ctx = accessor.HttpContext;
        if(ctx == null) throw Failure.BadRequest();
        
        if (ctx.Request.Query.TryGetValue("orderId", out var idValue))
        {
            if (long.TryParse(idValue, out long id))
            {
                return new PayCommand()
                {
                    OrderId = id,
                    UserId = ctx.UserID(),
                    ConfirmImage = req.ConfirmImage,
                    Method = req.Method,
                };
            } 
        }

        throw Failure.BadRequest();
    }
}