using Kindergarten.Domain.Entities;

namespace Kindergarten.Application.Common.Dto.ChildWithdrawalRequest;

public record WithdrawalRequestDtoResponse(
         Guid RequestId ,
         string ChildFullName ,   
         string FatherFullName ,
         string MotherFullName ,
         string SubmittedByName,      
         string? ReviewedByName,      
         DateTime? ReviewedAt,
         string TeacherFullName ,
         string DepartmentName ,
         string KindergartenName ,
         ChildWithdrawalRequestStatus Status ,
         DateTime CreatedAt ,
         string? Reason
    );