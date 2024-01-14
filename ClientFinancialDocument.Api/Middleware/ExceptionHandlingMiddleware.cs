﻿using ClientFinancialDocument.Api.Extensions;
using ClientFinancialDocument.Application.Exceptions;
using ClientFinancialDocument.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ClientFinancialDocument.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;


        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

                var exceptionDetails = GetExceptionDetails(exception);

                var problemDetails = new ProblemDetails
                {
                    Status = exceptionDetails.Status,
                    Type = exceptionDetails.Type,
                    Title = exceptionDetails.Title,
                    Detail = exceptionDetails.Detail
                };

                if (exceptionDetails.Errors is not null)
                {
                    problemDetails.Extensions["errors"] = exceptionDetails.Errors;
                }

                context.Response.StatusCode = exceptionDetails.Status;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        private static ExceptionDetails GetExceptionDetails(Exception exception)
        {
            return exception switch
            {
                ValidationException validationException => new ExceptionDetails(
                //StatusCodes.Status400BadRequest,
                //FIXME: if we need to have valid type of StatusCodes (default is 400 for Validation but EnigmaTry task is asking 403)
                //it is better to use Response Pattern inside of Query/Command handlers
                    ResultExtensions.GetResultProblemStatusCode((ErrorType)Enum.Parse(typeof(ErrorType),
                    validationException.Errors.ToArray()[0].ErrorCode)),
                "ValidationFailure",
                "Validation error",
                    "One or more validation errors has occurred",
                    validationException.Errors),

                _ => new ExceptionDetails(
                    StatusCodes.Status500InternalServerError,
                    "ServerError",
                    "Server error",
                    "An unexpected error has occurred",
                    null)
            };
        }

        internal record ExceptionDetails(
            int Status,
            string Type,
            string Title,
            string Detail,
            IEnumerable<object>? Errors);
    }
}
