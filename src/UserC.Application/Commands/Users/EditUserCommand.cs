using Shared.Mediator.Interface;
using UserC.Application.Models;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Users;

public class EditUserCommand : IRequest<UserModel>
{
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 顯示名稱
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// 頭像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 橫幅
    /// </summary>
    public string? Banner { get; set; }
}

internal class EditUserHandler : IRequestHandler<EditUserCommand, UserModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditUserHandler(
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserModel> HandleAsync(EditUserCommand request)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        user.DisplayName = request.DisplayName;
        user.Avatar = request.Avatar;
        user.Banner = request.Banner;

        await _unitOfWork.SaveChangeAsync();

        return user.ToModel();
    }
}