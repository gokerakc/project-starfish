﻿namespace Starfish.Web.Exceptions;

public static class ProblemDetailsHelper
{
    public static void ConfigureProblemDetails(Hellang.Middleware.ProblemDetails.ProblemDetailsOptions options, bool isDevelopment)
    {
        // Only include exception details in a development environment. There's really no need
        // to set this as it's the default behavior. It's just included here for completeness :)
        options.IncludeExceptionDetails = (ctx, ex) => isDevelopment;

        // Custom mapping function for FluentValidation's ValidationException.
        //options.MapFluentValidationException();

        // You can configure the middleware to re-throw certain types of exceptions, all exceptions or based on a predicate.
        // This is useful if you have upstream middleware that needs to do additional handling of exceptions.
        options.Rethrow<NotSupportedException>();

        // This will map NotImplementedException to the 501 Not Implemented status code.
        options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

        // This will map HttpRequestException to the 503 Service Unavailable status code.
        options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);

        // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
        // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
        options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
    }
}