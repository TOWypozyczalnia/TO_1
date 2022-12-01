using App.Data.Entities;
using App.Data.Interfaces;

namespace App.Data.Repositories;

public class ReviewRepository : BaseRepository<Review, int>, IReviewRepository
{
    public ReviewRepository(IAppDbContext appDbContext) : base(appDbContext) {}   
}