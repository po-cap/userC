using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Models;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries.Chats;

public class ChatQuery : IRequest<ChatModel>
{
    public string Uri { get; set; }

    public long UserId { get; set; }
}


public class ChatHandler : IRequestHandler<ChatQuery, ChatModel>
{
    private readonly AppDbContext _context;

    public ChatHandler(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ChatModel> HandleAsync(ChatQuery request)
    {
      
        
        var el = request.Uri.Split("/");
        if(long.TryParse(el[0], out var buyerId)) 
            throw Failure.BadRequest();
        if(long.TryParse(el[1], out var itemId)) 
            throw Failure.BadRequest();

        bool fromBuyer;  
        if (buyerId != request.UserId)
            fromBuyer = false;
        else
            fromBuyer = true;


        ChatModel chat;
        if (fromBuyer)
        {
            var item = await _context.Items
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == itemId);
            
            if(item == null)
                throw Failure.NotFound();

            chat = new ChatModel
            {
                Uri = request.Uri,
                PartnerId = item.UserId,
                Title = item.User.DisplayName,
                Avatar = item.User.Avatar,
                Photo = item.Extra.RootElement.GetProperty("cover").GetString()
            };
        }
        else
        {
            var item = await _context.Items
                .FirstOrDefaultAsync(x => x.Id == itemId);

            var buyer = await _context.Users.FirstOrDefaultAsync(x => x.Id == buyerId);
            
            if(item == null || buyer == null)
                throw Failure.NotFound();

            chat = new ChatModel
            {
                Uri = request.Uri,
                PartnerId = buyer.Id,
                Title = buyer.DisplayName,
                Avatar = buyer.Avatar,
                Photo = item.Extra.RootElement.GetProperty("cover").GetString()
            };
        }

        return chat;
    }
}