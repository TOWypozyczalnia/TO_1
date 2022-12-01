using App.Data.Entities;
using App.Data.Interfaces;

namespace App.Data.Repositories;

public class ActorRepository : BaseRepository<Actor, int>, IActorRepository
{
    public ActorRepository(IAppDbContext appDbContext) : base(appDbContext) {}
}