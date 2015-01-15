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
using namespace std;

namespace PlayerWindowsPhone8Test
{
    TEST_CLASS(LookBricks)
    {
    public:
        TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckFor100Percent)
        {
			Object *object = new Object("TestObject");
            auto script = shared_ptr<Script>(new StartScript(object));
			auto formulaTree = new FormulaTree("NUMBER", "100");
            auto brick = new ChangeGhostEffectByBrick(formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 1.0f));
        }

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckForZeroPercent)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
            ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
        }

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckForOverflow)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "300");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 1.0f));
        }

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckFor50Percent)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "50");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.5f));
        }

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckForVariousChanges)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "30");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.3f));

			brick = new ChangeGhostEffectByBrick(formulaTree, script);
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.7f));

			brick = new ChangeGhostEffectByBrick(formulaTree, script);
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.6f));
        }

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckForUnderflow)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-300");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
        }

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckForZero)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-100");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);

            float actualX;
            float actualY;
            object->GetScale(actualX, actualY);
			Assert::IsTrue(TestHelper::isEqual(actualX, 100.0f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 100.0f));
            brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::IsTrue(TestHelper::isEqual(actualX, 0.0f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 0.0f));
        }

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckForUnderflow)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-5");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);

            float actualX;
            float actualY;
            object->GetScale(actualX, actualY);
            Assert::IsTrue(TestHelper::isEqual(actualX, 100.f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 100.f));
			brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::IsTrue(TestHelper::isEqual(actualX, 95.f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 95.f));
        }

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckFor8)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "8");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);

            float actualX;
            float actualY;
            object->GetScale(actualX, actualY);
            Assert::IsTrue(TestHelper::isEqual(actualX, 100.f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 100.f));
            brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::IsTrue(TestHelper::isEqual(actualX, 108.f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 108.f));
        }

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckFor03)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-0.7");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);

            float actualX;
            float actualY;
            object->GetScale(actualX, actualY);
            Assert::IsTrue(TestHelper::isEqual(actualX, 100.f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 100.f));
            brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::IsTrue(TestHelper::isEqual(actualX, 99.3f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 99.3f));
        }

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckForVariousChanges)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);
            float actualX;
            float actualY;
            object->GetScale(actualX, actualY);
            Assert::IsTrue(TestHelper::isEqual(actualX, 100.f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 100.f));
            brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::IsTrue(TestHelper::isEqual(actualX, 100.f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 100.f));

			formulaTree = new FormulaTree("NUMBER", "-5");
			brick = new ChangeSizeByBrick(formulaTree, script);
			brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::IsTrue(TestHelper::isEqual(actualX, 95.f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 95.f));

			formulaTree = new FormulaTree("NUMBER", "3");
			brick = new ChangeSizeByBrick(formulaTree, script);
			brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::IsTrue(TestHelper::isEqual(actualX, 98.f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 98.f));

			formulaTree = new FormulaTree("NUMBER", "-2.8");
			brick = new ChangeSizeByBrick(formulaTree, script);
			brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::IsTrue(TestHelper::isEqual(actualX, 95.2f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 95.2f));

			formulaTree = new FormulaTree("NUMBER", "1.9");
			brick = new ChangeSizeByBrick(formulaTree, script);
			brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::IsTrue(TestHelper::isEqual(actualX, 97.1f));
            Assert::IsTrue(TestHelper::isEqual(actualY, 97.1f));
        }

		TEST_METHOD(LookBricks_CostumeBrick)
        {
			string spriteReference = "";
			string costumeDataReference = "";

            auto look1 = shared_ptr<Look>(new Look("test1", "testName1"));
            auto look2 = shared_ptr<Look>(new Look("test2", "testName2"));
            auto look3 = shared_ptr<Look>(new Look("test3", "testName3"));
            auto look4 = shared_ptr<Look>(new Look("test4", "testName4"));
            auto look5 = shared_ptr<Look>(new Look("test5", "testName5"));

			Object *object = new Object("TestObject");
			object->AddLook(look1);
			object->AddLook(look2);
			object->AddLook(look3);
			object->AddLook(look4);
			object->AddLook(look5);

			auto script = shared_ptr<Script>(new StartScript(object));
            Assert::AreEqual(object->GetLookListSize(), 5);

			CostumeBrick *brick = new CostumeBrick(script);
			brick->Execute();
            Assert::AreEqual(object->GetIndexOfCurrentLook(), 0);

			brick = new CostumeBrick(costumeDataReference, 0, script);
			brick->Execute();
            Assert::AreEqual(object->GetIndexOfCurrentLook(), 0);

			brick = new CostumeBrick(costumeDataReference, 1, script);
			brick->Execute();
            Assert::AreEqual(object->GetIndexOfCurrentLook(), 1);

			brick = new CostumeBrick(costumeDataReference, 2, script);
			brick->Execute();
            Assert::AreEqual(object->GetIndexOfCurrentLook(), 2);

			brick = new CostumeBrick(costumeDataReference, 3, script);
			brick->Execute();
            Assert::AreEqual(object->GetIndexOfCurrentLook(), 3);


			brick = new CostumeBrick(costumeDataReference, 4, script);
			brick->Execute();
            Assert::AreEqual(object->GetIndexOfCurrentLook(), 4);
        }

		TEST_METHOD(LookBricks_HideBrick)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			HideBrick *brick = new HideBrick(script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 1.0f));
        }

		TEST_METHOD(LookBricks_NextLookBrick)
        {
			string costumeDataReference = "";

            auto look1 = shared_ptr<Look>(new Look("test1", "testName1"));
            auto look2 = shared_ptr<Look>(new Look("test2", "testName2"));
            auto look3 = shared_ptr<Look>(new Look("test3", "testName3"));
            auto look4 = shared_ptr<Look>(new Look("test4", "testName4"));
            auto look5 = shared_ptr<Look>(new Look("test5", "testName5"));

			Object *object = new Object("TestObject");
			object->AddLook(look1);
			object->AddLook(look2);
			object->AddLook(look3);
			object->AddLook(look4);
			object->AddLook(look5);

			auto script = shared_ptr<Script>(new StartScript(object));

			Assert::AreEqual(object->GetLookListSize(), 5);

			CostumeBrick *costumeBrick = new CostumeBrick(script);
			costumeBrick->Execute();
			Assert::AreEqual(object->GetIndexOfCurrentLook(), 0);

			NextLookBrick *brick = new NextLookBrick(script);
			brick->Execute();
            Assert::AreEqual(object->GetLookListSize(), 1);

			brick = new NextLookBrick(script);
			brick->Execute();
            Assert::AreEqual(object->GetLookListSize(), 2);


			brick = new NextLookBrick(script);
			brick->Execute();
            Assert::AreEqual(object->GetLookListSize(), 3);


			brick = new NextLookBrick(script);
			brick->Execute();
            Assert::AreEqual(object->GetLookListSize(), 4);


			brick = new NextLookBrick(script);
			brick->Execute();
            Assert::AreEqual(object->GetLookListSize(), 0);
        }

		TEST_METHOD(LookBricks_SetGhostEffectBrick_CheckFor100Percent)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "100");
			SetGhostEffectBrick *brick = new SetGhostEffectBrick(formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 1.0f));
        }

		TEST_METHOD(LookBricks_SetGhostEffectBrick_CheckForZeroPercent)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
			SetGhostEffectBrick *brick = new SetGhostEffectBrick(formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
        }

		TEST_METHOD(LookBricks_SetGhostEffectBrick_CheckForOverflow)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "300");
			SetGhostEffectBrick *brick = new SetGhostEffectBrick(formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 1.0f));
        }

		TEST_METHOD(LookBricks_SetGhostEffectBrick_CheckFor50Percent)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "50");
			SetGhostEffectBrick *brick = new SetGhostEffectBrick(formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.5f));
        }

		TEST_METHOD(LookBricks_SetGhostEffectBrick_CheckForVariousChanges)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "30");
			SetGhostEffectBrick *brick = new SetGhostEffectBrick(formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.3f));

			formulaTree = new FormulaTree("NUMBER", "40");
			brick = new SetGhostEffectBrick(formulaTree, script);
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.4f));

			formulaTree = new FormulaTree("NUMBER", "-10");
			brick = new SetGhostEffectBrick(formulaTree, script);
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
        }

		TEST_METHOD(LookBricks_SetGhostEffectBrick_CheckForUnderflow)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-300");
			SetGhostEffectBrick *brick = new SetGhostEffectBrick(formulaTree, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
			brick->Execute();
			Assert::IsTrue(TestHelper::isEqual(object->GetTransparency(), 0.0f));
        }

		TEST_METHOD(LookBricks_SetSizeToBrick_CheckForZero)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
			SetSizeToBrick *brick = new SetSizeToBrick(formulaTree, script);

            float actualX;
            float actualY;
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 100.f);
            Assert::AreEqual(actualY, 100.f);

			brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 0.f);
            Assert::AreEqual(actualY, 0.f);
        }

		TEST_METHOD(LookBricks_SetSizeToBrick_CheckForUnderflow)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-5");
			SetSizeToBrick *brick = new SetSizeToBrick(formulaTree, script);

            float actualX;
            float actualY;
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 100.f);
            Assert::AreEqual(actualY, 100.f);

			brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 0.f);
            Assert::AreEqual(actualY, 0.f);
        }

		TEST_METHOD(LookBricks_SetSizeToBrick_CheckFor8)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "8");
			SetSizeToBrick *brick = new SetSizeToBrick(formulaTree, script);

            float actualX;
            float actualY;
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 100.f);
            Assert::AreEqual(actualY, 100.f);

            brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 8.f);
            Assert::AreEqual(actualY, 8.f);
        }

		TEST_METHOD(LookBricks_SetSizeToBrick_CheckFor03)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0.3");
			SetSizeToBrick *brick = new SetSizeToBrick(formulaTree, script);

            float actualX;
            float actualY;
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 100.f);
            Assert::AreEqual(actualY, 100.f);

            brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 0.3f);
            Assert::AreEqual(actualY, 0.3f);
        }

		TEST_METHOD(LookBricks_SetSizeToBrick_CheckForVariousChanges)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "100");
			SetSizeToBrick *brick = new SetSizeToBrick(formulaTree, script);

            float actualX;
            float actualY;
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 100.f);
            Assert::AreEqual(actualY, 100.f);

            brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 100.f);
            Assert::AreEqual(actualY, 100.f);

			formulaTree = new FormulaTree("NUMBER", "-50");
			brick = new SetSizeToBrick(formulaTree, script);

            brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 0.f);
            Assert::AreEqual(actualY, 0.f);

			formulaTree = new FormulaTree("NUMBER", "30");
			brick = new SetSizeToBrick(formulaTree, script);

            brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 30.f);
            Assert::AreEqual(actualY, 30.f);

			formulaTree = new FormulaTree("NUMBER", "20.8");
			brick = new SetSizeToBrick(formulaTree, script);

            brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 20.8f);
            Assert::AreEqual(actualY, 20.8f);

			formulaTree = new FormulaTree("NUMBER", "130.9");
			brick = new SetSizeToBrick(formulaTree, script);

            brick->Execute();
            object->GetScale(actualX, actualY);
            Assert::AreEqual(actualX, 130.9f);
            Assert::AreEqual(actualY, 130.9f);
        }

		TEST_METHOD(LookBricks_ShowBrick)
        {
			Object *object = new Object("TestObject");
			auto script = shared_ptr<Script>(new StartScript(object));
			HideBrick *hideBrick = new HideBrick(script);

			Assert::AreEqual(object->GetTransparency(), 0.0f);
			hideBrick->Execute();
			Assert::AreEqual(object->GetTransparency(), 1.0f);

			ShowBrick *brick = new ShowBrick(script);
			brick->Execute();
            Assert::AreEqual(object->GetTransparency(), 0.0f);
        }
    };
}