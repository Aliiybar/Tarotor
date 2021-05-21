namespace Tarotor.DAL.Repositories.Content
{
    public class ContentRepository :  BaseRepository<Entities.Content>, IContentRepository
    {
        private AppDbContext _appDbContext;

        public ContentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}