﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Model;
using ATS.Repository.DAO;

namespace ATS.Repository.Factory.Question
{
    public class BoolQues : IQuestion
    {
        public void Create(QuestionBankModel input, ATSDBContext context)
        {
            throw new NotImplementedException();
        }

        public List<QuestionBankModel> Select(ATSDBContext context, Func<QuestionBankModel, bool> condition)
        {
            throw new NotImplementedException();
        }

        public void Update(QuestionBankModel input, ATSDBContext context)
        {
            throw new NotImplementedException();
        }
    }
}
