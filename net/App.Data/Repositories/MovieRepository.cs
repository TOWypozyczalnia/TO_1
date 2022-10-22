using App.Data.Interfaces;

namespace App.Data.Repositories;

public class MovieRepository : BaseRepository, IMovieRepository
{
    public MovieRepository(IAppDbContext appDbContext) : base(appDbContext) {}
}