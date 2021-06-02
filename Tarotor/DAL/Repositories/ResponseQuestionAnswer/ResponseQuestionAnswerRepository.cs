namespace Tarotor.DAL.Repositories.ResponseQuestionAnswer
{
    public class ResponseQuestionAnswerRepository :  BaseRepository<Entities.ResponseQuestionAnswer>, IResponseQuestionAnswerRepository
    {
        private AppDbContext _appDbContext;

        public ResponseQuestionAnswerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}