namespace Tarotor.DAL.Repositories.Request
{
    public class RequestRepository :  BaseRepository<Entities.Request>, IRequestRepository
    {
        private AppDbContext _appDbContext;

        public RequestRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}