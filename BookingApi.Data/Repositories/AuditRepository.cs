using BookingApi.Data.Data;
using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;

namespace BookingApi.Data.Repositories;

public class AuditRepository : IAuditRepository
{
    private readonly ReservationContext _context;
    public AuditRepository(ReservationContext context)
    {
        _context = context;
    }
    
    public async Task CreateAsync(string auditEvent, Audit audit, User userEntity)
    {
        audit.Timestamp = DateTime.Now;
        audit.Event = auditEvent;
        audit.EntityType = userEntity.GetType().ToString();
        audit.By = userEntity.EmailAddress;
        audit.EntityId = userEntity.Id;
        await _context.AddAsync(audit);
    }
}