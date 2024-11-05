using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.Common.Interfaces;

public interface IKindergartenDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<RefreshToken> RefreshTokens { get; }
}