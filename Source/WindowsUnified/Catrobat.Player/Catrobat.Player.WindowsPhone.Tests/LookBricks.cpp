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
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			auto formulaTree = new FormulaTree("NUMBER", "100");
			auto brick = new ChangeGhostEffectByBrick(formulaTree, script);

			Assert::AreEqual(object->GetTransparency(), 0.0f);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 1.0f);
		}

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckForZeroPercent)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(formulaTree, script);

			Assert::AreEqual(object->GetTransparency(), 0.0f);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 0.0f);
		}

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckForOverflow)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "300");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(formulaTree, script);

			Assert::AreEqual(object->GetTransparency(), 0.0f);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 1.0f);
		}

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckFor50Percent)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "50");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(formulaTree, script);

			Assert::AreEqual(object->GetTransparency(), 0.0f);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 0.5f);
		}

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckForVariousChanges)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "30");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(formulaTree, script);

			Assert::AreEqual(object->GetTransparency(), 0.0f, EPSILON);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 0.3f, EPSILON);

			brick = new ChangeGhostEffectByBrick(formulaTree, script);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 0.6f, EPSILON);

			brick = new ChangeGhostEffectByBrick(formulaTree, script);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 0.9f, EPSILON);
		}

		TEST_METHOD(LookBricks_ChangeGhostEffectBrick_CheckForUnderflow)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-300");
			ChangeGhostEffectByBrick *brick = new ChangeGhostEffectByBrick(formulaTree, script);

			Assert::AreEqual(object->GetTransparency(), 0.0f);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 0.0f);
		}

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckForZero)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-1");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);

			float actualX;
			float actualY;
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 1.f, EPSILON);
			Assert::AreEqual(actualY, 1.f, EPSILON);
			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 0.f, EPSILON);
			Assert::AreEqual(actualY, 0.f, EPSILON);
		}

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckForUnderflow)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-5");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);

			float actualX;
			float actualY;
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 1.f, EPSILON);
			Assert::AreEqual(actualY, 1.f, EPSILON);
			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 0.f, EPSILON);
			Assert::AreEqual(actualY, 0.f, EPSILON);
		}

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckFor8)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "8");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);

			float actualX;
			float actualY;
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 1.f, EPSILON);
			Assert::AreEqual(actualY, 1.f, EPSILON);
			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 9.f, EPSILON);
			Assert::AreEqual(actualY, 9.f, EPSILON);
		}

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckFor03)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-0.7");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);

			float actualX;
			float actualY;
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 1.f, EPSILON);
			Assert::AreEqual(actualY, 1.f, EPSILON);
			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 0.3f, EPSILON);
			Assert::AreEqual(actualY, 0.3f, EPSILON);
		}

		TEST_METHOD(LookBricks_ChangeSizeByBrickTest_CheckForVariousChanges)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
			ChangeSizeByBrick *brick = new ChangeSizeByBrick(formulaTree, script);
			float actualX;
			float actualY;
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 1.f, EPSILON);
			Assert::AreEqual(actualY, 1.f, EPSILON);
			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 1.f, EPSILON);
			Assert::AreEqual(actualY, 1.f, EPSILON);

			formulaTree = new FormulaTree("NUMBER", "-5");
			brick = new ChangeSizeByBrick(formulaTree, script);
			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 0.f, EPSILON);
			Assert::AreEqual(actualY, 0.f, EPSILON);

			formulaTree = new FormulaTree("NUMBER", "3");
			brick = new ChangeSizeByBrick(formulaTree, script);
			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 3.f, EPSILON);
			Assert::AreEqual(actualY, 3.f, EPSILON);

			formulaTree = new FormulaTree("NUMBER", "-2.8");
			brick = new ChangeSizeByBrick(formulaTree, script);
			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 0.2f, EPSILON);
			Assert::AreEqual(actualY, 0.2f, EPSILON);

			formulaTree = new FormulaTree("NUMBER", "1.9");
			brick = new ChangeSizeByBrick(formulaTree, script);
			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 2.1f, EPSILON);
			Assert::AreEqual(actualY, 2.1f, EPSILON);
		}

		TEST_METHOD(LookBricks_CostumeBrick)
		{
			string costumeDataReference = "";

			auto look1(shared_ptr<Look>(new Look("test1", "testName1")));
			auto look2(shared_ptr<Look>(new Look("test2", "testName2")));
			auto look3(shared_ptr<Look>(new Look("test3", "testName3")));
			auto look4(shared_ptr<Look>(new Look("test4", "testName4")));
			auto look5(shared_ptr<Look>(new Look("test5", "testName5")));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			object->AddLook(look1);
			object->AddLook(look2);
			object->AddLook(look3);
			object->AddLook(look4);
			object->AddLook(look5);

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
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			HideBrick *brick = new HideBrick(script);

			Assert::AreEqual(object->GetTransparency(), 0.0f);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 1.0f);
		}

		TEST_METHOD(LookBricks_NextLookBrick)
		{
			string costumeDataReference = "";

			auto look1 = shared_ptr<Look>(new Look("test1", "testName1"));
			auto look2 = shared_ptr<Look>(new Look("test2", "testName2"));
			auto look3 = shared_ptr<Look>(new Look("test3", "testName3"));
			auto look4 = shared_ptr<Look>(new Look("test4", "testName4"));
			auto look5 = shared_ptr<Look>(new Look("test5", "testName5"));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			object->AddLook(look1);
			object->AddLook(look2);
			object->AddLook(look3);
			object->AddLook(look4);
			object->AddLook(look5);

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
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "100");
			SetGhostEffectBrick *brick = new SetGhostEffectBrick(formulaTree, script);

			Assert::AreEqual(object->GetTransparency(), 0.0f);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 1.0f);
		}

		TEST_METHOD(LookBricks_SetGhostEffectBrick_CheckForZeroPercent)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
			SetGhostEffectBrick *brick = new SetGhostEffectBrick(formulaTree, script);

			Assert::AreEqual(object->GetTransparency(), 0.0f);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 0.0f);
		}

		TEST_METHOD(LookBricks_SetGhostEffectBrick_CheckForOverflow)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "300");
			SetGhostEffectBrick *brick = new SetGhostEffectBrick(formulaTree, script);

			Assert::AreEqual(object->GetTransparency(), 0.0f);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 1.0f);
		}

		TEST_METHOD(LookBricks_SetGhostEffectBrick_CheckFor50Percent)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "50");
			SetGhostEffectBrick *brick = new SetGhostEffectBrick(formulaTree, script);

			Assert::AreEqual(object->GetTransparency(), 0.0f);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 0.5f);
		}

		TEST_METHOD(LookBricks_SetGhostEffectBrick_CheckForVariousChanges)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "30");
			SetGhostEffectBrick *brick = new SetGhostEffectBrick(formulaTree, script);

			Assert::AreEqual(object->GetTransparency(), 0.0f, EPSILON);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 0.3f, EPSILON);

			formulaTree = new FormulaTree("NUMBER", "40");
			brick = new SetGhostEffectBrick(formulaTree, script);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 0.4f, EPSILON);

			formulaTree = new FormulaTree("NUMBER", "-10");
			brick = new SetGhostEffectBrick(formulaTree, script);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 0.0f, EPSILON);
		}

		TEST_METHOD(LookBricks_SetGhostEffectBrick_CheckForUnderflow)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-300");
			SetGhostEffectBrick *brick = new SetGhostEffectBrick(formulaTree, script);

			Assert::AreEqual(object->GetTransparency(), 0.0f);
			brick->Execute();
			Assert::AreEqual(object->GetTransparency(), 0.0f);
		}

		TEST_METHOD(LookBricks_SetSizeToBrick_CheckForZero)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
			SetSizeToBrick *brick = new SetSizeToBrick(formulaTree, script);

			float actualX;
			float actualY;
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 1.f, EPSILON);
			Assert::AreEqual(actualY, 1.f, EPSILON);

			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 0.f, EPSILON);
			Assert::AreEqual(actualY, 0.f, EPSILON);
		}

		TEST_METHOD(LookBricks_SetSizeToBrick_CheckForUnderflow)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-5");
			SetSizeToBrick *brick = new SetSizeToBrick(formulaTree, script);

			float actualX;
			float actualY;
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 1.f, EPSILON);
			Assert::AreEqual(actualY, 1.f, EPSILON);

			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 0.f, EPSILON);
			Assert::AreEqual(actualY, 0.f, EPSILON);
		}

		TEST_METHOD(LookBricks_SetSizeToBrick_CheckFor8)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "8");
			SetSizeToBrick *brick = new SetSizeToBrick(formulaTree, script);

			float actualX;
			float actualY;
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 1.f, EPSILON);
			Assert::AreEqual(actualY, 1.f, EPSILON);

			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 8.f, EPSILON);
			Assert::AreEqual(actualY, 8.f, EPSILON);
		}

		TEST_METHOD(LookBricks_SetSizeToBrick_CheckFor03)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0.3");
			SetSizeToBrick *brick = new SetSizeToBrick(formulaTree, script);

			float actualX;
			float actualY;
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 1.f, EPSILON);
			Assert::AreEqual(actualY, 1.f, EPSILON);

			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 0.3f, EPSILON);
			Assert::AreEqual(actualY, 0.3f, EPSILON);
		}

		TEST_METHOD(LookBricks_SetSizeToBrick_CheckForVariousChanges)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "100");
			SetSizeToBrick *brick = new SetSizeToBrick(formulaTree, script);

			float actualX;
			float actualY;
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 1.f, EPSILON);
			Assert::AreEqual(actualY, 1.f, EPSILON);

			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 100.f, EPSILON);
			Assert::AreEqual(actualY, 100.f, EPSILON);

			formulaTree = new FormulaTree("NUMBER", "-50");
			brick = new SetSizeToBrick(formulaTree, script);

			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 0.f, EPSILON);
			Assert::AreEqual(actualY, 0.f, EPSILON);

			formulaTree = new FormulaTree("NUMBER", "30");
			brick = new SetSizeToBrick(formulaTree, script);

			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 30.f, EPSILON);
			Assert::AreEqual(actualY, 30.f, EPSILON);

			formulaTree = new FormulaTree("NUMBER", "20.8");
			brick = new SetSizeToBrick(formulaTree, script);

			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 20.8f, EPSILON);
			Assert::AreEqual(actualY, 20.8f, EPSILON);

			formulaTree = new FormulaTree("NUMBER", "130.9");
			brick = new SetSizeToBrick(formulaTree, script);

			brick->Execute();
			object->GetScale(actualX, actualY);
			Assert::AreEqual(actualX, 130.9f, EPSILON);
			Assert::AreEqual(actualY, 130.9f, EPSILON);
		}

		TEST_METHOD(LookBricks_ShowBrick)
		{
			auto object(shared_ptr<Object>(new Object("TestObject")));
			auto script(shared_ptr<Script>(new StartScript(object)));
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