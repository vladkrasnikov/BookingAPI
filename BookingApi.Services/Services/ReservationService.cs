using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Reservation;
using FluentResults;
using Mapster;

namespace BookingApi.Services.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IPerformerRepository _performerRepository;
    private readonly IUserRepository _userRepository;

    public ReservationService(IReservationRepository reservationRepository, IPerformerRepository performerRepository,
        IUserRepository userRepository)
    {
        _reservationRepository = reservationRepository;
        _performerRepository = performerRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<ReservationModel>> CreateAsync(AddOrUpdateReservationModel addOrUpdateReservationModel)
    {
        var performer = await _performerRepository.GetAsync(addOrUpdateReservationModel.PerformerId);
        if (performer == null)
        {
            return Result.Fail<ReservationModel>("Performer not found");
        }

        var user = await _userRepository.GetAsync(addOrUpdateReservationModel.UserId);
        if (user == null)
        {
            return Result.Fail<ReservationModel>("User not found");
        }

        var reservation = addOrUpdateReservationModel.Adapt<Reservation>();

        var createdReservation = await _reservationRepository.CreateAsync(reservation);

        return Result.Ok(createdReservation.Adapt<ReservationModel>());
    }

    public async Task<Result<ReservationModel>> GetAsync(Guid id)
    {
        var reservation = await _reservationRepository.GetAsync(id);
        if (reservation == null)
        {
            return Result.Fail<ReservationModel>("Reservation not found");
        }

        return Result.Ok(reservation.Adapt<ReservationModel>());
    }

    public async Task<Result<List<ReservationModel>>> GetByPerformerIdAsync(Guid performerId)
    {
        var reservations = await _reservationRepository.GetByPerformerIdAsync(performerId);
        if (reservations == null)
        {
            return Result.Fail<List<ReservationModel>>("Reservations not found");
        }

        return Result.Ok(reservations.Adapt<List<ReservationModel>>());
    }

    public async Task<Result<List<ReservationModel>>> GetByUserIdAsync(Guid userId)
    {
        var reservations = await _reservationRepository.GetByUserIdAsync(userId);
        if (reservations == null)
        {
            return Result.Fail<List<ReservationModel>>("Reservations not found");
        }

        return Result.Ok(reservations.Adapt<List<ReservationModel>>());
    }

    public async Task<Result<List<ReservationModel>>> GetByBrandIdAsync(Guid brandId)
    {
        var reservations = await _reservationRepository.GetByBrandIdAsync(brandId);
        if (reservations == null)
        {
            return Result.Fail<List<ReservationModel>>("Reservations not found");
        }

        return Result.Ok(reservations.Adapt<List<ReservationModel>>());
    }

    public async Task<Result<ReservationModel>> UpdateAsync(Guid id,
        AddOrUpdateReservationModel addOrUpdateReservationModel)
    {
        var reservation = await _reservationRepository.GetAsync(id);
        if (reservation == null)
        {
            return Result.Fail<ReservationModel>("Reservation not found");
        }

        var performer = await _performerRepository.GetAsync(addOrUpdateReservationModel.PerformerId);
        if (performer == null)
        {
            return Result.Fail<ReservationModel>("Performer not found");
        }

        var user = await _userRepository.GetAsync(addOrUpdateReservationModel.UserId);
        if (user == null)
        {
            return Result.Fail<ReservationModel>("User not found");
        }

        reservation = addOrUpdateReservationModel.Adapt(reservation);

        var updatedReservation = await _reservationRepository.UpdateAsync(reservation);

        return Result.Ok(updatedReservation.Adapt<ReservationModel>());
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var reservation = await _reservationRepository.GetAsync(id);
        if (reservation == null)
        {
            return Result.Fail("Reservation not found");
        }

        await _reservationRepository.DeleteAsync(id);

        return Result.Ok();
    }
}