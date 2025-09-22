using Shared.Mediator.Interface;
using UserC.Application.Models.Detailed;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders;

public class SetTrackingNumberCommand : IRequest
{
    /// <summary>
    /// 使用者 ID
    /// </summary>
    public required long UserId { get; set; }
    
    /// <summary>
    /// 訂單編號
    /// </summary>
    public required long orderId { get; set; }

    /// <summary>
    /// 物流公司
    /// </summary>
    public required string ShippingProvider { get; set; }

    /// <summary>
    /// 物流單號
    /// </summary>
    public required string TrackingNumber { get; set; }
}

public class SetTrackingNumberHandler : IRequestHandler<SetTrackingNumberCommand>
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public SetTrackingNumberHandler(
        IOrderRepository repository, 
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }


    public async Task HandleAsync(SetTrackingNumberCommand request)
    {
        await _repository.MarkAsShippedAsync(
            id: request.orderId,
            userId: request.UserId,
            ShippingProvider: request.ShippingProvider,
            trackingNumber: request.TrackingNumber);

        await _unitOfWork.SaveChangeAsync();
    }
}

