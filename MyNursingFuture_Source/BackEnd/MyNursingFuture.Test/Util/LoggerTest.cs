using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNursingFuture.Util;

namespace MyNursingFuture.Test.Util
{
    [TestClass]
    public class LoggerTest
    {
        [Ignore]
        [TestMethod]
        public void ThrowException()
        {
            try
            {
                var a = 2;
                var b = 0;
                a = a / b;
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
            
        }
    }
}
