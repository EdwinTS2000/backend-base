using System.Text.Json;
using App.Exceptions;

namespace Api.Middlewares
{
  public sealed class ExceptionMiddleware : IMiddleware
  {
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ExceptionMiddleware(
        ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
    {
      _logger = logger;
      _env = env;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      try
      {
        await next(context);
      }
      catch (Exception e)
      {
        await HandleExceptionAsync(context, e);
      }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      var statusCode = GetStatusCode(exception);

      if (statusCode >= 500)
        _logger.LogError(exception, "Unhandled server error: {Message}", exception.Message);
      else
        _logger.LogWarning("Handled exception [{Status}]: {Message}", statusCode, exception.Message);

      var response = new
      {
        title = exception is AppException appEx
          ? appEx.Title
          : "Server Error",
        status = statusCode,
        detail = statusCode >= 500 && !_env.IsDevelopment()
              ? "Ocurrió un error interno. Contacte al administrador."
              : exception.Message,
        errors = GetErrors(exception)
      };

      context.Response.ContentType = "application/json";
      context.Response.StatusCode = statusCode;

      await context.Response.WriteAsync(
          JsonSerializer.Serialize(response, _jsonOptions));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
          BadRequestException => StatusCodes.Status400BadRequest,
          UnauthorizedException => StatusCodes.Status401Unauthorized,
          ForbiddenException => StatusCodes.Status403Forbidden,
          NotFoundException => StatusCodes.Status404NotFound,
          ConflictException => StatusCodes.Status409Conflict,
          AppValidationException => StatusCodes.Status422UnprocessableEntity,
          _ => StatusCodes.Status500InternalServerError
        };

    private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
    {
      if (exception is AppValidationException validationException)
        return validationException.ErrorsDictionary;

      return new Dictionary<string, string[]>();
    }
  }
}