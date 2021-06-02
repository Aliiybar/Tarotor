namespace Tarotor.DAL.Repositories.ResponseQuestion
{
    public class ResponseQuestionRepository :  BaseRepository<Entities.ResponseQuestion>, IResponseQuestionRepository
    {
        private AppDbContext _appDbContext;

        public ResponseQuestionRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}