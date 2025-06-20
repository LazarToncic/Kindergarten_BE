using System.Text.Json;
using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Kindergarten.Application.Common.Mappers.Parent;

[Mapper]
public static partial class ParentRequestResponseMapper
{
    public static GetParentRequestQueryResponseDto ParentRequestToGetParentRequestQueryResponseDto(this List<ParentRequest> parentRequests)
    {
            var options = new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            };

            var parentRequestDtos = parentRequests.Select(pr =>
            {

                List<ParentRequestChildDto> children;
                try
                {
                    children = JsonSerializer
                        .Deserialize<List<ParentRequestChildDto>>(pr.ChildrenJson, options)
                        ?? new List<ParentRequestChildDto>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Mapper] Json.Deserialize error: {ex.Message}");
                    children = new List<ParentRequestChildDto>();
                }

                var dto = new ParentRequestSingleResponseDto(
                    pr.Id,
                    pr.User.FirstName,
                    pr.User.LastName,
                    pr.User.Email!,
                    pr.User.PhoneNumber!,
                    pr.PreferredKindergarten,
                    pr.IsOnlineApproved,
                    pr.IsInPersonApproved,
                    pr.CreatedAt,
                    pr.ParentRole,
                    children,
                    pr.OnlineApprovedByUser != null
                        ? $"{pr.OnlineApprovedByUser.FirstName} {pr.OnlineApprovedByUser.LastName}"
                        : "Not approved yet",
                    pr.InPersonApprovedByUser != null
                        ? $"{pr.InPersonApprovedByUser.FirstName} {pr.InPersonApprovedByUser.LastName}"
                        : "Not approved yet"
                );
                
                return dto;

            }).ToList();

            var responseDto = new GetParentRequestQueryResponseDto(parentRequestDtos);

            return responseDto;
    }

    public static ParentRequestSingleResponseDto ParentRequestToParentRequestSingleResponseDto(
        this ParentRequest pr)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var children = JsonSerializer
                           .Deserialize<List<ParentRequestChildDto>>(pr.ChildrenJson, options)
                       ?? new List<ParentRequestChildDto>();

        // Sastavimo DTO
        var dto = new ParentRequestSingleResponseDto(
            pr.Id,
            pr.User.FirstName,
            pr.User.LastName,
            pr.User.Email!,
            pr.User.PhoneNumber!,
            pr.PreferredKindergarten,
            pr.IsOnlineApproved,
            pr.IsInPersonApproved,
            pr.CreatedAt,
            pr.ParentRole,
            children,
            pr.OnlineApprovedByUser != null
                ? $"{pr.OnlineApprovedByUser.FirstName} {pr.OnlineApprovedByUser.LastName}"
                : "Not approved yet",
            pr.InPersonApprovedByUser != null
                ? $"{pr.InPersonApprovedByUser.FirstName} {pr.InPersonApprovedByUser.LastName}"
                : "Not approved yet"
        );

        return dto;
    }
}