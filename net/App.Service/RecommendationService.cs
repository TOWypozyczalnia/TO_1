using App.Data.Entities;
using App.Data.Interfaces;
using App.Service.Interfaces;

namespace APP.Service;

public class RecommendationService : IRecommendationSerivce
{
    private readonly ILoggedUserRepository _loggedUserRepository;
    private readonly IMovieRepository _movieRepository;

    public RecommendationService(ILoggedUserRepository loggedUserRepository, IMovieRepository movieRepository)
    {
        _loggedUserRepository = loggedUserRepository;
        _movieRepository = movieRepository;
    }
    
    public Movie GetRandomRecommendation(int userId)
    {
        var similarUser = _loggedUserRepository.GetUserSimilarities(userId).MaxBy(s => s.Count);
        var similarUserMovies = _movieRepository.MoviesWatchedByUser(similarUser.UserId);
        var userMovies = _movieRepository.MoviesWatchedByUser(userId);
        var recommendations = similarUserMovies.Except(userMovies);
        var recommendation = recommendations.FirstOrDefault();
        return recommendation == null
            ? _movieRepository.GetAllAsync().FirstOrDefault()
            : recommendation;
    }
}