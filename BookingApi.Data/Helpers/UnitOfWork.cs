using BookingApi.Data.Data;
using BookingApi.Data.Helpers.Interfaces;
using BookingApi.Data.Interfaces.Repository;

namespace BookingApi.Data.Helpers;

public class UnitOfWork : IUnitOfWork 
{
    private readonly ReservationContext _context;
    
    public ICompanyRepository Company { get; }
    public IBrandRepository Brand { get; }
    public IUserRepository User { get; }
    public IReservationRepository Reservation { get; }
    public IPerformerRepository Performer { get; }
    public IAuditRepository Audit { get; }
    
    public UnitOfWork(ReservationContext context,
        ICompanyRepository companyRepository,
        IBrandRepository brandRepository,
        IUserRepository userRepository,
        IReservationRepository reservationRepository,
        IPerformerRepository performerRepository,
        IAuditRepository auditRepository)
    {
        _context = context;
        Company = companyRepository;
        Brand = brandRepository;
        User = userRepository;
        Reservation = reservationRepository;
        Performer = performerRepository;
        Audit = auditRepository;
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}