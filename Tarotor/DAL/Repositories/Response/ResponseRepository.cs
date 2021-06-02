namespace Tarotor.DAL.Repositories.Response
{
    public class ResponseRepository :  BaseRepository<Entities.Response>, IResponseRepository
    {
        private AppDbContext _appDbContext;

        public ResponseRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}