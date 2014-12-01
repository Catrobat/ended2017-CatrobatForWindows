using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources;
using System.Threading;
using System.Collections.Generic;
using Catrobat.IDE.WindowsShared.Services.Common;

namespace Catrobat.IDE.WindowsPhone.Tests.Tests.Services.Storage
{
    [TestClass]
    public class WebCommunicationTests
    {
        private string _currentUserName = "testuserwp2";
        private string _currentUserPassword = "spar1234";
        private string _currentToken = "IzfRZnMZlUZh6KxKoIVpcjqThwnGbR4l";
        private string _currentUserEmail = "";

        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            
        }

        // Tests API that is specified in ApplicationResources.API_BASE_ADRESS

        [TestMethod, TestCategory("Services"), TestCategory("ExcludeGated")]
        public async Task LoadOnlineProjectsAsyncTest()
        {
            var webCommunicationService = new WebCommunicationServiceWindowsShared();
            string filterText = "";
            int offset = 0;
            int count = 5;
            CancellationTokenSource taskCancellation = new CancellationTokenSource();
            List<OnlineProgramHeader> projects = await webCommunicationService.LoadOnlineProgramsAsync(filterText, offset, count, taskCancellation.Token);
            Assert.AreEqual(count, projects.Count);
        }

        [TestMethod, TestCategory("Services"), TestCategory("ExcludeGated")]
        public async Task AsyncCheckTokenAsyncTest()
        {
            var webCommunicationService = new WebCommunicationServiceWindowsShared();

            JSONStatusResponse statusResponse = await webCommunicationService.CheckTokenAsync(_currentUserName, _currentToken, "de");
            Assert.AreEqual(StatusCodes.ServerResponseOk, statusResponse.statusCode);
        }

        [TestMethod, TestCategory("Services"), TestCategory("ExcludeGated")]
        public async Task LoginAsyncTest()
        {
            var webCommunicationService = new WebCommunicationServiceWindowsShared();

            JSONStatusResponse statusResponse = await webCommunicationService.LoginOrRegisterAsync(_currentUserName, _currentUserPassword, _currentUserEmail, "de", "AT");
            Assert.AreEqual(StatusCodes.ServerResponseOk, statusResponse.statusCode);
        }
    }
}
