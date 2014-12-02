using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Storage;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Resources.Localization;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Formula
{
    [TestClass]
    public class AddNewLocalVariableViewTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Lazy);
        }

        [TestMethod, TestCategory("ViewModels.Formula")]
        public void SaveActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(AddNewLocalVariableViewModel);

            var viewModel = new AddNewLocalVariableViewModel
            {
                UserVariableName = "TestUserVariableName"
            };
            var program = new Program
            {
                Name = "TestProgram"
            };
            var messageProgramm = new GenericMessage<Program>(program);
            Messenger.Default.Send(messageProgramm, ViewModelMessagingToken.CurrentProgramChangedListener);
            var localVariable = new LocalVariable
            {
                Name = "TestLocalVariable"
            };
            var sprite = new Sprite
            {
                IsSelected = true
            };
            sprite.LocalVariables.Add(localVariable);
            var messageSprite = new GenericMessage<Sprite>(sprite);
            Messenger.Default.Send(messageSprite, ViewModelMessagingToken.CurrentSpriteChangedListener);

            viewModel.SaveCommand.Execute(null);

            Assert.AreEqual(2, viewModel.CurrentSprite.LocalVariables.Count);
            Assert.AreEqual("TestUserVariableName", viewModel.CurrentSprite.LocalVariables[1].Name);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Formula")]
        public void SaveActionNameAlreadyUsedLocallyTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(AddNewLocalVariableViewModel);

            var viewModel = new AddNewLocalVariableViewModel
            {
                UserVariableName = "TestUserVariableName"
            };
            var program = new Program
            {
                Name = "TestProgram"
            };
            var messageProgramm = new GenericMessage<Program>(program);
            Messenger.Default.Send(messageProgramm, ViewModelMessagingToken.CurrentProgramChangedListener);
            var localVariable = new LocalVariable
            {
                Name = "TestUserVariableName"
            };
            var sprite = new Sprite
            {
                IsSelected = true
            };
            sprite.LocalVariables.Add(localVariable);
            var messageSprite = new GenericMessage<Sprite>(sprite);
            Messenger.Default.Send(messageSprite, ViewModelMessagingToken.CurrentSpriteChangedListener);

            viewModel.SaveCommand.Execute(null);

            Assert.AreEqual(1, viewModel.CurrentSprite.LocalVariables.Count);
            Assert.AreEqual("TestUserVariableName", viewModel.CurrentSprite.LocalVariables[0].Name);
            Assert.AreEqual(NavigationServiceTest.NavigationType.Initial, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(AddNewLocalVariableViewModel), navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Formula")]
        public void SaveActionNameAlreadyUsedGloballyTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(AddNewLocalVariableViewModel);

            var viewModel = new AddNewLocalVariableViewModel
            {
                UserVariableName = "TestUserVariableName"
            };
            var globalVariable = new GlobalVariable
            {
                Name = "TestUserVariableName"
            };
            var program = new Program
            {
                Name = "TestProgram"
            };
            program.GlobalVariables.Add(globalVariable);
            var messageProgramm = new GenericMessage<Program>(program);
            Messenger.Default.Send(messageProgramm, ViewModelMessagingToken.CurrentProgramChangedListener);
            var localVariable = new LocalVariable
            {
                Name = "TestLocalVariableName"
            };
            var sprite = new Sprite
            {
                IsSelected = true
            };
            sprite.LocalVariables.Add(localVariable);
            var messageSprite = new GenericMessage<Sprite>(sprite);
            Messenger.Default.Send(messageSprite, ViewModelMessagingToken.CurrentSpriteChangedListener);

            viewModel.SaveCommand.Execute(null);

            Assert.AreEqual(1, viewModel.CurrentSprite.LocalVariables.Count);
            Assert.AreEqual("TestLocalVariableName", viewModel.CurrentSprite.LocalVariables[0].Name);
            Assert.AreEqual("TestUserVariableName", viewModel.CurrentProgram.GlobalVariables[0].Name);
            Assert.AreEqual(NavigationServiceTest.NavigationType.Initial, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(AddNewLocalVariableViewModel), navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Formula")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(AddNewLocalVariableViewModel);

            var viewModel = new AddNewLocalVariableViewModel();
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Formula")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(AddNewLocalVariableViewModel);

            var viewModel = new AddNewLocalVariableViewModel
            {
                UserVariableName = "TestUserVariableName"
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual(AppResources.Editor_DefaultLocalVariableName, viewModel.UserVariableName);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
