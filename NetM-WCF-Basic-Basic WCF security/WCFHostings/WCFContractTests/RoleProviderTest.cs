using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCFContractTests.Channels;

namespace WCFContractTests
{
    [TestClass]
    public class RoleProviderTest
    {
        private const string Channel = "ConsoleHostRoleServiceConfiguration";
        //private const string Channel = "IISRoleServiceConfiguration";

        private const string UserName = "Peter";
        private const string Password = "pass2";

        [TestMethod]
        public void Roles()
        {
            var channelFactory = new ChannelFactory<IRoleServiceChannel>(Channel);
            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var roleServiceChannel = channelFactory.CreateChannel())
            {
                foreach (var role in roleServiceChannel.Roles())
                {
                    Console.WriteLine(role.ToString());
                }
            }
        }

        [TestMethod]
        public void GetRole()
        {
            var channelFactory = new ChannelFactory<IRoleServiceChannel>(Channel);
            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var roleServiceChannel = channelFactory.CreateChannel())
            {
                var roles = roleServiceChannel.RolesByUser("1");
                foreach (var role in roles)
                {
                    Console.WriteLine(role.ToString());
                }
            }
        }

        [TestMethod]
        public void CheckAuth()
        {
            var channelFactory = new ChannelFactory<IRoleServiceChannel>(Channel);

            using (var roleServiceChannel = channelFactory.CreateChannel())
            {
                Assert.IsTrue(roleServiceChannel.IsUserExist("Вася", "pass1"));
            }
        }

        [TestMethod]
        public void CheckAuthPasswordFail()
        {
            var channelFactory = new ChannelFactory<IRoleServiceChannel>(Channel);

            using (var roleServiceChannel = channelFactory.CreateChannel())
            {
                Assert.IsTrue(roleServiceChannel.IsUserExist("Вася", "pass12"));
            }
        }

        [TestMethod]
        public void CheckAuthUnexistUserFail()
        {
            var channelFactory = new ChannelFactory<IRoleServiceChannel>(Channel);

            using (var roleServiceChannel = channelFactory.CreateChannel())
            {
                Assert.IsTrue(roleServiceChannel.IsUserExist("Вася1", "pass12"));
            }
        }
    }
}
