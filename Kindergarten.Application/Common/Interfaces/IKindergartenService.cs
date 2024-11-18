namespace Kindergarten.Application.Common.Interfaces;

public interface IKindergartenService
{
    public Task<Guid> GetKindergartenId(string kindergartenName);
}