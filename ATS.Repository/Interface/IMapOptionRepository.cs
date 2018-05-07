using ATS.Core.Model;
using ATS.Repository.DAO;


namespace ATS.Repository.Interface
{
   public interface IMapOptionRepository : ICRUD<QuestionOptionMapModel>
    {
        void CreateTask(QuestionOptionMapModel input, ATSDBContext context);
        void UpdateTask(QuestionOptionMapModel input, ATSDBContext context);
        void DeleteTask(QuestionOptionMapModel input, ATSDBContext context);
    }
}
