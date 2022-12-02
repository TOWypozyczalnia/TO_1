using App.Data.Entities;

namespace App.Service.Interfaces;

public interface IRecommendationSerivce
{
    public Movie GetRandomRecommendation(int userId);
}