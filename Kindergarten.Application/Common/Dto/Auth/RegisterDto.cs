namespace Kindergarten.Application.Common.Dto.Auth;

public record RegisterDto(string FirstName, string LastName, string Username, int YearOfBirth, string Email, string Password, string PhoneNumber);