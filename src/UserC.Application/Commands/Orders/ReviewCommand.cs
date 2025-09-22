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
    private readonly IReviewRepository _reviewRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReviewHandler(
        IAuthorizeUser authorizeUser,
        IReviewRepository reviewRepository, 
        IOrderRepository orderRepository, 
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork)
    {
        _authorizeUser = authorizeUser;
        _reviewRepository = reviewRepository;
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Review> HandleAsync(ReviewCommand request)
    {
        // 取得用戶資料
        var userId = _authorizeUser.Id;
        var user  = await _userRepository.GetByIdAsync(userId);
        if(user == null) throw Failure.NotFound();
        
        // 取得訂單資料
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if(order == null) throw Failure.NotFound();
        
        // 取得
        var review = await _reviewRepository.AddAsync(
            order: order, 
            user: user, 
            rating: request.Rating, 
            comment: request.Comment);

        if ((int)order.Status < 4)
            throw Failure.BadRequest("貨物必須送達才能評價");
        
        // 如果貨物已送達，狀態改為評價中
        if (order.Status == OrderStatus.delivered)
            order.Status = OrderStatus.reviewing;
        
        // 如果狀態是評價中，改為訂單完成
        if (order.Status == OrderStatus.reviewing)
            order.Status = OrderStatus.completed;
        
        // 存檔
        await _unitOfWork.SaveChangeAsync();

        return review;
    }
}