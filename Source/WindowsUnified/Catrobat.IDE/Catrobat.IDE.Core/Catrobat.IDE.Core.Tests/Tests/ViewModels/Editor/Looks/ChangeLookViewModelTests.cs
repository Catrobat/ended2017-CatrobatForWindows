using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Looks
{
    [TestClass]
    public class ChangeLookViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.Register<PictureServiceTest>(TypeCreationMode.Normal);
            ServiceLocator.Register<StorageFactoryTest>(TypeCreationMode.Normal);
            ServiceLocator.Register<StorageTest>(TypeCreationMode.Normal);
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Normal);
        }

        [TestMethod, TestCategory("ViewModels.Editor"), TestCategory("ExcludeGated")]
        public void SaveActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeLookViewModel);

            var changeLookViewModel = new ChangeLookViewModel();
            var Look = new Look { Name = "TestLook", FileName = "TestFilename"};

            var messageContext = new GenericMessage<Look>(Look);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.LookListener);
            changeLookViewModel.LookName = "NewLookName";

            changeLookViewModel.SaveCommand.Execute(null);

            Assert.AreEqual("NewLookName", changeLookViewModel.LookName);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Editor"), TestCategory("ExcludeGated")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeLookViewModel);

            var changeLookViewModel = new ChangeLookViewModel();
            changeLookViewModel.CancelCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        //[TestMethod, TestCategory("ViewModels.Editor"), TestCategory("ExcludeGated")]
        //public async Task EditLookActionTest()
        //{
        //    var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
        //    navigationService.PageStackCount = 2;
        //    navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
        //    navigationService.CurrentView = typeof(ChangeLookViewModel);

        //    var pictureService = (PictureServiceTest)ServiceLocator.PictureService;
        //    pictureService.NextMethodAction = PictureServiceStatus.Success;

        //    var changeLookViewModel = new ChangeLookViewModel();

        //    var look = new Look { Name = "TestLook", FileName = "TestFilename", Image = new PortableImage()};
        //    var messageContext = new GenericMessage<Look>(look);
        //    Messenger.Default.Send(messageContext, ViewModelMessagingToken.LookListener);

        //    var project = new Project { Name = "TestProject" };
        //    var messageContext2 = new GenericMessage<Project>(project);
        //    Messenger.Default.Send(messageContext2, ViewModelMessagingToken.CurrentProgramChangedListener);

        //    await changeLookViewModel.EditLookCommand.ExecuteAsync(null);

        //    Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
        //    Assert.AreEqual(null, navigationService.CurrentView);
        //    Assert.AreEqual(0, navigationService.PageStackCount);
        //}

        [TestMethod, TestCategory("ViewModels.Editor")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeLookViewModel);

            var changeLookViewModel = new ChangeLookViewModel();
            changeLookViewModel.LookName = "NewLookName";
            changeLookViewModel.GoBackCommand.Execute(null);

            Assert.AreEqual("", changeLookViewModel.LookName);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
