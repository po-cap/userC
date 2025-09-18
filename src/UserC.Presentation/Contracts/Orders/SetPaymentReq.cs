using Po.Api.Response;
using UserC.Application.Commands.Orders;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Contracts.Orders;

public class SetPaymentReq
{
    /// <summary>
    /// 匯款帳戶
    /// </summary>
    public string? BankAccount { get; set; }

    /// <summary>
    /// 收款碼
    /// </summary>
    public string? QrCodeImage { get; set; }
}

public static partial class ContractExtension
{
    public static SetPaymentCommand ToCommand(
        this SetPaymentReq req,
        IHttpContextAccessor accessor)
    {
        var ctx = accessor.HttpContext;
        if(ctx == null) throw Failure.BadRequest();
        
        if (ctx.Request.Query.TryGetValue("orderId", out var idValue))
        {
            if (long.TryParse(idValue, out long id))
            {
                return new SetPaymentCommand()
                {
                    OrderId = id,
                    UserId = ctx.UserID(),
                    BankAccount = req.BankAccount,
                    QrCodeImage = req.QrCodeImage,
                };
            } 
        }

        throw Failure.BadRequest();
    }
}