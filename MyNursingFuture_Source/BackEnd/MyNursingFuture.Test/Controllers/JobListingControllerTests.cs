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
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace ListingAPI.Tests
{
    [TestClass()]
    public class JobListingControllerTests
    {
        [TestMethod()]
        public void CreateJobListingTest()
        {
            //To Implement
            Assert.IsTrue(true);
        }
        [TestMethod()]
        public void GetJSON()
        {
            JSchemaGenerator generator = new JSchemaGenerator();

            JSchema schema = generator.Generate(typeof(JobListingEntity));
            Console.WriteLine(schema.ToString());
            Assert.IsTrue(true);
        }
    }
}