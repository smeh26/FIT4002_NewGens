using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;

namespace MyNursingFuture.Test.BL.Manager
{
    [TestClass]
    public class AdministratorsManagerTest
    {
        [Ignore]
        [TestMethod]
        public void TestRegister()
        {
            var adminManager = new AdministratorsManager();
            var result = adminManager.Insert(
                new AdministratorEntity()
                {
                    Username = "admin",
                    Password = "sabex421"
                }
            );
            Assert.AreEqual(result.Success, true);
        }
        [Ignore]
        [TestMethod]
        public void TestLogin()
        {
            var adminManager = new AdministratorsManager();
            var result = adminManager.LogIn(
                new AdministratorEntity()
                {
                    Username = "admin",
                    Password = "sabex421"
                }
            );
            Assert.AreEqual(result.Success, true);
        }
        [Ignore]
        [TestMethod]
        public void TestToken()
        {
            var cred = new CredentialsManager();
            var result =
                cred.ValidateAdminToken(
                    "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJBZG1pbnRyYXRvcklkIjoxLCJVc2VybmFtZSI6ImFkbWluIiwiZXhwIjoxNTAxNjgxNzIxLCJpYXQiOjE0OTkwODk3MjF9.WYthz69O9h-kyj5LNw0J3ob3XvJOrHbjSzGFjljtcnA");
            
            Assert.AreEqual(result.Success, true);
        }
    }
}
