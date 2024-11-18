using Kindergarten.Application.Common.Dto.Qualification;

namespace Kindergarten.Application.Common.Dto.Employee;

public record CreateEmployeeDto(string EmailOrUsername, string EmployeePositionName, string KindergartenName, 
    List<QualificationCreateEmployeeDto> Qualifications, string DepartmentName);