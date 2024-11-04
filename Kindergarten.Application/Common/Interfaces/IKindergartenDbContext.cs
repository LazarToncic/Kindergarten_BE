namespace Kindergarten.Application.Common.Interfaces;

public interface IKindergartenDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}