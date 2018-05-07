using ATS.Core.Model;
using ATS.Repository.DAO;
using ATS.Repository.Model;

namespace ATS.Repository.Interface
{
    interface IOptionRepository : ICRUD<QuestionOptionModel>
    {
        void CreateTask(QuestionOptionModel input, ATSDBContext context);
        void UpdateTask(QuestionOptionModel input, ATSDBContext context);
        void DeleteTask(QuestionOptionModel input, ATSDBContext context);
    }
}
