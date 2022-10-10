using App.Data.Interfaces;

namespace App.Data.Repositories;

public class DirectorRepository : BaseRepository
{
    public DirectorRepository(IAppDbContext appDbContext) : base(appDbContext) {}
}