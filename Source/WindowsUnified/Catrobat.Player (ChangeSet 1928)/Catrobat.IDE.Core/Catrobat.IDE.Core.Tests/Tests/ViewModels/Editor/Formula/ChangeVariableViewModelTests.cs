using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Storage;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Formula
{
    [TestClass]
    public class ChangeVariableViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Lazy);
        }

        public void NavigateToTest()
        {
            var viewModel = new ChangeVariableViewModel
            {
                UserVariableName = "TestUserVariableName"
            };
            var localVariable = new LocalVariable
            {
                Name = "TestVariableName",
                IsSelected = true
            };
            var messageContext = new GenericMessage<Variable>(localVariable);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.SelectedUserVariableChangedListener);
            viewModel.NavigateTo();

            Assert.AreEqual("TestVariableName", viewModel.UserVariableName);
        }

        [TestMethod, TestCategory("ViewModels.Formula")]
        public void SaveActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeVariableViewModel);

            var viewModel = new ChangeVariableViewModel
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
                Name = "TestLocalVariable",
                IsSelected = false
            };
            var localVariableSecond = new LocalVariable // current variable whose name is going to be changed
            {
                Name = "TestVariableNameSecond",
                IsSelected = true
            };
            var sprite = new Sprite
            {
                IsSelected = true
            };
            sprite.LocalVariables.Add(localVariable);
            sprite.LocalVariables.Add(localVariableSecond);
            var messageSprite = new GenericMessage<Sprite>(sprite);
            Messenger.Default.Send(messageSprite, ViewModelMessagingToken.CurrentSpriteChangedListener);
            
            var messageContext = new GenericMessage<Variable>(localVariableSecond);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.SelectedUserVariableChangedListener);
            Assert.AreEqual("TestVariableNameSecond", viewModel.UserVariable.Name);
            Assert.AreEqual("TestLocalVariable", viewModel.SelectedSprite.LocalVariables[0].Name);
            Assert.AreEqual("TestVariableNameSecond", viewModel.SelectedSprite.LocalVariables[1].Name);

            viewModel.SaveCommand.Execute(null);

            Assert.AreEqual("TestUserVariableName", viewModel.UserVariable.Name);
            Assert.AreEqual("TestLocalVariable", viewModel.SelectedSprite.LocalVariables[0].Name);
            Assert.AreEqual("TestUserVariableName", viewModel.SelectedSprite.LocalVariables[1].Name);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Formula")]
        public void SaveActionDublicateNameTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeVariableViewModel);

            var viewModel = new ChangeVariableViewModel
            {
                UserVariableName = "TestUserVariableName" // name that is already used for a variable
            };
            var program = new Program
            {
                Name = "TestProgram"
            };
            var messageProgramm = new GenericMessage<Program>(program);
            Messenger.Default.Send(messageProgramm, ViewModelMessagingToken.CurrentProgramChangedListener);
            var localVariable = new LocalVariable
            {
                Name = "TestUserVariableName",
                IsSelected = false
            };
            var localVariableSecond = new LocalVariable // current variable whose name is going to be changed
            {
                Name = "TestVariableNameSecond",
                IsSelected = true
            };
            var sprite = new Sprite
            {
                IsSelected = true
            };
            sprite.LocalVariables.Add(localVariable);
            sprite.LocalVariables.Add(localVariableSecond);
            var messageSprite = new GenericMessage<Sprite>(sprite);
            Messenger.Default.Send(messageSprite, ViewModelMessagingToken.CurrentSpriteChangedListener);

            var messageContext = new GenericMessage<Variable>(localVariableSecond);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.SelectedUserVariableChangedListener);
            Assert.AreEqual("TestVariableNameSecond", viewModel.UserVariable.Name);
            Assert.AreEqual("TestUserVariableName", viewModel.SelectedSprite.LocalVariables[0].Name);
            Assert.AreEqual("TestVariableNameSecond", viewModel.SelectedSprite.LocalVariables[1].Name);

            viewModel.SaveCommand.Execute(null);

            Assert.AreEqual("TestVariableNameSecond", viewModel.UserVariable.Name);
            Assert.AreEqual("TestUserVariableName", viewModel.SelectedSprite.LocalVariables[0].Name);
            Assert.AreEqual("TestVariableNameSecond", viewModel.SelectedSprite.LocalVariables[1].Name);
            Assert.AreEqual(NavigationServiceTest.NavigationType.Initial, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(ChangeVariableViewModel), navigationService.CurrentView);
            Assert.AreEqual(1, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Formula")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeVariableViewModel);

            var viewModel = new ChangeVariableViewModel
            {
                UserVariableName = "TestUserVariableName"
            };
            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual("TestUserVariableName", viewModel.UserVariableName);
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
            navigationService.CurrentView = typeof(ChangeVariableViewModel);

            var viewModel = new ChangeVariableViewModel
            {
                UserVariableName = "TestUserVariableName"
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.AreEqual("", viewModel.UserVariableName);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
