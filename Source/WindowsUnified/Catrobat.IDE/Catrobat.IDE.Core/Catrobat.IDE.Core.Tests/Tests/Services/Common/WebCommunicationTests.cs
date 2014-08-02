using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources;
using System.Threading;
using System.Collections.Generic;

namespace Catrobat.IDE.Core.Tests.Tests.Services.Common
{
    [TestClass]
    public class WebCommunicationTests
    {
        private string _currentUserName = "testuserwp";
        private string _currentUserPassword = "spar1234";
        private string _currentToken = "D4RAzWfoHUjOqeUb9CrNO8laN9aK3ykr";
        private string _currentUserEmail = "";

        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        // Tests API that is specified in ApplicationResources.API_BASE_ADRESS

        [TestMethod/*, TestCategory("GatedTests")*/]
        public async Task LoadOnlineProjectsAsyncTest()
        {
            var webCommunicationService = new WebCommunicationService();
            string filterText = "";
            int offset = 0;
            int count = 5;
            CancellationTokenSource taskCancellation = new CancellationTokenSource();
            List<OnlineProgramHeader> projects = await webCommunicationService.LoadOnlineProjectsAsync(filterText, offset, count, taskCancellation.Token);
            Assert.AreEqual(count, projects.Count);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public async Task AsyncCheckTokenAsyncTest()
        {
            var webCommunicationService = new WebCommunicationService();

            JSONStatusResponse statusResponse = await webCommunicationService.CheckTokenAsync(_currentUserName, _currentToken, "de");
            Assert.AreEqual(StatusCodes.ServerResponseOk, statusResponse.statusCode);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public async Task LoginAsyncTest()
        {
            var webCommunicationService = new WebCommunicationService();

            JSONStatusResponse statusResponse = await webCommunicationService.LoginOrRegisterAsync(_currentUserName, _currentUserPassword, _currentUserEmail, "de", "AT");
            Assert.AreEqual(StatusCodes.ServerResponseOk, statusResponse.statusCode);
        }
    }
}
