using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Common;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.ViewModels;
using System.Globalization;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Main
{
    [TestClass]
    public class ProgramDetailViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<WebCommunicationTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.CultureService.SetCulture(new CultureInfo("en"));
        }

        [TestMethod, TestCategory("GatedTests")]
        public void EditCurrentProjectActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProgramDetailViewModel);

            var viewModel = new ProgramDetailViewModel();
            viewModel.EditCurrentProjectCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(SpritesViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount); 
        }

        [TestMethod, TestCategory("GatedTests")]
        public void UploadCurrentProjectActionTest()
        {
            //TODO check messages for different responses - e.g. wrong password or http-request failed
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProgramDetailViewModel);

            var viewModel = new ProgramDetailViewModel();
            var localSettings = new LocalSettings();
            var context = new CatrobatContext
            {
                LocalSettings = localSettings,
                CurrentToken = "TestToken",
                CurrentUserName = "TestUser",
                CurrentUserEmail = ""
            };
            var messageContext = new GenericMessage<CatrobatContextBase>(context);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.ContextListener);

            viewModel.UploadCurrentProjectCommand.Execute(null);
            
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(UploadProgramViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }

        [TestMethod/*, TestCategory("GatedTests")*/]
        public void PlayCurrentProjectActionTest()
        {
            //Launch Player
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("GatedTests")]
        public void RenameProjectActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProgramDetailViewModel);

            var viewModel = new ProgramDetailViewModel();
            viewModel.RenameProjectCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(ProgramSettingsViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);  
        }

        //[TestMethod, TestCategory("GatedTests")]
        //public void PinLocalProjectActionTest()
        //{
        //    // currently not in use
        //}

        //[TestMethod, TestCategory("GatedTests")]
        //public void ShareLocalProjectActionTest()
        //{
        //    // currently not in use
        //}
    }
}
