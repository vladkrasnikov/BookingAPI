using FluentResults;
using Mapster;

namespace BookingApi.Services.Extensions;

public static class ResultMapper
{
    public static Result<TO> EntityToModel<TFrom, TO>(this Result<TFrom> result)
    {
        return result.IsFailed ? result.ToResult() : Result.Ok(result.Value.Adapt<TO>());
    }
}