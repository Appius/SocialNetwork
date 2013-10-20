#region

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetwork.Core.Models.Repos;

#endregion

namespace SocialNetwork.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestReadRoles()
        {
            var roleRepository = new RoleRepository();
            var roles = roleRepository.GetAll().Select(item => item.RoleName).ToList();

            var rightRoles = new List<string> {"admin", "user"};
            foreach (var role in roles)
                if (!rightRoles.Contains(role))
                    Assert.Fail("Somithing is wrong... Collections is not same.");
        }
    }
}