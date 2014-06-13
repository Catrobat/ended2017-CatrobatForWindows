using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;
using Catrobat.IDE.Tests.Services;
using Catrobat.IDE.Tests.Services.Storage;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.ViewModel.Editor.Costumes
{
    [TestClass]
    public class ChangeCostumeViewModelTests
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

        [TestMethod] // , TestCategory("GatedTests") // TODO: fix test takes very long time on server
        public void SaveActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeCostumeViewModel);

            var changeCostumeViewModel = new ChangeCostumeViewModel();
            var costume = new Costume { Name = "TestCostume", FileName = "TestFilename"};

            var messageContext = new GenericMessage<Costume>(costume);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CostumeListener);
            changeCostumeViewModel.CostumeName = "NewCostumeName";


            changeCostumeViewModel.SaveCommand.Execute(null);

            Assert.IsTrue(changeCostumeViewModel.CostumeName == "NewCostumeName");
            Assert.IsFalse(changeCostumeViewModel.CostumeName == "TestCostume");
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod] // , TestCategory("GatedTests") // TODO: fix test takes very long time on server
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeCostumeViewModel);

            var changeCostumeViewModel = new ChangeCostumeViewModel();
            changeCostumeViewModel.CancelCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod] // , TestCategory("GatedTests") // TODO: fix test takes very long time on server
        public async Task EditCostumeActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 2;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeCostumeViewModel);

            var pictureService = (PictureServiceTest)ServiceLocator.PictureService;
            pictureService.NextMethodAction = PictureServiceStatus.Success;

            var changeCostumeViewModel = new ChangeCostumeViewModel();

            var costume = new Costume { Name = "TestCostume", FileName = "TestFilename", Image = new PortableImage()};
            var messageContext = new GenericMessage<Costume>(costume);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CostumeListener);

            var project = new Project { Name = "TestProject" };
            var messageContext2 = new GenericMessage<Project>(project);
            Messenger.Default.Send(messageContext2, ViewModelMessagingToken.CurrentProjectChangedListener);

            await changeCostumeViewModel.EditCostumeCommand.ExecuteAsync(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod] // , TestCategory("GatedTests") // TODO: fix test takes very long time on server
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeCostumeViewModel);

            var changeCostumeViewModel = new ChangeCostumeViewModel();
            changeCostumeViewModel.CostumeName = "NewCostumeName";
            changeCostumeViewModel.GoBackCommand.Execute(null);

            Assert.IsTrue(changeCostumeViewModel.CostumeName == "");
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
