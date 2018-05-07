using ATS.Core.Model;
using ATS.Repository.DAO;


namespace ATS.Repository.Interface
{
    interface IOptionRepository : ICRUD<QuestionOptionModel>
    {
        void CreateTask(ref QuestionOptionModel input, ATSDBContext context);
        void UpdateTask(QuestionOptionModel input, ATSDBContext context);
        void DeleteTask(QuestionOptionModel input, ATSDBContext context);
    }
}
