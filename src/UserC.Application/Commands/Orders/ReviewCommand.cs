using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Entities.Rating;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders;

public class ReviewCommand : IRequest<Review>
{
    /// <summary>
    /// 訂單 ID
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// 評分
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// 評語
    /// </summary>
    public string Comment { get; set; }
}

public class ReviewHandler : IRequestHandler<ReviewCommand, Review>
{
    private readonly IAuthorizeUser _authorizeUser;
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Snowflake _snowflake;

    public ReviewHandler(
        IAuthorizeUser authorizeUser,
        IOrderRepository orderRepository, 
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork, 
        Snowflake snowflake)
    {
        _authorizeUser = authorizeUser;
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _snowflake = snowflake;
    }

    public async Task<Review> HandleAsync(ReviewCommand request)
    {
        // 取得用戶資料
        var userId = _authorizeUser.Id;
        var user  = await _userRepository.GetByIdAsync(userId);
        if(user == null) throw Failure.NotFound();
        
        // 取得訂單資料
        var order = await _orderRepository.GetByIdAsync(
            request.OrderId,
            q => q.Include(x => x.Reviews));
        if(order == null) throw Failure.NotFound();
        
        // 增加評語
        var review = order.OnReview(
            id: _snowflake.Get(),
            user: user,
            rating: request.Rating,
            comment: request.Comment);
        
        // 存檔
        await _unitOfWork.SaveChangeAsync();

        return review;
    }
}