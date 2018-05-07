﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using ATS.Core.Model;

namespace ATS.Repository.DAO
{
    public class TypeDefRepository : BaseRepository, ITypeRepository
    {
        public bool Create(TypeDefModel input)
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
                            input.TypeId = Guid.NewGuid();
                           // context.TypeDef.Add(input);
                            context.SaveChanges();                            
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

        public bool Delete(TypeDefModel input)
        {
            bool isDeleted = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        TypeDef typeDef = context.TypeDef.AsNoTracking().Where(x => x.TypeId == input.TypeId).FirstOrDefault();
                        if (typeDef != null)
                        {
                            context.TypeDef.Remove(typeDef);
                            context.SaveChanges();
                            
                            dbContextTransaction.Commit();
                            isDeleted = true;
                        }
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

        public TypeDefModel Retrieve(TypeDefModel input)
        {
            TypeDefModel typeDef=null;
            using (var context = GetConnection())
            {
                try
                {
                    //typeDef = context.TypeDef.AsNoTracking().Where(x => x.TypeId == input.TypeId).FirstOrDefault();
                }
                catch
                {
                    throw;
                }
                return typeDef;
            }
        }

        public List<TypeDefModel> Select(params object[] inputs)
        {
            List<TypeDefModel> typeDefs=null;
            using (var context = GetConnection())
            {
                try
                {
                   // typeDefs = context.TypeDef.AsNoTracking().ToList();
                }
                catch
                {
                    throw;
                }
                return typeDefs;
            }
        }

        public bool Update(TypeDefModel input)
        {
            bool isUpdated = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var typeDef=context.TypeDef.AsNoTracking().Where(x=>x.TypeId==input.TypeId).FirstOrDefault();

                        if (typeDef != null)
                        {
                            typeDef.LastUpdatedBy = input.LastUpdatedBy;
                            typeDef.LastUpdatedDate = input.LastUpdatedDate;
                            typeDef.ParentTypeId = input.ParentTypeId;
                            typeDef.Status = input.Status;
                            typeDef.Description = input.Description;
                            typeDef.Value = input.Value;

                            context.SaveChanges();
                            dbContextTransaction.Commit();
                            isUpdated = true;
                        }
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
    }
}
