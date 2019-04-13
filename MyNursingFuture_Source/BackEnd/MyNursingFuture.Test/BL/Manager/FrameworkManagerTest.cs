using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNursingFuture.BL.Managers;

namespace MyNursingFuture.Test.BL.Manager
{
    [TestClass]
    public class FrameworkManagerTest
    {
        [Ignore]
        [TestMethod]
        public void TestGet()
        {
            var manager = new FrameworkManager();
            var result = manager.Get();
            Assert.IsTrue(result.Success);
        }
    }
}
