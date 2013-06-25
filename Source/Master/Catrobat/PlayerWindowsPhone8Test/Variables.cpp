#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

#include "Project.h"
#include "ProjectDaemon.h"
#include "Object.h"
#include "StartScript.h"
#include "UserVariable.h"

#include "ChangeSizeByBrick.h"

#include <time.h>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
	TEST_CLASS(Variables)
	{
	public:
		
		TEST_METHOD(Variables_ObjectVariable_Valid)
		{
			string variableName = "testVariable";
            UserVariable *variable = new UserVariable(variableName, "10");
            FormulaTree *formulaTree = new FormulaTree("USER_VARIABLE", variableName);
			string spriteReference = "";
			Object *object = new Object("TestObject"); 
            object->addVariable(variable->Name(), variable);

			StartScript *script = new StartScript(spriteReference, object);
            ChangeSizeByBrick *brick = new ChangeSizeByBrick(spriteReference, formulaTree, script);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            brick->Execute();
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 110.0f));
		}

        TEST_METHOD(Variables_ObjectVariable_InValid)
		{
			string variableName = "testVariable";
            string inValidVariableName = "invalidTestVariableName";
            UserVariable *variable = new UserVariable(variableName, "10");
            FormulaTree *formulaTree = new FormulaTree("USER_VARIABLE", inValidVariableName);
			string spriteReference = "";
			Object *object = new Object("TestObject"); 
            object->addVariable(variable->Name(), variable);

			StartScript *script = new StartScript(spriteReference, object);
            ChangeSizeByBrick *brick = new ChangeSizeByBrick(spriteReference, formulaTree, script);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            brick->Execute();
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 110.0f));
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
		}

        TEST_METHOD(Variables_ProjectVariable_Valid)
		{
            #pragma region Local Variables Delcaration

	        string					applicationBuildName = "testProjectApplicationBuildName";
	        int						applicationBuildNumber = 0;
	        string					applicationName = "testProjectApplicationName";
	        string					applicationVersion = "1.0";
	        string					catrobatLanguageVersion = "1.0";
            time_t					dateTimeUpload;
	        string					description = "testProjectDescription";
	        string					deviceName = "testDevice";
	        string					mediaLicense = "testProjectMediaLicense";
	        string					platform = "testPlatform";
	        int						platformVersion = 1;
	        string					programLicense = "TestProjectProgramLicense";
	        string					programName = "TestProjectProgramName";
	        string					remixOf = "remixOfTestProject";
            int						screenHeight = 100;
	        int						screenWidth = 200;
	        vector<string>*			tags = new vector<string>();
	        string					url = "testProjectURL";
	        string					userHandle = "testProjectUserHandle";

            time(&dateTimeUpload);
            tags->push_back("testProjectTag1");
            tags->push_back("testProjectTag2");

            #pragma endregion

            Project *project = new Project(	applicationBuildName, 
						                    applicationBuildNumber, 
						                    applicationName, 
						                    applicationVersion, 
						                    catrobatLanguageVersion, 
						                    dateTimeUpload, 
						                    description, 
						                    deviceName,
						                    mediaLicense, 
						                    platform,
						                    platformVersion,
						                    programLicense,
						                    programName,
						                    remixOf,
						                    screenHeight,
						                    screenWidth, 
						                    tags,
						                    url,
						                    userHandle
					                      );

            ProjectDaemon::Instance()->setProject(project);
			string variableName = "testVariable";
            UserVariable *variable = new UserVariable(variableName, "10");
            FormulaTree *formulaTree = new FormulaTree("USER_VARIABLE", variableName);
			string spriteReference = "";
			Object *object = new Object("TestObject"); 

            project->getObjectList()->addObject(object);
            project->addVariable(variable->Name(), variable);

			StartScript *script = new StartScript(spriteReference, object);
            ChangeSizeByBrick *brick = new ChangeSizeByBrick(spriteReference, formulaTree, script);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            brick->Execute();
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 110.0f));
		}

        TEST_METHOD(Variables_ProjectVariable_InValid)
		{
			#pragma region Local Variables Delcaration

	        string					applicationBuildName = "testProjectApplicationBuildName";
	        int						applicationBuildNumber = 0;
	        string					applicationName = "testProjectApplicationName";
	        string					applicationVersion = "1.0";
	        string					catrobatLanguageVersion = "1.0";
            time_t					dateTimeUpload;
	        string					description = "testProjectDescription";
	        string					deviceName = "testDevice";
	        string					mediaLicense = "testProjectMediaLicense";
	        string					platform = "testPlatform";
	        int						platformVersion = 1;
	        string					programLicense = "TestProjectProgramLicense";
	        string					programName = "TestProjectProgramName";
	        string					remixOf = "remixOfTestProject";
            int						screenHeight = 100;
	        int						screenWidth = 200;
	        vector<string>*			tags = new vector<string>();
	        string					url = "testProjectURL";
	        string					userHandle = "testProjectUserHandle";

            time(&dateTimeUpload);
            tags->push_back("testProjectTag1");
            tags->push_back("testProjectTag2");

            #pragma endregion

            Project *project = new Project(	applicationBuildName, 
						                    applicationBuildNumber, 
						                    applicationName, 
						                    applicationVersion, 
						                    catrobatLanguageVersion, 
						                    dateTimeUpload, 
						                    description, 
						                    deviceName,
						                    mediaLicense, 
						                    platform,
						                    platformVersion,
						                    programLicense,
						                    programName,
						                    remixOf,
						                    screenHeight,
						                    screenWidth, 
						                    tags,
						                    url,
						                    userHandle
					                      );

            ProjectDaemon::Instance()->setProject(project);
			string variableName = "testVariable";
            string inValidVariableName = "invalidTestVariableName";
            UserVariable *variable = new UserVariable(variableName, "10");
            FormulaTree *formulaTree = new FormulaTree("USER_VARIABLE", inValidVariableName);
			string spriteReference = "";
			Object *object = new Object("TestObject"); 

            project->getObjectList()->addObject(object);
            project->addVariable(variable->Name(), variable);

			StartScript *script = new StartScript(spriteReference, object);
            ChangeSizeByBrick *brick = new ChangeSizeByBrick(spriteReference, formulaTree, script);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            brick->Execute();
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 110.0f));
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
		}
	};
}