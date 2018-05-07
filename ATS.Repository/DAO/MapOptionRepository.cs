﻿using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;

namespace ATS.Repository.DAO
{
    public class MapOptionRepository : BaseRepository, IMapOptionRepository
    {
        public bool Create(QuestionOptionMapModel input)
        {
            bool isCreated = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (input != null)
                        {
                            CreateTask(input, context);
                            dbContextTransaction.Commit();
                            isCreated = true;
                        }
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
                return isCreated;
            }
        }
        public void CreateTask(QuestionOptionMapModel input, ATSDBContext context)
        {
            if (input != null)
            {
                input.Id = Guid.NewGuid();
                QuestionOptionMapping mapQues = new QuestionOptionMapping
                {
                    Id = input.Id,
                    QId = input.QId,
                    OptionKeyId = input.OptionKeyId,
                    Answer = input.Answer
                };
                context.QuestionOptionMapping.Add(mapQues);
                context.SaveChanges();
            }
        }
        public bool Delete(QuestionOptionMapModel input)
        {
            bool isDeleted = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        DeleteTask(input, context);
                        dbContextTransaction.Commit();
                        isDeleted = true;
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
                return isDeleted;
            }
        }
        public void DeleteTask(QuestionOptionMapModel input, ATSDBContext context)
        {
            QuestionOptionMapping dataFound = context.QuestionOptionMapping.Where(x => x.Id == input.Id).FirstOrDefault();
            if (dataFound != null)
            {
                context.QuestionOptionMapping.Remove(dataFound);
                context.SaveChanges();
            }
        }
        public QuestionOptionMapModel Retrieve(QuestionOptionMapModel input)
        {
            QuestionOptionMapModel result = null;
            using (var context = GetConnection())
            {
                try
                {
                    //result = context.QuestionOptionMapping.Where(x => x.Id == input.Id).FirstOrDefault();
                }
                catch
                {
                    throw;
                }
                return result;
            }
        }
        public List<QuestionOptionMapModel> Select(params object[] inputs)
        {
            throw new NotImplementedException();
        }
        public bool Update(QuestionOptionMapModel input)
        {
            bool isUpdated = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        UpdateTask(input, context);
                        dbContextTransaction.Commit();
                        isUpdated = true;
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
                return isUpdated;
            }
        }
        public void UpdateTask(QuestionOptionMapModel input, ATSDBContext context)
        {
            QuestionOptionMapping dataFound = context.QuestionOptionMapping.Where(x => x.Id == input.Id).FirstOrDefault();
            if (dataFound != null)
            {
                //updation start
                dataFound.Answer = input.Answer;
                //updateion end
                context.SaveChanges();
            }
        }
    }
}
