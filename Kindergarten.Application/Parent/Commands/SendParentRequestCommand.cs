using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Parent.Commands;

public record SendParentRequestCommand(SendParentRequestCommandDto Dto) : IRequest;

public class SendParentRequestCommandHandler(IParentService parentService) : IRequestHandler<SendParentRequestCommand>
{
    public async Task Handle(SendParentRequestCommand request, CancellationToken cancellationToken)
    {
        await parentService.CreateParentRequest(request.Dto.NumberOfChildren, request.Dto.AdditionalInfo,
            request.Dto.PreferredKindergarten, request.Dto.Children, cancellationToken);
    }
}