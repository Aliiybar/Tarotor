namespace Tarotor.DAL.Repositories.RequestProfile
{
    public class RequestProfileRepository :  BaseRepository<Entities.RequestProfile>, IRequestProfileRepository
    {
        private AppDbContext _appDbContext;

        public RequestProfileRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}