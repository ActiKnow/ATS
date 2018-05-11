using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ATS.Core.Model;
using System.Collections.Generic;
using System.Linq;
using ATS.Core.Helper;
using ATS.Core.Interface;

namespace ATS.UnitTest
{
    [TestClass]
    public class SimpleQueryTest
    {
        [TestMethod]
        public void GetQueryTest()
        {
            SimpleQueryBuilder<TypeDefModel> simpleQry = new SimpleQueryBuilder<TypeDefModel>();
            List<TypeDefModel> data = new List<TypeDefModel>
            {
                new TypeDefModel{ TypeId=new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709") }
            };
            SimpleQueryModel simpleModels = new SimpleQueryModel
            {

                ModelName = nameof(TypeDefModel),
                Properties = new Dictionary<string, object>
                    {
                        { nameof(TypeDefModel.TypeId) , "9D2B0228-4D0D-4C23-8B49-01A698857709" },
                         //{ nameof(TypeDefModel.ParentKey) , "9D2B0228-4D0D-4C23-8B49-01A698857709"}
                    }
            };
            var results = data.Where(simpleQry.GetQuery(simpleModels).Compile()).ToList();
            Assert.IsNotNull(results);
            Assert.AreEqual(results[0].TypeId, data[0].TypeId);
        }

        [TestMethod]
        public void QueryIndexerTest()
        {
            ISimpleQueryable<TypeDefModel, SimpleQueryModel> simpleQry = new SimpleQueryBuilder<TypeDefModel>();
            //SimpleQueryBuilder<TypeDefModel> simpleQry = new SimpleQueryBuilder<TypeDefModel>();
            List<TypeDefModel> data = new List<TypeDefModel>
            {
                new TypeDefModel{ TypeId=new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"),
                    //ParentKey = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709")
                }
            };
            SimpleQueryModel simpleModels = new SimpleQueryModel();
            simpleModels.ModelName = nameof(TypeDefModel);
            simpleModels[nameof(TypeDefModel.TypeId)] = "9D2B0228-4D0D-4C23-8B49-01A698857709";
            object test = simpleModels[nameof(TypeDefModel.TypeId)];
            test = simpleModels[""];
            simpleModels[nameof(TypeDefModel.ParentKey)] = "9D2B0228-4D0D-4C23-8B49-01A698857709";

            var results = data.Where(simpleQry.GetQuery(simpleModels).Compile()).ToList();
            Assert.IsNotNull(results);
            Assert.AreEqual(results[0].TypeId, data[0].TypeId);
        }

        [TestMethod]
        public void CheckNullQuery()
        {
            SimpleQueryBuilder<TypeDefModel> simpleQry = new SimpleQueryBuilder<TypeDefModel>();
            List<TypeDefModel> data = new List<TypeDefModel>
            {
                new TypeDefModel{ TypeId=new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"),
                    //ParentKey = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709")
                }
            };
            SimpleQueryModel simpleModels = null;
            var results = data.Where(simpleQry.GetQuery(simpleModels).Compile()).ToList();
            Assert.IsNotNull(results);
            Assert.AreEqual(results[0].TypeId, data[0].TypeId);
        }

        [TestMethod]
        public void CheckDateQuery()
        {
          
            SimpleQueryBuilder<UserTestHistoryModel> simpleQry = new SimpleQueryBuilder<UserTestHistoryModel>();
            List<UserTestHistoryModel> data = new List<UserTestHistoryModel>
            {
                new UserTestHistoryModel
                {
                    HistoryId =new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"),
                    AssignedDate=new DateTime(2010,05,23),
                    ReusableDate=new DateTime(2010,06,23),
                    LastUsedDate=null
                }
            };
            SimpleQueryModel simpleModels = new SimpleQueryModel
            {
                ModelName = nameof(UserTestHistoryModel)
            };
            simpleModels[nameof(UserTestHistoryModel.HistoryId)] = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709");
            simpleModels[nameof(UserTestHistoryModel.AssignedDate)] = new DateTime(2010, 05, 23);
            simpleModels[nameof(UserTestHistoryModel.ReusableDate)] ="23/06/2010";
            simpleModels[nameof(UserTestHistoryModel.LastUsedDate)] = null;
            var results = data.Where(simpleQry.GetQuery(simpleModels).Compile()).ToList();
            Assert.IsNotNull(results);
            Assert.AreEqual(1 , results.Count);
        }
    }
}
