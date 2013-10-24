#region

using System;
using NUnit.Framework;
using SocialNetwork.Core.Helpers;

#endregion

namespace SocialNetwork.Tests
{
    [TestFixture]
    public class StringHelperTests
    {
        [Test]
        public void ComputeStringHashShouldThrowOnEmptyValue()
        {
            Assert.Throws<ArgumentNullException>(() => "".ComputeStringHash());
        }

        [Test]
        public void ComputeStringHash()
        {
            const string testStr = "test string";
            var hash = testStr.ComputeStringHash();

            Assert.AreEqual("6f8db599de986fab7a21625b7916589c", hash);
        }
    }
}