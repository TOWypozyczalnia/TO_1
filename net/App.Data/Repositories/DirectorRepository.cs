using App.Data.Entities;
using App.Data.Interfaces;

namespace App.Data.Repositories;

public class DirectorRepository : BaseRepository<Director, int>, IDirectorRepository
{
    public DirectorRepository(IAppDbContext appDbContext) : base(appDbContext) {}
}