using B2BDashboard.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace B2BDashboard.Api.ErrorHandling;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "Recurso não encontrado"),
            ConflictException => (StatusCodes.Status409Conflict, "Conflito de dados"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Requisição inválida"),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, "Não autorizado"),
            _ => (StatusCodes.Status500InternalServerError, "Erro interno do servidor")
        };

        var detail = statusCode == StatusCodes.Status500InternalServerError
            ? "Ocorreu um erro interno. Nossa equipe já foi notificada."
            : exception.Message;

        if (statusCode == StatusCodes.Status500InternalServerError)
            logger.LogError(exception, "Erro não tratado em {Path}", httpContext.Request.Path);

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}