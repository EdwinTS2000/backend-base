namespace App.Exceptions
{
  public abstract class AppException : Exception
  {
    public string Title { get; }

    protected AppException(string title, string message)
        : base(message)
    {
      Title = title;
    }
  }

  public sealed class NotFoundException : AppException
  {
    public NotFoundException(string name, object key)
        : base("Not Found", $"{name} con Id '{key}' no encontrado.") { }
  }

  public sealed class BadRequestException : AppException
  {
    public BadRequestException(string message)
        : base("Bad Request", message) { }
  }

  public sealed class UnauthorizedException : AppException
  {
    public UnauthorizedException(string message = "No autorizado.")
        : base("Unauthorized", message) { }
  }

  public sealed class ForbiddenException : AppException
  {
    public ForbiddenException(string message = "Acceso denegado.")
        : base("Forbidden", message) { }
  }

  public sealed class ConflictException : AppException
  {
    public ConflictException(string message)
        : base("Conflict", message) { }
  }
}