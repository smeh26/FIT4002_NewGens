using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.DL.Models;

namespace MyNursingFuture.Test.BL.Manager
{
    [TestClass]
    public class SectionManagerTest
    {
        [Ignore]
        [TestMethod]
        public void TestInsertSection()
        {
            var sectionManager =  new SectionsManager();
            var result = sectionManager.Insert(
                new SectionEntity()
                {
                    Name = "section-test-2",
                    Published = false,
                    Sealed = false,
                    Title = "Section Test"
                }
            );
            Assert.AreEqual(result.Success, true);
        }

        [Ignore]
        [TestMethod]
        public void TestUpdatePublishedSection()
        {
            var sectionManager = new SectionsManager();
            var result = sectionManager.SetPublished(20);
            Assert.AreEqual(result.Success, true);
        }
    }
}
