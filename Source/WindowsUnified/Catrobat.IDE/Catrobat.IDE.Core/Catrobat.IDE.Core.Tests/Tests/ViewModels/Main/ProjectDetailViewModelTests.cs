using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
//using Catrobat.IDE.Core.Tests.Services.Storage;
//using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels.Main;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModel.Editor.Costumes
{
    [TestClass]
    public class ProjectDetailViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            //ServiceLocator.Register<PictureServiceTest>(TypeCreationMode.Normal);
            //ServiceLocator.Register<StorageFactoryTest>(TypeCreationMode.Normal);
            //ServiceLocator.Register<StorageTest>(TypeCreationMode.Normal);
            //ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Normal);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void EditCurrentProjectActionTest()
        {
            Assert.AreEqual(0, 1);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void UploadCurrentProjectActionTest()
        {
            Assert.AreEqual(0, 1);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void PlayCurrentProjectActionTest()
        {
            Assert.AreEqual(0, 1);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void PinLocalProjectActionTest()
        {
            Assert.AreEqual(0, 1);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void ShareLocalProjectActionTest()
        {
            Assert.AreEqual(0, 1);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void RenameProjectActionTest()
        {
            Assert.AreEqual(0, 1);
        }
        

        [TestMethod, TestCategory("GatedTests")]
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

        [TestMethod, TestCategory("GatedTests")]
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


        [TestMethod, TestCategory("GatedTests")]
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
