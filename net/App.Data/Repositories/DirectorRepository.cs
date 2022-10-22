using App.Data.Interfaces;

namespace App.Data.Repositories;

public class DirectorRepository : BaseRepository, IDirectorRepository
{
    public DirectorRepository(IAppDbContext appDbContext) : base(appDbContext) {}
}