namespace Tarotor.DAL.Repositories.Smtp
{
    public interface ISmtpRepository: IBaseRepository<Entities.Smtp>
    {
        
    }
    public class SmtpRepository :  BaseRepository<Entities.Smtp>, ISmtpRepository
    {
        private AppDbContext _appDbContext;

        public SmtpRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}