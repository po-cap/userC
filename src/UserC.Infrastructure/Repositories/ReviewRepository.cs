using Po.Api.Response;
using UserC.Application.Services;
using UserC.Domain.Entities;
using UserC.Domain.Entities.Orders;
using UserC.Domain.Entities.Rating;
using UserC.Domain.Repositories;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Repositories;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    private readonly Snowflake _snowflake;
    
    public ReviewRepository(
        AppDbContext context, 
        Snowflake snowflake) : base(context)
    {
        _snowflake = snowflake;
    }
    
    public async Task<Review> AddAsync(
        Order order, 
        User user, 
        int rating, 
        string comment)
    {
        if (order == null)
            throw Failure.NotFound();
        
        var review = new Review()
        {
            Id = _snowflake.Get(),
            ReviewerAvatar = user.Avatar,
            ReviewerDisplayName = user.DisplayName,
            IsBuyer = order.BuyerId == user.Id,
            OrderId = order.Id,
            UserId = user.Id,
            Rating = rating,
            Comment = comment,
            CreatedAt = DateTimeOffset.Now
        };

        await context.AddAsync(review);

        return review;
    }
}