using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ATS.Repository.Model;
using ATS.Core.Model;
using ATS.Core.Helper;

namespace ATS.UnitTest
{
    /// <summary>
    /// Summary description for UtilityTest
    /// </summary>
    [TestClass]
    public class UtilityTest
    {
        public UtilityTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CopyEntityTest()
        {
            List<TypeDef> type = new List<TypeDef>();
            List<TypeDefModel> test = new List<TypeDefModel> { new TypeDefModel { TypeId = Guid.Empty, Description = "xxxxx" } ,
                new TypeDefModel { TypeId = Guid.Empty, Description = "aaaaa" } };
            Utility.CopyEntity(out type, test);
            Assert.IsNotNull(type);
            Assert.AreEqual(type[0].TypeId, Guid.Empty);
        }
    }
}
