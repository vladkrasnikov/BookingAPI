using BookingApi.Data.Models;

namespace BookingApi.Data.Interfaces.Repository;

public interface IAuditRepository
{
    public Task CreateAsync(string auditEvent, Audit audit, User userEntity);
}