using System.Data;
using App.Data.Entities;
using App.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories;

public class LoggedUserRepository : BaseRepository<LoggedUser, int>, ILoggedUserRepository
{
    public LoggedUserRepository(IAppDbContext appDbContext) : base(appDbContext) {}
    public List<UserSimilarity> GetUserSimilarities(int userId)
    {
        return AppDbContext
            .Set<UserSimilarity, int>()
            .FromSqlRaw(
                "SELECT * FROM dbo.fntGetUserSimilarites(@Id)",
                new[] { new SqlParameter("Id", SqlDbType.Int) { Value = userId } }.ToArray())
            .ToList();
    }
}