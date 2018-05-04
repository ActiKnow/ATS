using System;
using System.Collections.Generic;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Interface
{
   public interface ITestRepository
    {
        Guid CreateTest(List<QuestionBank> questions);
        Guid UpdateTest(List<QuestionBank> questions);
        List<TestBank> GetTestBanks(Guid userId);
    }
}
