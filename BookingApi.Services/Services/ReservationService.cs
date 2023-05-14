using BookingApi.Data.Helpers.Interfaces;
using BookingApi.Data.Models;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Reservation;
using FluentResults;
using Mapster;

namespace BookingApi.Services.Services;

public class ReservationService : IReservationService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReservationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ReservationModel>> CreateAsync(AddOrUpdateReservationModel addOrUpdateReservationModel)
    {
        var performer = await _unitOfWork.Performer.GetAsync(addOrUpdateReservationModel.PerformerId);
        if (performer == null)
        {
            return Result.Fail<ReservationModel>("Performer not found");
        }

        var user = await _unitOfWork.User.GetAsync(addOrUpdateReservationModel.UserId);
        if (user == null)
        {
            return Result.Fail<ReservationModel>("User not found");
        }

        var reservation = addOrUpdateReservationModel.Adapt<Reservation>();

        var createdReservation = await _unitOfWork.Reservation.CreateAsync(reservation);

        return Result.Ok(createdReservation.Adapt<ReservationModel>());
    }

    public async Task<Result<ReservationModel>> GetAsync(Guid id)
    {
        var reservation = await _unitOfWork.Reservation.GetAsync(id);
        if (reservation == null)
        {
            return Result.Fail<ReservationModel>("Reservation not found");
        }

        return Result.Ok(reservation.Adapt<ReservationModel>());
    }

    public async Task<Result<List<ReservationModel>>> GetByPerformerIdAsync(Guid performerId)
    {
        var reservations = await _unitOfWork.Reservation.GetByPerformerIdAsync(performerId);
        if (reservations == null)
        {
            return Result.Fail<List<ReservationModel>>("Reservations not found");
        }

        return Result.Ok(reservations.Adapt<List<ReservationModel>>());
    }

    public async Task<Result<List<ReservationModel>>> GetByUserIdAsync(Guid userId)
    {
        var reservations = await _unitOfWork.Reservation.GetByUserIdAsync(userId);
        if (reservations == null)
        {
            return Result.Fail<List<ReservationModel>>("Reservations not found");
        }
        
        var reservationModels = reservations.Adapt<List<ReservationModel>>();
        
        foreach (var reservationModel in reservationModels)
        {
            reservationModel.BrandName =
                reservations.FirstOrDefault(x => x.Id == reservationModel.Id).Performer.Brand.Name;

        }

        return Result.Ok(reservationModels);
    }

    public async Task<Result<List<ReservationModel>>> GetByBrandIdAsync(Guid brandId)
    {
        var reservations = await _unitOfWork.Reservation.GetByBrandIdAsync(brandId);
        if (reservations == null)
        {
            return Result.Fail<List<ReservationModel>>("Reservations not found");
        }

        return Result.Ok(reservations.Adapt<List<ReservationModel>>());
    }
    
    public async Task<Result<List<ReservationModel>>> GetAllReservationsOfCompaniesByUserIdAsync(Guid userId)
    {
        var user = await _unitOfWork.User.GetAsync(userId);
        if (user == null)
        {
            return Result.Fail<List<ReservationModel>>("User not found");
        }
        var reservations = await _unitOfWork.Reservation.GetByCompanyIdAsync(userId);
        if (reservations == null)
        {
            return Result.Fail<List<ReservationModel>>("Reservations not found");
        }
        
        var reservationModels = reservations.Adapt<List<ReservationModel>>();
        
        foreach (var reservationModel in reservationModels)
        {
            reservationModel.BrandName =
                reservations.FirstOrDefault(x => x.Id == reservationModel.Id).Performer.Brand.Name;

        }

        return Result.Ok(reservations.Adapt<List<ReservationModel>>());
    }

    public async Task<Result<ReservationModel>> UpdateAsync(Guid id, AddOrUpdateReservationModel addOrUpdateReservationModel)
    {
        var reservation = await _unitOfWork.Reservation.GetAsync(id);
        if (reservation == null)
        {
            return Result.Fail<ReservationModel>("Reservation not found");
        }

        var performer = await _unitOfWork.Performer.GetAsync(addOrUpdateReservationModel.PerformerId);
        if (performer == null)
        {
            return Result.Fail<ReservationModel>("Performer not found");
        }

        var user = await _unitOfWork.User.GetAsync(addOrUpdateReservationModel.UserId);
        if (user == null)
        {
            return Result.Fail<ReservationModel>("User not found");
        }

        reservation = addOrUpdateReservationModel.Adapt(reservation);

        var updatedReservation = await _unitOfWork.Reservation.UpdateAsync(reservation);

        return Result.Ok(updatedReservation.Adapt<ReservationModel>());
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var reservation = await _unitOfWork.Reservation.GetAsync(id);
        if (reservation == null)
        {
            return Result.Fail("Reservation not found");
        }

        await _unitOfWork.Reservation.DeleteAsync(id);

        return Result.Ok();
    }
}