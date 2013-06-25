#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

#include "Object.h"
#include "StartScript.h"
#include "UserVariable.h"

#include "ChangeVariableBrick.h"
#include "SetVariableBrick.h"
#include "VariableManagementBrick.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
	TEST_CLASS(VariableBricks)
	{
	public:
		
		TEST_METHOD(VariableBricks_ChangeVariableBrick)
		{
            string variableName = "testVariable";
            UserVariable *variable = new UserVariable(variableName, "10");
            FormulaTree *formulaTree = new FormulaTree("USER_VARIABLE", variableName);
			string spriteReference = "";
			Object *object = new Object("TestObject"); 

			StartScript *script = new StartScript(spriteReference, object);
            SetVariableBrick *brick = new SetVariableBrick(spriteReference, formulaTree, script);
            Assert::IsTrue(false);
		}

        TEST_METHOD(VariableBricks_SetVariableBrick)
		{
			// TODO: Your test code here
            Assert::IsTrue(false);
		}
	};
}