using Po.Api.Response;
using UserC.Application.Commands.Orders;
using UserC.Domain.Enums;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Contracts.Orders;

public class SetPayoutDetailRequest
{
    /// <summary>
    /// 匯款帳戶
    /// </summary>
    public string? BankAccount { get; set; }

    /// <summary>
    /// 收款碼
    /// </summary>
    public string? QrCodeImage { get; set; }
    
    /// <summary>
    /// 銀行名稱
    /// </summary>
    public string? BankName { get; set; }
    
    /// <summary>
    /// 銀行代碼
    /// </summary>
    public string? BankCode { get; set; }

    /// <summary>
    /// 付款方式
    /// </summary>
    public PaymentMethod Method { get; set; }
}

public static partial class ContractExtension
{
    public static SetPayoutDetailCommand ToCommand(
        this SetPayoutDetailRequest req,
        IHttpContextAccessor accessor)
    {
        var ctx = accessor.HttpContext;
        if(ctx == null) throw Failure.BadRequest();
        
        if (ctx.Request.Query.TryGetValue("orderId", out var idValue))
        {
            if (long.TryParse(idValue, out long id))
            {
                return new SetPayoutDetailCommand()
                {
                    OrderId = id,
                    UserId = ctx.UserID(),
                    BankName = req.BankName,
                    BankCode = req.BankCode,
                    BankAccount = req.BankAccount,
                    QrCodeImage = req.QrCodeImage,
                    Method = req.Method
                };
            } 
        }

        throw Failure.BadRequest();
    }
}