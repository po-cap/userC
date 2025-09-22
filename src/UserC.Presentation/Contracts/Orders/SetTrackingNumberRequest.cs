using UserC.Application.Commands.Orders;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Contracts.Orders;

public class SetTrackingNumberRequest
{
    /// <summary>
    /// 訂單編號
    /// </summary>
    public long orderId { get; set; }

    /// <summary>
    /// 物流公司
    /// </summary>
    public string ShippingProvider { get; set; }

    /// <summary>
    /// 物流單號
    /// </summary>
    public string TrackingNumber { get; set; }
}

public static partial class ContractExtension
{
    public static SetTrackingNumberCommand ToCommand(
        this SetTrackingNumberRequest req,
        IHttpContextAccessor accessor)
    {
        var context = accessor.HttpContext;
        if (context == null) throw new Exception("Please register HttpContextAccessor");
        
        return new SetTrackingNumberCommand
        {
            UserId = context.UserID(),
            orderId = req.orderId,
            ShippingProvider = req.ShippingProvider,
            TrackingNumber = req.TrackingNumber
        };
    }
}