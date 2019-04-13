using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.Test.BL.Manager
{
    [Ignore]
    [TestClass]
    public class AspectsManagerTest
    {
        [Ignore]
        [TestMethod]
        public void CopyAspects()
        {
            var am = new AspectsManager();
            var result = am.InsertTest(1, 2);

        }
    }
}
