namespace Kindergarten.Application.Common.Interfaces;

public interface ICoordinatorService
{
    public Task<bool> CheckIfCoordinatorWorksInSameKindergarten(string kindergartenName);
}