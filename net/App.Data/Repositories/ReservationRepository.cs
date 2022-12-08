using App.Data.Entities;
using App.Data.Interfaces;

namespace App.Data.Repositories;

public class ReservationRepository : BaseRepository<Reservation, int>, IReservationRepository
{
    public ReservationRepository(IAppDbContext appDbContext) : base(appDbContext) {}
}