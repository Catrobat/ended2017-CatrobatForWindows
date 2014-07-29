#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

//#include "Project.h"
//#include "ProjectDaemon.h"
//#include "Object.h"
//#include "StartScript.h"
//#include "UserVariable.h"
//
//#include "ChangeSizeByBrick.h"

#include <time.h>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace Catrobat_Player_WindowsPhone_Tests
{
	TEST_CLASS(Variables)
	{
    private:
        #pragma region Local Variables Declaration

        //string					m_applicationBuildName;
        //int						m_applicationBuildNumber;
        //string					m_applicationName;
        //string					m_applicationVersion;
        //string					m_catrobatLanguageVersion;
        //time_t					m_dateTimeUpload;
        //string					m_description;
        //string					m_deviceName;
        //string					m_mediaLicense;
        //string					m_platform;
        //int						m_platformVersion;
        //string					m_programLicense;
        //string					m_programName;
        //string					m_remixOf;
        //int						m_screenHeight;
        //int						m_screenWidth;
        //vector<string>*			m_tags;
        //string					m_url;
        //string					m_userHandle;

        //#pragma endregion

        //Project *m_project;
//
//        TEST_METHOD_INITIALIZE(TestInitialization)
//        {
//            #pragma region Local Variables Initialization
//
//            m_applicationBuildName      = "testProjectApplicationBuildName";
//            m_applicationBuildNumber    = 0;
//            m_applicationName           = "testProjectApplicationName";
//            m_applicationVersion        = "1.0";
//            m_catrobatLanguageVersion   = "1.0";
//            m_description               = "testProjectDescription";
//            m_deviceName                = "testDevice";
//            m_mediaLicense              = "testProjectMediaLicense";
//            m_platform                  = "testPlatform";
//            m_platformVersion           = 1;
//            m_programLicense            = "TestProjectProgramLicense";
//            m_programName               = "TestProjectProgramName";
//            m_remixOf                   = "remixOfTestProject";
//            m_screenHeight              = 100;
//            m_screenWidth               = 200;
//            m_tags                      = new vector<string>();
//            m_url                       = "testProjectURL";
//            m_userHandle                = "testProjectUserHandle";
//
//            time(&m_dateTimeUpload);
//            m_tags->push_back("testProjectTag1");
//            m_tags->push_back("testProjectTag2");
//
//            #pragma endregion
//
//            m_project = new Project(m_applicationBuildName,
//                m_applicationBuildNumber,
//                m_applicationName,
//                m_applicationVersion,
//                m_catrobatLanguageVersion,
//                m_dateTimeUpload,
//                m_description,
//                m_deviceName,
//                m_mediaLicense,
//                m_platform,
//                m_platformVersion,
//                m_programLicense,
//                m_programName,
//                m_remixOf,
//                m_screenHeight,
//                m_screenWidth,
//                m_tags,
//                m_url,
//                m_userHandle
//                );
//
//            ProjectDaemon::Instance()->SetProject(m_project);
//        }
//
	public:
//		
//		TEST_METHOD(Variables_ObjectVariable_Valid)
//		{
//			string variableName = "testVariable";
//            UserVariable *variable = new UserVariable(variableName, "10");
//            FormulaTree *formulaTree = new FormulaTree("USER_VARIABLE", variableName);
//			Object *object = new Object("TestObject"); 
//            object->AddVariable(variable->GetName(), variable);
//
//			StartScript *script = new StartScript(object);
//            ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);
//
//            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
//            brick->Execute();
//            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 110.0f));
//		}
//
//        TEST_METHOD(Variables_ObjectVariable_InValid)
//		{
//			string variableName = "testVariable";
//            string inValidVariableName = "invalidTestVariableName";
//            UserVariable *variable = new UserVariable(variableName, "10");
//            FormulaTree *formulaTree = new FormulaTree("USER_VARIABLE", inValidVariableName);
//			Object *object = new Object("TestObject"); 
//            object->AddVariable(variable->GetName(), variable);
//
//			StartScript *script = new StartScript(object);
//            ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);
//
//            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
//            brick->Execute();
//            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
//		}
//
//        TEST_METHOD(Variables_ProjectVariable_Valid)
//		{
//			string variableName = "testVariable";
//            UserVariable *variable = new UserVariable(variableName, "10");
//            FormulaTree *formulaTree = new FormulaTree("USER_VARIABLE", variableName);
//			Object *object = new Object("TestObject"); 
//
//            m_project->AddVariable(variable->GetName(), variable);
//
//			StartScript *script = new StartScript(object);
//            ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);
//
//            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
//            brick->Execute();
//            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 110.0f));
//		}
//
//        TEST_METHOD(Variables_ProjectVariable_InValid)
//		{
//			string variableName = "testVariable";
//            string inValidVariableName = "invalidTestVariableName";
//            UserVariable *variable = new UserVariable(variableName, "10");
//            FormulaTree *formulaTree = new FormulaTree("USER_VARIABLE", inValidVariableName);
//			Object *object = new Object("TestObject"); 
//
//            m_project->AddVariable(variable->GetName(), variable);
//
//			StartScript *script = new StartScript(object);
//            ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);
//
//            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
//            brick->Execute();
//            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
//		}
	};
}