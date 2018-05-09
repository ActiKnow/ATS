﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ATS.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace ATS.UnitTest
{
    [TestClass]
    public class SimpleQueryTest
    {
        [TestMethod]
        public void GetQueryTest()
        {
            SimpleQuery<TypeDefModel> simpleQry = new SimpleQuery<TypeDefModel>();
            List<TypeDefModel> data = new List<TypeDefModel>
            {
                new TypeDefModel{ TypeId=new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"),
                ParentKey = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709")}
            };
            SimpleQueryModel simpleModels = new SimpleQueryModel
            {

                ModelName = nameof(TypeDefModel),
                Properties = new Dictionary<string, object>
                    {
                        { nameof(TypeDefModel.TypeId) , "9D2B0228-4D0D-4C23-8B49-01A698857709" },
                         { nameof(TypeDefModel.ParentKey) , "9D2B0228-4D0D-4C23-8B49-01A698857709"}
                    }
            };
            var results = data.Where(simpleQry.GetQuery(simpleModels).Compile()).ToList();
            Assert.IsNotNull(results);
            Assert.AreEqual(results[0].TypeId, data[0].TypeId);
        }

        [TestMethod]
        public void QueryIndexerTest()
        {
            SimpleQuery<TypeDefModel> simpleQry = new SimpleQuery<TypeDefModel>();
            List<TypeDefModel> data = new List<TypeDefModel>
            {
                new TypeDefModel{ TypeId=new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"),
                ParentKey = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709")}
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
    }
}