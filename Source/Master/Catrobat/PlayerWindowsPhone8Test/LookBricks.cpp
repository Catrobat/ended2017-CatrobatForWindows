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

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckForZeroPercent)
        {
            // TODO: Your test code here
			string spriteReference = "";
			Object *object = new Object("TestObject");
			StartScript *script = new StartScript(spriteReference, object);
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(spriteReference, formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
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

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckFor50Percent)
        {
            // TODO: Your test code here
			string spriteReference = "";
			Object *object = new Object("TestObject");
			StartScript *script = new StartScript(spriteReference, object);
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "50");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(spriteReference, formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.5f));
        }

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckForVariousChanges)
        {
            // TODO: Your test code here
			string spriteReference = "";
			Object *object = new Object("TestObject");
			StartScript *script = new StartScript(spriteReference, object);
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "30");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(spriteReference, formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.3f));

			formulaTree = new FormulaTree("NUMBER", "40");
			brick = new ChangeGhostEffectByBrick(spriteReference, formulaTree, script);
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.7f));

			formulaTree = new FormulaTree("NUMBER", "-10");
			brick = new ChangeGhostEffectByBrick(spriteReference, formulaTree, script);
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.6f));
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

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckForZero)
        {
            // TODO: Your test code here
			string spriteReference = "";
			Object *object = new Object("TestObject");
			StartScript *script = new StartScript(spriteReference, object);
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-1");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(spriteReference, formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 1.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 0.0f));
        }

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckForUnderflow)
        {
            // TODO: Your test code here
			string spriteReference = "";
			Object *object = new Object("TestObject");
			StartScript *script = new StartScript(spriteReference, object);
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-5");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(spriteReference, formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 1.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 0.0f));
        }

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckFor8)
        {
            // TODO: Your test code here
			string spriteReference = "";
			Object *object = new Object("TestObject");
			StartScript *script = new StartScript(spriteReference, object);
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "7");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(spriteReference, formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 1.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 8.0f));
        }

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckFor03)
        {
            // TODO: Your test code here
			string spriteReference = "";
			Object *object = new Object("TestObject");
			StartScript *script = new StartScript(spriteReference, object);
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-0.7");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(spriteReference, formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 1.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 0.3f));
        }

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckForVariousChanges)
        {
            // TODO: Your test code here
			string spriteReference = "";
			Object *object = new Object("TestObject");
			StartScript *script = new StartScript(spriteReference, object);
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(spriteReference, formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 1.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 1.0f));

			formulaTree = new FormulaTree("NUMBER", "-5");
			brick = new ChangeSizeByBrick(spriteReference, formulaTree, script);
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 0.0f));

			formulaTree = new FormulaTree("NUMBER", "3");
			brick = new ChangeSizeByBrick(spriteReference, formulaTree, script);
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 3.0f));

			formulaTree = new FormulaTree("NUMBER", "-2.8");
			brick = new ChangeSizeByBrick(spriteReference, formulaTree, script);
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 0.2f));

			formulaTree = new FormulaTree("NUMBER", "1.9");
			brick = new ChangeSizeByBrick(spriteReference, formulaTree, script);
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 2.1f));
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