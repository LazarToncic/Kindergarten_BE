using Kindergarten.Application.Common.Exceptions;

namespace Kindergarten.Infrastructure.Exceptions;

public class InfrastructureException(string message, object? additionalData = null) : BaseException(message, additionalData);