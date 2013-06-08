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
        TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckFor100Percent)
        {
            // TODO: Your test code here
			string spriteReference = "";
			Object *object = new Object("TestObject");
			StartScript *script = new StartScript(spriteReference, object);
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "100");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(spriteReference, formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 1.0f));
        }

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckForOverflow)
        {
            // TODO: Your test code here
			string spriteReference = "";
			Object *object = new Object("TestObject");
			StartScript *script = new StartScript(spriteReference, object);
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "300");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(spriteReference, formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 1.0f));
        }

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckForUnderflow)
        {
            // TODO: Your test code here
			string spriteReference = "";
			Object *object = new Object("TestObject");
			StartScript *script = new StartScript(spriteReference, object);
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-300");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(spriteReference, formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
        }

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest)
        {
            // TODO: Your test code here
			Assert::IsTrue(false);
        }

		TEST_METHOD(LookBricks_CostumeBrick)
        {
            // TODO: Your test code here
			Assert::IsTrue(false);
        }

		TEST_METHOD(LookBricks_HideBrick)
        {
            // TODO: Your test code here
			Assert::IsTrue(false);
        }

		TEST_METHOD(LookBricks_NextLookBrick)
        {
            // TODO: Your test code here
			Assert::IsTrue(false);
        }

		TEST_METHOD(LookBricks_SetGhostEffectBrick)
        {
            // TODO: Your test code here
			Assert::IsTrue(false);
        }

		TEST_METHOD(LookBricks_SetSizeToBrick)
        {
            // TODO: Your test code here
			Assert::IsTrue(false);
        }

		TEST_METHOD(LookBricks_ShowBrick)
        {
            // TODO: Your test code here
			Assert::IsTrue(false);
        }
    };
}