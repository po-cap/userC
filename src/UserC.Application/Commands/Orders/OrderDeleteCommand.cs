using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders;

public class OrderDeleteCommand : IRequest
{
    public long OrderId { get; set; }
}


public class OrderDeleteHandler : IRequestHandler<OrderDeleteCommand>
{
    private readonly IOrderRepository _repository;

    private readonly IAuthorizeUser _user;

    public OrderDeleteHandler(
        IOrderRepository repository, 
        IAuthorizeUser user)
    {
        _repository = repository;
        _user = user;
    }

    public async Task HandleAsync(OrderDeleteCommand request)
    {
        var order = await _repository.GetByIdAsync(request.OrderId);
        if (order == null)
            throw Failure.NotFound();

        if (_user.Id == order.SellerId)
        {
            if(order.Status != OrderStatus.pending)
                throw Failure.BadRequest("買家已付款，只能走退款退貨流程");
            
            await _repository.DeleteAsync(order);
        }
        else if(_user.Id == order.BuyerId)
        {
            if(order.Status >= OrderStatus.shipped)
                throw Failure.BadRequest("賣家已出貨，只能走退款退貨流程");
            
            await _repository.DeleteAsync(order);
        }
        else
        {
            throw Failure.Unauthorized();
        }
    }
}