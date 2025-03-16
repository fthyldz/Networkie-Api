namespace Networkie.Application.Common.Exceptions;

public class BadRequestException(string message = "BadRequest") : Exception(message);