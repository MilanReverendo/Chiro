using Chiro.Application.Dtos;
using Chiro.Domain.Entities;

namespace Chiro.Application.Interfaces
{
    public interface IAuthService
    {
        Task<IEnumerable<UserShortDto>> GetAllUsersAsync();
        Task<IEnumerable<UserShortDto>> GetGroupLeadersAsync();
        Task<UserShortDto>? GetUserByIdAsync(Guid id);
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
        Task<UserShortDto> ModifyDetailsAsync(UserShortDto request);
        Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDto request);
    }
}
