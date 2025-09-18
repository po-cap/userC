using Po.Api.Response;
using UserC.Application.Commands.Orders;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Contracts.Orders;

public class ConfirmPaymentReq;

public static partial class ContractExtension
{
    public static ConfirmPaymentCommand ToCommand(
        this ConfirmPaymentReq req,
        IHttpContextAccessor accessor)
    {
        var ctx = accessor.HttpContext;
        if(ctx == null) throw Failure.BadRequest();
        
        if (ctx.Request.Query.TryGetValue("orderId", out var idValue))
        {
            if (long.TryParse(idValue, out long id))
            {
                return new ConfirmPaymentCommand()
                {
                    OrderId = id,
                    UserId = ctx.UserID(),
                };
            } 
        }
        
        throw Failure.BadRequest();
    }
}