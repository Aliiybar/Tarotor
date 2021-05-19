namespace Tarotor.DAL.Repositories.Template
{
    public class TemplateRepository :  BaseRepository<Entities.Template>, ITemplateRepository
    {
        private AppDbContext _appDbContext;

        public TemplateRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}