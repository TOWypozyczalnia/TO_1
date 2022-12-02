using App.Data.Entities;
using App.Data.Interfaces;

namespace App.Data.Repositories;

public class MovieRepository : BaseRepository<Movie, int>, IMovieRepository
{
    public MovieRepository(IAppDbContext appDbContext) : base(appDbContext) {}
    public List<Movie> MoviesWatchedByUser(int userId)
    {
        var userMovies = AppDbContext
            .Set<LoggedUserMovie, int>()
            .Where(um => um.UserId == userId);
        return GetAllAsync().Where(m => userMovies.Any(um => um.MovieId == m.Id)).ToList();
    }
}