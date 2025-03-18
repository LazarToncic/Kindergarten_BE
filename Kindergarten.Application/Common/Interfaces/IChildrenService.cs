using Kindergarten.Domain.Entities;

namespace Kindergarten.Application.Common.Interfaces;

public interface IChildrenService
{
    Task AddChildrenThroughParentRequest(string jsonChildren, Guid parentId, CancellationToken cancellationToken);
}