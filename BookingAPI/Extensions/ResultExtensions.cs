using FluentResults;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Extensions;

public static class ResultExtensions
{
    public static Result<TO> ToResult<TFrom, TO>(Result<TFrom> result)
    {
        return result.IsFailed ? result.ToResult() : Result.Ok(result.Value.Adapt<TO>());
    }
    
    public static IActionResult ToActionResult(this Result result)
    {
        return result.IsSuccess ? HandleSuccess(result) : HandleError(result);
    }

    public static IActionResult ToActionResult<TType>(this Result<TType> result)
    {
        return result.IsSuccess ? HandleSuccess(result) : HandleError(result);
    }

    private static IActionResult HandleSuccess<TType>(Result<TType> result)
    {
        return new ObjectResult(result.Value)
        {
            StatusCode = SelectSuccessStatusCode(result)
        };
    }

    private static IActionResult HandleSuccess(ResultBase result)
    {
        return new StatusCodeResult(SelectSuccessStatusCode(result));
    }

    private static IActionResult HandleSuccess(ResultBase result, object resultObject)
    {
        return new ObjectResult(resultObject)
        {
            StatusCode = SelectSuccessStatusCode(result)
        };
    }

    private static IActionResult HandleError(ResultBase result)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Internal error"
        };

        if (result.Errors.Any())
        {
            // Workaround to result.Errors serialization problem. Without this casting json is empty.
            problemDetails.Extensions["internalErrors"] = result.Errors.Cast<Error>();
        }

        if (result.Successes.Any())
        {
            problemDetails.Extensions["internalSuccesses"] = result.Successes;
        }

        var objectResult = new ObjectResult(problemDetails)
        {
            StatusCode = SelectErrorStatusCode(result)
        };

        return objectResult;
    }

    private static int SelectSuccessStatusCode(ResultBase result)
    {
        // if (result.HasSuccess<Created>())
        // {
        //     return StatusCodes.Status201Created;
        // }

        return StatusCodes.Status200OK;
    }

    private static int SelectErrorStatusCode(ResultBase result)
    {
        // if (
        //     result.HasError<VerificationError>()
        //     || result.HasError<LimitExceededError>()
        //     || result.HasError<NotActiveError>()
        // )
        // {
        //     return StatusCodes.Status400BadRequest;
        // }
        //
        // if (result.HasError<ForbiddenError>())
        // {
        //     return StatusCodes.Status403Forbidden;
        // }
        //
        // if (result.HasError<NotFoundError>())
        // {
        //     return StatusCodes.Status404NotFound;
        // }
        //
        // if (result.HasError<ConflictError>())
        // {
        //     return StatusCodes.Status409Conflict;
        // }

        return StatusCodes.Status500InternalServerError;
    }
}