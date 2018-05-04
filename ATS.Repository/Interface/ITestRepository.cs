using System;
using System.Collections.Generic;
using ATS.Core.Model;

namespace ATS.Repository.Interface
{
   public interface ITestRepository : ICRUD<TestBank>
    {
        Guid CreateTest(List<QuestionBank> questions);
        Guid UpdateTest(List<QuestionBank> questions);
        List<TestBank> GetTestBanks(Guid userId);
    }
}
