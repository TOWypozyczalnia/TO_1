using App.Data.Entities;

namespace App.Data.Interfaces;

public interface IMovieRepository : IBaseRepository<Movie, int>
{
    List<Movie> MoviesWatchedByUser(int userId);
}