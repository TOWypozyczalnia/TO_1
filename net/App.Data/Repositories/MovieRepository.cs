using App.Data.Interfaces;

namespace App.Data.Repositories;

public class MovieRepository : BaseRepository
{
    public MovieRepository(IAppDbContext appDbContext) : base(appDbContext) {}
}