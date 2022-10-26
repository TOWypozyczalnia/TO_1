using App.Data.Entities;
using App.Data.Interfaces;

namespace App.Data.Repositories;

public class MovieRepository : BaseRepository<Movie, int>, IMovieRepository
{
    public MovieRepository(IAppDbContext appDbContext) : base(appDbContext) {}
}