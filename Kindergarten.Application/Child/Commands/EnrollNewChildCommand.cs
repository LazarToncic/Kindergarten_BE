using Kindergarten.Application.Common.Dto.Children;
using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities.Enums;
using MediatR;

namespace Kindergarten.Application.Child.Commands;

public record EnrollNewChildCommand(NewChildDto NewChildDto, ParentChildRelationship ParentChildRelationship) : IRequest;

public class EnrollNewChildCommandHandler(IChildrenService childrenService) : IRequestHandler<EnrollNewChildCommand>
{
    public async Task Handle(EnrollNewChildCommand request, CancellationToken cancellationToken)
    {
        await childrenService.AddNewChild(request.NewChildDto.FirstName, request.NewChildDto.LastName,
            request.NewChildDto.DateOfBirth, request.NewChildDto.HasAllergies, request.NewChildDto.Allergies,
            request.NewChildDto.HasMedicalIssues, request.NewChildDto.MedicalConditions, request.ParentChildRelationship, request.NewChildDto.PreferredKindergartenId, cancellationToken);
    } 
}