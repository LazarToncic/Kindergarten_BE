using Kindergarten.Application.Common.Dto.Auth;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Auth.Commands;

public record RefreshTokenCommand(string RefreshToken) : IRequest<LoginResponseDto>;

public class RefreshTokenCommandHandler(ITokenService tokenService) : IRequestHandler<RefreshTokenCommand, LoginResponseDto>
{
    public async Task<LoginResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await tokenService.GenerateRefreshTokenAfterAccessIsExpired(request.RefreshToken);
    }
} 