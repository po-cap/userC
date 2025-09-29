using Po.Api.Response;
using UserC.Application.Commands.Orders.Payments;

namespace UserC.Presentation.Contracts.Orders;

public class ConfirmPaymentReq;

public static partial class ContractExtension
{
    public static ReceivePayCommand ToCommand(
        this ConfirmPaymentReq req,
        IHttpContextAccessor accessor)
    {
        var ctx = accessor.HttpContext;
        if(ctx == null) throw Failure.BadRequest();
        
        if (ctx.Request.Query.TryGetValue("orderId", out var idValue))
        {
            if (long.TryParse(idValue, out long id))
            {
                return new ReceivePayCommand()
                {
                    OrderId = id,
                };
            } 
        }
        
        throw Failure.BadRequest();
    }
}