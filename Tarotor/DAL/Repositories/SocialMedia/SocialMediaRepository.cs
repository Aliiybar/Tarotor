namespace Tarotor.DAL.Repositories.SocialMedia
{
    public class SocialMediaRepository :  BaseRepository<Entities.SocialMedia>, ISocialMediaRepository
    {
        private AppDbContext _appDbContext;

        public SocialMediaRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}