using System;
using ArchPM.ApiQuery.AuthConfigSection.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArchPM.ApiQuery.AuthConfigSection.Tests
{
    [TestClass]
    public class ValidSectionSampleTests
    {
        [TestMethod]
        public void ValidSectionSample_WhenAllFieldsValidThenReturnValid()
        {
            var manager = ApiQuerySectionManager.Initialize("ValidSectionSample");

            Assert.IsNotNull(manager.Section);
            Assert.IsNotNull(manager.Section.Auths);
            Assert.IsNotNull(manager.Section.EnvironmentInfos);

            var auths = manager.Section.Auths;
            Assert.AreEqual(1, auths.Count);
            Assert.AreEqual(AuthConfigSectionEnvironments.UAT, auths[0].Env);
            Assert.AreEqual("SodexoPlus", auths[0].Application);
            Assert.AreEqual("Utils", auths[0].Service);
            Assert.AreEqual("*", auths[0].Action);
            Assert.AreEqual("TEST_User", auths[0].Username);
            Assert.AreEqual("sad123", auths[0].Password);

            var envs = manager.Section.EnvironmentInfos;
            Assert.AreEqual(3, envs.Count);
            Assert.AreEqual(AuthConfigSectionEnvironments.Dev, envs[0].Env);
            Assert.AreEqual("SodexoPlus", envs[0].Application);
            Assert.AreEqual("http://10.10.10.10", envs[0].Url);
            Assert.AreEqual(8040, envs[0].Port);

            Assert.AreEqual(AuthConfigSectionEnvironments.UAT, envs[1].Env);
            Assert.AreEqual("SodexoPlus", envs[1].Application);
            Assert.AreEqual("http://10.10.10.11", envs[1].Url);
            Assert.AreEqual(8041, envs[1].Port);

            Assert.AreEqual(AuthConfigSectionEnvironments.Prod, envs[2].Env);
            Assert.AreEqual("SodexoPlus", envs[2].Application);
            Assert.AreEqual("http://10.10.10.12", envs[2].Url);
            Assert.AreEqual(8042, envs[2].Port);

        }


    }
}
