namespace Kindergarten.Application.Common.Interfaces;

public interface IRoleService
{
    Task CreateRoleAsync(string role);
    Task AddRoleToEmployeeAsync(string userId, string newRole, CancellationToken cancellationToken);
}