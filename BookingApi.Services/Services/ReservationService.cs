using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.Reservation;
using FluentResults;

namespace BookingApi.Services.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IPerformerRepository _performerRepository;
    private readonly IUserRepository _userRepository;

    public ReservationService(IReservationRepository reservationRepository, IPerformerRepository performerRepository, IUserRepository userRepository)
    {
        _reservationRepository = reservationRepository;
        _performerRepository = performerRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<ReservationModel>> CreateAsync(CreateReservationModel createReservationModel)
    {
        var performer = await _performerRepository.GetAsync(createReservationModel.PerformerId);
        if (performer == null)
        {
            return Result.Fail<ReservationModel>("Performer not found");
        }

        var user = await _userRepository.GetAsync(createReservationModel.UserId);
        if (user == null)
        {
            return Result.Fail<ReservationModel>("User not found");
        }

        var reservation = new Reservation
        {
            PerformerId = createReservationModel.PerformerId,
            UserId = createReservationModel.UserId,
            StartDate = createReservationModel.StartDate,
            EndDate = createReservationModel.EndDate
        };

        var createdReservation = await _reservationRepository.CreateAsync(reservation);

        return Result.Ok(new ReservationModel
        {
            Id = createdReservation.Id,
            PerformerId = createdReservation.PerformerId,
            UserId = createdReservation.UserId,
            StartDate = createdReservation.StartDate,
            EndDate = createdReservation.EndDate,
        });
    }

    public async Task<Result<ReservationModel>> GetAsync(Guid id)
    {
        var reservation = await _reservationRepository.GetAsync(id);
        if (reservation == null)
        {
            return Result.Fail<ReservationModel>("Reservation not found");
        }

        return Result.Ok(new ReservationModel
        {
            Id = reservation.Id,
            PerformerId = reservation.PerformerId,
            UserId = reservation.UserId,
            StartDate = reservation.StartDate,
            EndDate = reservation.EndDate
        });
    }

    public async Task<Result<List<ReservationModel>>> GetByPerformerIdAsync(Guid performerId)
    {
        var reservations = await _reservationRepository.GetByPerformerIdAsync(performerId);
        if (reservations == null)
        {
            return Result.Fail<List<ReservationModel>>("Reservations not found");
        }

        return Result.Ok(reservations.Select(reservation => new ReservationModel
        {
            Id = reservation.Id,
            PerformerId = reservation.PerformerId,
            UserId = reservation.UserId,
            StartDate = reservation.StartDate,
            EndDate = reservation.EndDate
        }).ToList());

    }
}