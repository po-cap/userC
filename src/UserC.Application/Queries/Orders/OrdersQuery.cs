using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Entities.Orders;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;

namespace UserC.Application.Queries.Orders;

public record OrdersQuery : IRequest<IEnumerable<Order>>
{
    /// <summary>
    /// 是否為買家
    /// </summary>
    public bool IsBuyer { get; init; }

    /// <summary>
    /// 頁大小
    /// </summary>
    public int? Size { get; init; }

    /// <summary>
    /// 上一頁最後一筆資料 ID
    /// </summary>
    public long? LastId { get; init; }

    /// <summary>
    /// 狀態
    /// </summary>
    public OrderStatus? Status { get; init; }

    /// <summary>
    /// 訂單 ID ， 如果只想查詢一筆時
    /// </summary>
    public long? Id { get; set; }
}

public class OrdersQueryHandler : IRequestHandler<OrdersQuery, IEnumerable<Order>>
{
    private readonly IAuthorizeUser _user;

    private readonly IOrderRepository _repository;

    public OrdersQueryHandler(
        IAuthorizeUser user, 
        IOrderRepository repository)
    {
        _user = user;
        _repository = repository;
    }


    public async Task<IEnumerable<Order>> HandleAsync(OrdersQuery request)
    {
        if (request.Id != null)
        {
            var order = await _repository.GetByIdAsync(
                request.Id.Value, q => q
                .Include(x => x.Buyer)
                .Include(x => x.Seller)
                .Include(x => x.Shipment)
                .Include(x => x.Record)
                .Include(x => x.Amount));
            if(order == null)
                throw Failure.NotFound();

            return [order];
        }
        else
        {
            return await _repository.GetAllAsync(q => q
                .Include(x => x.Buyer)
                .Include(x => x.Seller)
                .Where(x => request.IsBuyer ? x.BuyerId == _user.Id : x.SellerId == _user.Id)
                .Where(x => request.Status == null || x.Status == request.Status)
                // 如果是要搜尋“已送達”狀態的，需要查看是否已經被評論過了沒
                .Where(x => request.Status == null || 
                            request.Status != OrderStatus.delivered || 
                            (request.IsBuyer ? !x.ReviewedByBuyer : !x.ReviewedBySeller))
                // 用 last Id 做分頁處理
                .Where(x => request.LastId == null || x.Id > request.LastId)
                .OrderByDescending(x => x.Id)
                .Take(request.Size ?? 20)
            );    
        }
    }
}