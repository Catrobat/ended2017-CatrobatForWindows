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
			string spriteReference = "";
			string costumeDataReference = "";

			Look *look1 = new Look("test1", "testName1");
			Look *look2 = new Look("test2", "testName2");
			Look *look3 = new Look("test3", "testName3");
			Look *look4 = new Look("test4", "testName4");
			Look *look5 = new Look("test5", "testName5");

			Object *object = new Object("TestObject");
			object->addLook(look1);
			object->addLook(look2);
			object->addLook(look3);
			object->addLook(look4);
			object->addLook(look5);

			StartScript *script = new StartScript(spriteReference, object);

			Assert::AreEqual(object->GetLookCount(), 5);

			CostumeBrick *brick = new CostumeBrick(spriteReference, script);
			brick->Execute();
			Assert::AreEqual(object->GetLook(), 0);
			Assert::IsTrue(object->GetCurrentLook() == look1);
			Assert::IsFalse(object->GetCurrentLook() == look2);
			Assert::IsFalse(object->GetCurrentLook() == look3);
			Assert::IsFalse(object->GetCurrentLook() == look4);
			Assert::IsFalse(object->GetCurrentLook() == look5);

			brick = new CostumeBrick(spriteReference, costumeDataReference, 0, script);
			brick->Execute();
			Assert::AreEqual(object->GetLook(), 0);
			Assert::IsTrue(object->GetCurrentLook() == look1);
			Assert::IsFalse(object->GetCurrentLook() == look2);
			Assert::IsFalse(object->GetCurrentLook() == look3);
			Assert::IsFalse(object->GetCurrentLook() == look4);
			Assert::IsFalse(object->GetCurrentLook() == look5);

			brick = new CostumeBrick(spriteReference, costumeDataReference, 1, script);
			brick->Execute();
			Assert::AreEqual(object->GetLook(), 1);
			Assert::IsFalse(object->GetCurrentLook() == look1);
			Assert::IsTrue(object->GetCurrentLook() == look2);
			Assert::IsFalse(object->GetCurrentLook() == look3);
			Assert::IsFalse(object->GetCurrentLook() == look4);
			Assert::IsFalse(object->GetCurrentLook() == look5);

			brick = new CostumeBrick(spriteReference, costumeDataReference, 2, script);
			brick->Execute();
			Assert::AreEqual(object->GetLook(), 2);
			Assert::IsFalse(object->GetCurrentLook() == look1);
			Assert::IsFalse(object->GetCurrentLook() == look2);
			Assert::IsTrue(object->GetCurrentLook() == look3);
			Assert::IsFalse(object->GetCurrentLook() == look4);
			Assert::IsFalse(object->GetCurrentLook() == look5);

			brick = new CostumeBrick(spriteReference, costumeDataReference, 3, script);
			brick->Execute();
			Assert::AreEqual(object->GetLook(), 3);
			Assert::IsFalse(object->GetCurrentLook() == look1);
			Assert::IsFalse(object->GetCurrentLook() == look2);
			Assert::IsFalse(object->GetCurrentLook() == look3);
			Assert::IsTrue(object->GetCurrentLook() == look4);
			Assert::IsFalse(object->GetCurrentLook() == look5);

			brick = new CostumeBrick(spriteReference, costumeDataReference, 4, script);
			brick->Execute();
			Assert::AreEqual(object->GetLook(), 4);
			Assert::IsFalse(object->GetCurrentLook() == look1);
			Assert::IsFalse(object->GetCurrentLook() == look2);
			Assert::IsFalse(object->GetCurrentLook() == look3);
			Assert::IsFalse(object->GetCurrentLook() == look4);
			Assert::IsTrue(object->GetCurrentLook() == look5);
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