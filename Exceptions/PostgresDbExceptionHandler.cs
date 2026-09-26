using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace WatchArchive.Server.Exceptions;

public class PostgresDbExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (
            exception is DbUpdateException dbUpdateException
            && dbUpdateException.InnerException is PostgresException postgresException
        )
        {
            switch (postgresException.SqlState)
            {
                case PostgresErrorCodes.UniqueViolation:
                    await WriteResponse(
                        httpContext,
                        StatusCodes.Status409Conflict,
                        "The resource already exists.",
                        cancellationToken
                    );
                    return true;

                case PostgresErrorCodes.ForeignKeyViolation:
                    await WriteResponse(
                        httpContext,
                        StatusCodes.Status409Conflict,
                        "The referenced resource does not exist.",
                        cancellationToken
                    );
                    return true;
            }
        }

        await WriteResponse(
            httpContext,
            StatusCodes.Status500InternalServerError,
            "An unexpected error occurred.",
            cancellationToken
        );

        return true;
    }

    private static async Task WriteResponse(
        HttpContext context,
        int statusCode,
        string message,
        CancellationToken cancellationToken
    )
    {
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new { message }, cancellationToken);
    }
}
