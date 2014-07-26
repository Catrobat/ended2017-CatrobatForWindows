using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Tests.Tests.Services.Common
{
    [TestClass]
    public class WebCommunicationTests
    {
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("GatedTests")]
        public async Task TestAsyncCheckToken()
        {
            var webCommunicationService = new WebCommunicationService();
            string currentUserName = "testuserwp";
            // with "spar1234" password
            string currentToken = "D4RAzWfoHUjOqeUb9CrNO8laN9aK3ykr";

            JSONStatusResponse statusResponse = await webCommunicationService.CheckTokenAsync(currentUserName, currentToken, "de");
            Assert.AreEqual(StatusCodes.ServerResponseOk, statusResponse.statusCode);
        }
    }
}
