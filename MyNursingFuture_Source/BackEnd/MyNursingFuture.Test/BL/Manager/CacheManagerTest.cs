using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNursingFuture.BL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.Test.BL.Manager
{
    [TestClass]
    public class CacheManagerTest
    {
        [Ignore]
        [TestMethod]
        public void TestGet()
        {
            var manager = new CacheManager();
            manager.Add(CacheTypes.Framework, "ASD");
            var result = manager.Get(CacheTypes.Framework);
            Assert.AreEqual(result.ToString(), "ASD");
        }
    }
}
