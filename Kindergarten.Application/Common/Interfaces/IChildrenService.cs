using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Entities.Enums;

namespace Kindergarten.Application.Common.Interfaces;

public interface IChildrenService
{
    Task AddChildrenThroughParentRequest(string jsonChildren, Guid parentId, ParentChildRelationship relationship, CancellationToken cancellationToken);
}