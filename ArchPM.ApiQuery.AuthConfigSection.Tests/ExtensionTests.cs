using System;
using ArchPM.ApiQuery.AuthConfigSection.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArchPM.ApiQuery.AuthConfigSection.Tests
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        public void Extension_ToList()
        {
            var manager = ApiQuerySectionManager.Initialize("ValidSectionSample");

            var infos = manager.Section.EnvironmentInfos.ToList<EnvironmentInfoElement>();

            Assert.AreEqual(3, infos.Count);
            Assert.AreEqual(AuthConfigSectionEnvironments.Dev, infos[0].Env);
            Assert.AreEqual(AuthConfigSectionEnvironments.UAT, infos[1].Env);
            Assert.AreEqual(AuthConfigSectionEnvironments.Prod, infos[2].Env);


        }


    }
}
