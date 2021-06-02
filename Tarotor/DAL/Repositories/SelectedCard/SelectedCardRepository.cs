namespace Tarotor.DAL.Repositories.SelectedCard
{
    public class SelectedCardRepository :  BaseRepository<Entities.SelectedCard>, ISelectedCardRepository
    {
        private AppDbContext _appDbContext;

        public SelectedCardRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}