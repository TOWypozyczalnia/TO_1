using App.Data.Entities;
using App.Data.Interfaces;

namespace App.Data.Repositories;

public class LoggedUserRepository : BaseRepository<LoggedUser, int>, ILoggedUserRepository
{
    public LoggedUserRepository(IAppDbContext appDbContext) : base(appDbContext) {}
}