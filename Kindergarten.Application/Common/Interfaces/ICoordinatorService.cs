namespace Kindergarten.Application.Common.Interfaces;

public interface ICoordinatorService
{
    public Task<bool> CheckIfCoordinatorWorksInSameKindergarten(string kindergartenName);
    public Task<bool> CheckIfCoordinatorWorksInSameKindergarten2(string kindergartenName);

    public Task<bool> CheckIfCoordinatorWorksInSameKindergarten(Guid kindergartenId, CancellationToken cancellationToken);

    Task<Guid> GetKindergartenIdForCoordinator(string userId, CancellationToken cancellationToken);
}