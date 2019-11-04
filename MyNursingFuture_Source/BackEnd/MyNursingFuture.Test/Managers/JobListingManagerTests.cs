using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNursingFuture.BL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.Api.Models;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.Util;
using MyNursingFuture.Api.Filters;
//using Xunit;
namespace MyNursingFuture.BL.Managers.Tests
{
    [TestClass()]
    public class JobListingManagerTests
    {
        [TestMethod()]
        public void CreateJobListingTest()
        {
            var manager = new JobListingManager();

            var listing = new JobListingEntity();
            System.Diagnostics.Debugger.Launch();
            listing.EmployerId = 1;

            var result = manager.CreateJobListingById(listing, 1);

            Assert.IsTrue(result.Success == true);

        }
    }
}