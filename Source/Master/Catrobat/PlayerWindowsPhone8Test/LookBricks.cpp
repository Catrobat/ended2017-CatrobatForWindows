#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

#include "Object.h"
#include "FormulaTree.h"
#include "StartScript.h"

#include "ChangeGhostEffectByBrick.h"
#include "ChangeSizeByBrick.h"
#include "CostumeBrick.h"
#include "HideBrick.h"
#include "NextLookBrick.h"
#include "SetGhostEffectBrick.h"
#include "SetSizeToBrick.h"
#include "ShowBrick.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
    TEST_CLASS(LookBricks)
    {
    public:
        TEST_METHOD(LookBricks_ChangeGhostEffectBrick)
        {
            // TODO: Your test code here
			string spriteReference = "";
			Object *object = new Object("TestObject");
			StartScript *script = new StartScript(spriteReference, object);
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "10");

			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(spriteReference, formulaTree, script);

			float test = object->GetTransparency();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			//Assert::IsTrue(object->GetTransparency() == 1.0f);
        }

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest)
        {
            // TODO: Your test code here
        }

		TEST_METHOD(LookBricks_CostumeBrick)
        {
            // TODO: Your test code here
        }

		TEST_METHOD(LookBricks_HideBrick)
        {
            // TODO: Your test code here
        }

		TEST_METHOD(LookBricks_NextLookBrick)
        {
            // TODO: Your test code here
        }

		TEST_METHOD(LookBricks_SetGhostEffectBrick)
        {
            // TODO: Your test code here
        }

		TEST_METHOD(LookBricks_SetSizeToBrick)
        {
            // TODO: Your test code here
        }

		TEST_METHOD(LookBricks_ShowBrick)
        {
            // TODO: Your test code here
        }
    };
}