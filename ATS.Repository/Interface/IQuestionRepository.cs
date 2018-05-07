using ATS.Core.Model;
using ATS.Repository.DAO;
using ATS.Repository.Model;

namespace ATS.Repository.Interface
{
    public interface IQuestionRepository : ICRUD<QuestionBankModel>
    {
        void CreateTask(QuestionBankModel input, ATSDBContext context);
        void UpdateTask(QuestionBankModel input, ATSDBContext context);
        void DeleteTask(QuestionBankModel input, ATSDBContext context);
    
    }
}
