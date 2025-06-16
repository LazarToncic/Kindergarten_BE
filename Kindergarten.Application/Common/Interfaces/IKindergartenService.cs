using Kindergarten.Application.Common.Dto.Kindergarten;

namespace Kindergarten.Application.Common.Interfaces;

public interface IKindergartenService
{
    public Task<Guid> GetKindergartenId(string kindergartenName);
    public Task<string> GetKindergartenName(Guid kindergartenId);
    public Task<Guid> GetKindergartenIdWithDepartmentId(Guid departmentId);
    public Task<List<GetIdAndNameKindergartenDto>> GetKindergartenIdsAndNames();
}