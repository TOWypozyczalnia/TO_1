using App.Data.Entities;

namespace App.Data.Interfaces;

public interface ILoggedUserRepository : IBaseRepository<LoggedUser, int>
{
    List<UserSimilarity> GetUserSimilarities(int userId);
}