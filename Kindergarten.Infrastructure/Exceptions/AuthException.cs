namespace Kindergarten.Infrastructure.Exceptions;

internal class AuthException(string message, object? additionalData = null) : InfrastructureException(message, additionalData);