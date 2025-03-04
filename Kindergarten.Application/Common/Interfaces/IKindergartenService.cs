namespace Kindergarten.Application.Common.Interfaces;

public interface IKindergartenService
{
    public Task<Guid> GetKindergartenId(string kindergartenName);
    public Task<string> GetKindergartenName(Guid kindergartenId);
}