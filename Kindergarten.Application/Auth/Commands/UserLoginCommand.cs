using Kindergarten.Application.Common.Dto.Auth;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Auth.Commands;

public record UserLoginCommand(LoginUserDto Dto) : IRequest<LoginResponseDto>;

public class UserLoginCommandHandler(IAuthService authService) : IRequestHandler<UserLoginCommand, LoginResponseDto>
{
    public async Task<LoginResponseDto> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.LoginAsync(request.Dto);
    }
} 