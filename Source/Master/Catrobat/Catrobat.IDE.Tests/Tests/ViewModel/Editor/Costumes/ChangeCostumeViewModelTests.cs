using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Costumes;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Costumes;
using Catrobat.IDE.Tests.Services;
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
            ServiceLocator.UnRegisterAll();
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.Register<PictureServiceTest>(TypeCreationMode.Normal);
        }

        [TestMethod] //, TestCategory("GatedTests")]
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

        [TestMethod] //, TestCategory("GatedTests")]
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

        [TestMethod] //, TestCategory("GatedTests")]
        public void EditCostumeActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 2;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeCostumeViewModel);

            var pictureService = (PictureServiceTest)ServiceLocator.PictureService;
            pictureService.NextMethodAction = PictureServiceTest.MethodAction.Success;

            var changeCostumeViewModel = new ChangeCostumeViewModel();
            var costume = new Costume { Name = "TestCostume", FileName = "TestFilename" };
            var project = new Project();

            var messageContext = new GenericMessage<Costume>(costume);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CostumeListener);
            var messageContext2 = new GenericMessage<Project>(project);
            Messenger.Default.Send(messageContext2, ViewModelMessagingToken.CurrentProjectChangedListener);

            changeCostumeViewModel.EditCostumeCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod] //, TestCategory("GatedTests")]
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
