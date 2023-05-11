using BookingApi.Data.Interfaces.Repository;

namespace BookingApi.Data.Helpers.Interfaces;

public interface IUnitOfWork: IAsyncDisposable {
    ICompanyRepository Company {
        get;
    }
    
    IBrandRepository Brand {
        get;
    }
    
    IUserRepository User {
        get;
    }
    
    IReservationRepository Reservation {
        get;
    }
        
    IPerformerRepository Performer {
        get;
    }
    
    IAuditRepository Audit {
        get;
    }
    
    Task<int> SaveAsync();
}