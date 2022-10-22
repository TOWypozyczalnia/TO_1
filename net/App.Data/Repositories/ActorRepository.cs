using App.Data.Interfaces;

namespace App.Data.Repositories;

public class ActorRepository : BaseRepository, IActorRepository
{
    public ActorRepository(IAppDbContext appDbContext) : base(appDbContext) {}   
}