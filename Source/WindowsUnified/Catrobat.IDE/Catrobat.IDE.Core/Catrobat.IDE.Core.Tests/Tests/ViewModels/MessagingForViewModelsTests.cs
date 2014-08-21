using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor;
using Catrobat.IDE.Core.ViewModels.Editor.Actions;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.ViewModels.Settings;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Tests.SampleData;
using System.Globalization;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels
{
    [TestClass]
    public class MessagingForViewModelsTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<FormulaKeyboardViewModel>(TypeCreationMode.Lazy);
        }

        //LoadSettings,
        //SaveSettings,
        //LookListener,
        //SoundNameListener,
        //CurrentSpriteChangedListener,
        //SpriteNameListener,
        //BroadcastObjectListener,
        //BroadcastMessageListener,
        //ScriptBrickCollectionListener,
        //SelectedBrickListener,
        //CurrentProgramChangedListener,        --done
        //LocalProgramsChangedListener,         --to be tested in MainViewModelTests (UpdateLocalPrograms)
        //SelectedOnlineProgramChangedListener, --done
        //ContextListener,                      --done
        //ThemeChooserListener,
        //DownloadProgramStartedListener,       --tested in MainViewModelTests (private)
        //UploadProgramStartedListener,         --tested in MainViewModelTests (private)
        //SelectedUserVariableChangedListener,  --done
        //ShareProgramHeaderListener,
        //CurrentProgramHeaderChangedListener,  --done
        //LookImageListener,
        //LookImageToSaveListener,
        //ScriptBrickCategoryListener,
        //SoundStreamListener,
        //PlayProgramNameListener,
        //IsPlayerStartFromTileListener,
        //FormulaEvaluated,
        //ClearPageCache,
        //ExportStreamChanged,
        //ToastNotificationActivated            --done

        [TestMethod, TestCategory("ViewModels")]
        public void CurrentProgramChangedTest()
        {
            // Editor
            var editorLoadingViewModel = new EditorLoadingViewModel();

            // Editor.Formular
            var addNewGlobalVariableViewModel = new AddNewGlobalVariableViewModel();
            var addNewLocalVariableViewModel = new AddNewLocalVariableViewModel();
            var changeVariableViewModel = new ChangeVariableViewModel();
            var formulaKeyboardViewModel = new FormulaKeyboardViewModel();
            //var formulaEditorViewModel = new FormulaEditorViewModel();
            var variableSelectionViewModel = new VariableSelectionViewModel();

            // Editor.Looks
            var changeLookViewModel = new ChangeLookViewModel();
            var lookNameChooserViewModel = new LookNameChooserViewModel();
            
            // Editor.Sounds
            var soundNameChooserViewModel = new SoundNameChooserViewModel();

            // Editor.Sprites
            var addNewSpriteViewModel = new AddNewSpriteViewModel();
            var spriteEditorViewModel = new SpriteEditorViewModel();
            var spritesViewModel = new SpritesViewModel();

            // Main
            var addNewProgramViewModel = new AddNewProgramViewModel();
            var mainViewModel = new MainViewModel();
            var programExportViewModel = new ProgramExportViewModel();
            var programSettingsViewModel = new ProgramSettingsViewModel();

            // Service
            var uploadProgramViewModel = new UploadProgramViewModel();


            var program = new Program
            {
                Name = "TestProgram",
                Description = "TestProgramDescription"
            };
            var messageContext = new GenericMessage<Program>(program);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProgramChangedListener);


            // Editor.Formular
            Assert.AreEqual("TestProgram", addNewGlobalVariableViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", addNewGlobalVariableViewModel.CurrentProgram.Description);
            Assert.AreEqual("TestProgram", addNewLocalVariableViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", addNewLocalVariableViewModel.CurrentProgram.Description);
            Assert.AreEqual("TestProgram", changeVariableViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", changeVariableViewModel.CurrentProgram.Description);
            Assert.AreEqual("TestProgram", formulaKeyboardViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", formulaKeyboardViewModel.CurrentProgram.Description);
            //Assert.AreEqual("TestProgram", formulaEditorViewModel.CurrentProgram.Name);
            //Assert.AreEqual("TestProgramDescription", formulaEditorViewModel.CurrentProgram.Description);
            Assert.AreEqual("TestProgram", variableSelectionViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", variableSelectionViewModel.CurrentProgram.Description);

            // Editor.Looks
            Assert.AreEqual("TestProgram", changeLookViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", changeLookViewModel.CurrentProgram.Description);
            Assert.AreEqual("TestProgram", lookNameChooserViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", lookNameChooserViewModel.CurrentProgram.Description);

            // Editor.Sounds
            Assert.AreEqual("TestProgram", soundNameChooserViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", soundNameChooserViewModel.CurrentProgram.Description);

            // Editor.Sprites
            Assert.AreEqual("TestProgram", addNewSpriteViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", addNewSpriteViewModel.CurrentProgram.Description);
            Assert.AreEqual("TestProgram", spriteEditorViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", spriteEditorViewModel.CurrentProgram.Description);
            Assert.AreEqual("TestProgram", spritesViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", spritesViewModel.CurrentProgram.Description);

            // Main
            Assert.AreEqual("TestProgram", addNewProgramViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", addNewProgramViewModel.CurrentProgram.Description);
            Assert.AreEqual("TestProgram", mainViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", mainViewModel.CurrentProgram.Description);
            Assert.AreEqual("TestProgram", programExportViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", programExportViewModel.CurrentProgram.Description);
            Assert.AreEqual("TestProgram", programSettingsViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", programSettingsViewModel.CurrentProgram.Description);

            // Service
            Assert.AreEqual("TestProgram", uploadProgramViewModel.CurrentProgram.Name);
            Assert.AreEqual("TestProgramDescription", uploadProgramViewModel.CurrentProgram.Description);
        }

        [TestMethod, TestCategory("ViewModels")]
        public void ContextChangedTest()
        {
            // Main
            var mainViewModel = new MainViewModel();
            var programExportViewModel = new ProgramExportViewModel();
            var programDetailViewModel = new ProgramDetailViewModel();

            // Service
            var onlineProgramReportViewModel = new OnlineProgramReportViewModel();
            var uploadProgramForgotPasswordViewModel = new UploadProgramForgotPasswordViewModel();
            var uploadProgramLoginViewModel = new UploadProgramLoginViewModel();
            var uploadProgramNewPasswordViewModel = new UploadProgramNewPasswordViewModel();
            var uploadProgramRegisterViewModel = new UploadProgramRegisterViewModel();
            var uploadProgramViewModel = new UploadProgramViewModel();

            // Settings
            var settingsBrickViewModel = new SettingsBrickViewModel();

            var localSettings = new LocalSettings
            {
                CurrentLanguageString = "de"
            };
            var context = new CatrobatContext
            {
                LocalSettings = localSettings,
                CurrentToken = "TestTokenFromTestSystem_en",
                CurrentUserName = "TestUser",
                CurrentUserEmail = "TestEmail"
            };
            var messageContext = new GenericMessage<CatrobatContextBase>(context);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.ContextListener);

            // Main
            Assert.AreEqual("TestUser", mainViewModel.Context.CurrentUserName);
            Assert.AreEqual("de", mainViewModel.Context.LocalSettings.CurrentLanguageString);
            Assert.AreEqual("TestUser", programExportViewModel.Context.CurrentUserName);
            Assert.AreEqual("de", programExportViewModel.Context.LocalSettings.CurrentLanguageString);
            Assert.AreEqual("TestUser", programDetailViewModel.Context.CurrentUserName);
            Assert.AreEqual("de", programDetailViewModel.Context.LocalSettings.CurrentLanguageString);

            // Service
            //Assert.AreEqual("TestUser", onlineProgramReportViewModel.Context.CurrentUserName);
            //Assert.AreEqual("de", onlineProgramReportViewModel.Context.LocalSettings.CurrentLanguageString);
            Assert.AreEqual("TestUser", uploadProgramForgotPasswordViewModel.Context.CurrentUserName);
            Assert.AreEqual("de", uploadProgramForgotPasswordViewModel.Context.LocalSettings.CurrentLanguageString);
            Assert.AreEqual("TestUser", uploadProgramLoginViewModel.Context.CurrentUserName);
            Assert.AreEqual("de", uploadProgramLoginViewModel.Context.LocalSettings.CurrentLanguageString);
            Assert.AreEqual("TestUser", uploadProgramNewPasswordViewModel.Context.CurrentUserName);
            Assert.AreEqual("de", uploadProgramNewPasswordViewModel.Context.LocalSettings.CurrentLanguageString);
            Assert.AreEqual("TestUser", uploadProgramRegisterViewModel.Context.CurrentUserName);
            Assert.AreEqual("de", uploadProgramRegisterViewModel.Context.LocalSettings.CurrentLanguageString);
            Assert.AreEqual("TestUser", uploadProgramViewModel.Context.CurrentUserName);
            Assert.AreEqual("de", uploadProgramViewModel.Context.LocalSettings.CurrentLanguageString);

            // Settings
            Assert.AreEqual("TestUser", settingsBrickViewModel.Context.CurrentUserName);
            Assert.AreEqual("de", settingsBrickViewModel.Context.LocalSettings.CurrentLanguageString);
        }

        [TestMethod, TestCategory("ViewModels")]
        public void SelectedOnlineProgramChangedTest()
        {
            // Service
            var onlineProgramReportViewModel = new OnlineProgramReportViewModel();

            var onlineProjectHeader = SampleLoader.GetSampleOnlineProjectHeader();

            var messageContext = new GenericMessage<OnlineProgramHeader>(onlineProjectHeader);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.SelectedOnlineProgramChangedListener);

            // Service
            Assert.AreEqual("Radio Fun City", onlineProgramReportViewModel.SelectedOnlineProgram.ProjectName);
        }

        [TestMethod, TestCategory("ViewModels")]
        public void SelectedUserVariableChangedTest()
        {

        
        }

        [TestMethod, TestCategory("ViewModels")]
        public void CurrentProgramHeaderChangedTest()
        {
            // Main
            var programDetailViewModel = new ProgramDetailViewModel();
            var programStettingsViewModel = new ProgramSettingsViewModel();

            var programHeader = new LocalProgramHeader
            {
                ProjectName = "TestProgramHeader"
            };

            var messageContext = new GenericMessage<LocalProgramHeader>(programHeader);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProgramHeaderChangedListener);

            // Main
            Assert.AreEqual("TestProgramHeader", programDetailViewModel.CurrentProgramHeader.ProjectName);
            Assert.AreEqual("TestProgramHeader", programStettingsViewModel.CurrentProgramHeader.ProjectName);
        }

        [TestMethod, TestCategory("ViewModels"), TestCategory("ExcludeGated")]
        public void ToastNotificationActivatedTest()
        {
            Assert.AreEqual(0, "Hard to test - test not fully implemented");
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(MainViewModel);

            var mainViewModel = new MainViewModel();

            var tagEnum = ToastTag.ImportFin;
            var toastActivatedMessage = new GenericMessage<ToastTag>(tagEnum);
            Messenger.Default.Send(toastActivatedMessage, ViewModelMessagingToken.ToastNotificationActivated);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(MainViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);
        }
    }
}
