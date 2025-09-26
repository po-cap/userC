using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Entities.Orders;
using UserC.Domain.Repositories;

namespace UserC.Application.Queries.Orders;

public class RefundQuery : IRequest<Refund>
{
    public long OrderId { get; set; }
}

public class RefundQueryHandler : IRequestHandler<RefundQuery, Refund>
{
    private readonly IOrderRepository _repository;
    private readonly IAuthorizeUser _user;

    public RefundQueryHandler(
        IOrderRepository repository,
        IAuthorizeUser user)
    {
        _repository = repository;
        _user = user;
    }

    public async Task<Refund> HandleAsync(RefundQuery request)
    {
        var order = await _repository.GetByIdAsync(
            request.OrderId, 
            q => q.Include(x => x.Refund));
        if (order == null)
            throw Failure.NotFound();

        if (_user.Id != order.SellerId && _user.Id != order.BuyerId)
            throw Failure.Forbidden();

        return order.Refund;
    }
} 