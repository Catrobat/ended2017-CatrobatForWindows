#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

#include "Object.h"
#include "FormulaTree.h"
#include "StartScript.h"
#include "BroadcastScript.h"
#include "BroadcastMessageDaemon.h"

#include "BroadcastBrick.h"
#include "ForeverBrick.h"
#include "IfBrick.h"
#include "RepeatBrick.h"
#include "WaitBrick.h"

#include "SetSizeToBrick.h"
#include "ChangeSizeByBrick.h"

#include <ppltasks.h>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
	TEST_CLASS(ControlBricks)
	{
	public:
		
		TEST_METHOD(ControlBricks_BroadcastBrick_InSameObjectWithValidMessage)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "9");
			string spriteReference = "";
			string broadcastMessage = "TestMessage";
			Object *object = new Object("TestObject");

			StartScript *script = new StartScript(spriteReference, object);
			BroadcastScript *broadcastScript = new BroadcastScript(broadcastMessage, spriteReference, object);
			broadcastScript->addBrick(new SetSizeToBrick(spriteReference, formulaTree, broadcastScript));

			BroadcastBrick *brick = new BroadcastBrick(spriteReference, broadcastMessage, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
			brick->Execute();
			Concurrency::wait(100);
			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 9.0f));
		}

		TEST_METHOD(ControlBricks_BroadcastBrick_InSameObjectWithWrongMessage)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "9");
			string spriteReference = "";
			string broadcastMessage = "WrongTestMessage";
			string expectedMessage = "TestMessage";
			Object *object = new Object("TestObject");

			StartScript *script = new StartScript(spriteReference, object);
			BroadcastScript *broadcastScript = new BroadcastScript(expectedMessage, spriteReference, object);
			broadcastScript->addBrick(new SetSizeToBrick(spriteReference, formulaTree, broadcastScript));

			BroadcastBrick *brick = new BroadcastBrick(spriteReference, broadcastMessage, script);

			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
			brick->Execute();
			Concurrency::wait(100);
			Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
			Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 9.0f));
		}

		TEST_METHOD(ControlBricks_BroadcastBrick_InDifferentObjectWithValidMessage)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "9");
			string spriteReference = "";
			string broadcastMessage = "TestMessage";
			Object *object1 = new Object("TestObject1");
			Object *object2 = new Object("TestObject2");

			StartScript *script = new StartScript(spriteReference, object1);
			BroadcastScript *broadcastScript = new BroadcastScript(broadcastMessage, spriteReference, object2);
			broadcastScript->addBrick(new SetSizeToBrick(spriteReference, formulaTree, broadcastScript));

			BroadcastBrick *brick = new BroadcastBrick(spriteReference, broadcastMessage, script);

			Assert::IsTrue(TestHelper::isEqual(object2->GetScale(), 100.0f));
			brick->Execute();
			Concurrency::wait(100);
			Assert::IsTrue(TestHelper::isEqual(object2->GetScale(), 9.0f));
		}

		TEST_METHOD(ControlBricks_BroadcastBrick_InDifferentObjectWithWrongMessage)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "9");
			string spriteReference = "";
			string broadcastMessage = "WrongTestMessage";
			string expectedMessage = "TestMessage";
			Object *object1 = new Object("TestObject1");
			Object *object2 = new Object("TestObject2");

			StartScript *script = new StartScript(spriteReference, object1);
			BroadcastScript *broadcastScript = new BroadcastScript(expectedMessage, spriteReference, object2);
			broadcastScript->addBrick(new SetSizeToBrick(spriteReference, formulaTree, broadcastScript));

			BroadcastBrick *brick = new BroadcastBrick(spriteReference, broadcastMessage, script);

			Assert::IsTrue(TestHelper::isEqual(object2->GetScale(), 100.0f));
			brick->Execute();
			Concurrency::wait(100);
			Assert::IsTrue(TestHelper::isEqual(object2->GetScale(), 100.0f));
			Assert::IsFalse(TestHelper::isEqual(object2->GetScale(), 9.0f));
		}

		TEST_METHOD(ControlBricks_BroadcastBrick_VariousTests)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "9");
			string spriteReference = "";
			string broadcastMessageFalse = "WrongTestMessage";
			string broadcastMessageValid = "TestMessage2";
			string expectedMessage2 = "TestMessage2";
			string expectedMessage = "TestMessage";
			Object *object1 = new Object("TestObject1");
			Object *object2 = new Object("TestObject2");

			StartScript *script = new StartScript(spriteReference, object1);
			BroadcastScript *broadcastScript = new BroadcastScript(expectedMessage, spriteReference, object2);
			broadcastScript->addBrick(new SetSizeToBrick(spriteReference, formulaTree, broadcastScript));

			BroadcastBrick *brick = new BroadcastBrick(spriteReference, broadcastMessageFalse, script);

			Assert::IsTrue(TestHelper::isEqual(object2->GetScale(), 100.0f));
			brick->Execute();
			Concurrency::wait(100);
			Assert::IsTrue(TestHelper::isEqual(object2->GetScale(), 100.0f));
			Assert::IsFalse(TestHelper::isEqual(object2->GetScale(), 9.0f));

			brick = new BroadcastBrick(spriteReference, broadcastMessageValid, script);

			Assert::IsTrue(TestHelper::isEqual(object2->GetScale(), 100.0f));
			brick->Execute();
			Concurrency::wait(100);
			Assert::IsTrue(TestHelper::isEqual(object2->GetScale(), 100.0f));
			Assert::IsFalse(TestHelper::isEqual(object2->GetScale(), 9.0f));

			formulaTree = new FormulaTree("NUMBER", "8");
			BroadcastScript *broadcastScript2 = new BroadcastScript(expectedMessage2, spriteReference, object1);
			broadcastScript2->addBrick(new SetSizeToBrick(spriteReference, formulaTree, broadcastScript));

			brick = new BroadcastBrick(spriteReference, broadcastMessageValid, script);

			Assert::IsTrue(TestHelper::isEqual(object1->GetScale(), 100.0f));
			brick->Execute();
			Concurrency::wait(100);
			Assert::IsTrue(TestHelper::isEqual(object1->GetScale(), 100.0f));
			Assert::IsFalse(TestHelper::isEqual(object1->GetScale(), 8.0f));
		}

		TEST_METHOD(ControlBricks_ForeverBrick)
		{
            FormulaTree *formulaTree = new FormulaTree("NUMBER", "50");
			string spriteReference = "";
			Object *object = new Object("TestObject");
            StartScript *script = new StartScript(spriteReference, object);

            ForeverBrick *brick = new ForeverBrick(spriteReference, script);
            script->addBrick(brick);
            SetSizeToBrick *setSizeBrick = new SetSizeToBrick(spriteReference, formulaTree, script);
            brick->AddBrick(setSizeBrick);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
			Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 50.0f));
            script->Execute();
            Concurrency::wait(100);
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 50.0f));

            formulaTree = new FormulaTree("NUMBER", "30");
            SetSizeToBrick *setSizeBrick2 = new SetSizeToBrick(spriteReference, formulaTree, script);
            setSizeBrick2->Execute();
            Concurrency::wait(100);
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 50.0f));
			Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 30.0f));

            formulaTree = new FormulaTree("NUMBER", "80");
            SetSizeToBrick *setSizeBrick3 = new SetSizeToBrick(spriteReference, formulaTree, script);
            setSizeBrick3->Execute();
            Concurrency::wait(100);
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 50.0f));
			Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 80.0f));
		}

		TEST_METHOD(ControlBricks_IfBrick_SimpleTrueCheck)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "1");
			string spriteReference = "";
			Object *object = new Object("TestObject");

			StartScript *script = new StartScript(spriteReference, object);
            IfBrick *brick = new IfBrick(spriteReference, formulaTree, script);

            formulaTree = new FormulaTree("NUMBER", "50");
            SetSizeToBrick *setSizeBrick = new SetSizeToBrick(spriteReference, formulaTree, script);
            brick->AddBrick(setSizeBrick);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
			Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 50.0f));
            brick->Execute();
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 50.0f));
		}

        TEST_METHOD(ControlBricks_IfBrick_SimpleFalseCheck)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
			string spriteReference = "";
			Object *object = new Object("TestObject");

			StartScript *script = new StartScript(spriteReference, object);
            IfBrick *brick = new IfBrick(spriteReference, formulaTree, script);

            formulaTree = new FormulaTree("NUMBER", "50");
            SetSizeToBrick *setSizeBrick = new SetSizeToBrick(spriteReference, formulaTree, script);
            brick->AddBrick(setSizeBrick);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
			Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 50.0f));
            brick->Execute();
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 50.0f));
		}

        TEST_METHOD(ControlBricks_IfBrick_ElseCheckTrue)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
			string spriteReference = "";
			Object *object = new Object("TestObject");

			StartScript *script = new StartScript(spriteReference, object);
            IfBrick *brick = new IfBrick(spriteReference, formulaTree, script);

            formulaTree = new FormulaTree("NUMBER", "80");
            SetSizeToBrick *setSizeBrick1 = new SetSizeToBrick(spriteReference, formulaTree, script);
            brick->AddBrick(setSizeBrick1);

            formulaTree = new FormulaTree("NUMBER", "40");
            SetSizeToBrick *setSizeBrick2 = new SetSizeToBrick(spriteReference, formulaTree, script);
            brick->AddBrick(setSizeBrick2);
            brick->AddElseBrick(setSizeBrick2);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
			Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 80.0f));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 40.0f));
            brick->Execute();
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 40));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 80.0f));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 100.0f));
		}

        TEST_METHOD(ControlBricks_IfBrick_ElseCheckFalse)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "1");
			string spriteReference = "";
			Object *object = new Object("TestObject");

			StartScript *script = new StartScript(spriteReference, object);
            IfBrick *brick = new IfBrick(spriteReference, formulaTree, script);

            formulaTree = new FormulaTree("NUMBER", "80");
            SetSizeToBrick *setSizeBrick1 = new SetSizeToBrick(spriteReference, formulaTree, script);
            brick->AddBrick(setSizeBrick1);

            formulaTree = new FormulaTree("NUMBER", "40");
            SetSizeToBrick *setSizeBrick2 = new SetSizeToBrick(spriteReference, formulaTree, script);
            brick->AddElseBrick(setSizeBrick2);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
			Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 80.0f));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 40.0f));
            brick->Execute();
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 40));
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 80.0f));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 100.0f));
		}

        TEST_METHOD(ControlBricks_IfBrick_RecursiveElseIfCheck)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
			string spriteReference = "";
			Object *object = new Object("TestObject");

			StartScript *script = new StartScript(spriteReference, object);
            IfBrick *brick = new IfBrick(spriteReference, formulaTree, script);

            formulaTree = new FormulaTree("NUMBER", "80");
            SetSizeToBrick *setSizeBrick1 = new SetSizeToBrick(spriteReference, formulaTree, script);
            brick->AddBrick(setSizeBrick1);

            formulaTree = new FormulaTree("NUMBER", "1");
            IfBrick *elseifBrick = new IfBrick(spriteReference, formulaTree, script);
            brick->AddElseBrick(elseifBrick);

            formulaTree = new FormulaTree("NUMBER", "40");
            SetSizeToBrick *setSizeBrick2 = new SetSizeToBrick(spriteReference, formulaTree, script);
            elseifBrick->AddBrick(setSizeBrick2);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
			Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 80.0f));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 40.0f));
            brick->Execute();
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 40));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 80.0f));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 100.0f));
		}

		TEST_METHOD(ControlBricks_RepeatBrick_NormalCheck)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "5");
			string spriteReference = "";
			Object *object = new Object("TestObject");
            StartScript *script = new StartScript(spriteReference, object);

            RepeatBrick *brick = new RepeatBrick(spriteReference, formulaTree, script);
            formulaTree = new FormulaTree("NUMBER", "10");
            ChangeSizeByBrick *changeSizeBrick = new ChangeSizeByBrick(spriteReference, formulaTree, script);
            brick->AddBrick(changeSizeBrick);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            brick->Execute();
            Concurrency::wait(100);
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 100.0f));
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 150.0f));
            Concurrency::wait(100);
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 100.0f));
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 150.0f));
		}

        TEST_METHOD(ControlBricks_RepeatBrick_UnderflowCheck)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-5");
			string spriteReference = "";
			Object *object = new Object("TestObject");
            StartScript *script = new StartScript(spriteReference, object);

            RepeatBrick *brick = new RepeatBrick(spriteReference, formulaTree, script);
            formulaTree = new FormulaTree("NUMBER", "10");
            ChangeSizeByBrick *changeSizeBrick = new ChangeSizeByBrick(spriteReference, formulaTree, script);
            brick->AddBrick(changeSizeBrick);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            brick->Execute();
            Concurrency::wait(100);
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 150.0f));
            Concurrency::wait(100);
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 150.0f));
		}

        TEST_METHOD(ControlBricks_RepeatBrick_0Check)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "0");
			string spriteReference = "";
			Object *object = new Object("TestObject");
            StartScript *script = new StartScript(spriteReference, object);

            RepeatBrick *brick = new RepeatBrick(spriteReference, formulaTree, script);
            formulaTree = new FormulaTree("NUMBER", "10");
            ChangeSizeByBrick *changeSizeBrick = new ChangeSizeByBrick(spriteReference, formulaTree, script);
            brick->AddBrick(changeSizeBrick);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            brick->Execute();
            Concurrency::wait(100);
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 150.0f));
            Concurrency::wait(100);
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 150.0f));
		}

        TEST_METHOD(ControlBricks_RepeatBrick_VariousChecks)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "1");
			string spriteReference = "";
			Object *object = new Object("TestObject");
            StartScript *script = new StartScript(spriteReference, object);

            RepeatBrick *brick = new RepeatBrick(spriteReference, formulaTree, script);
            formulaTree = new FormulaTree("NUMBER", "10");
            ChangeSizeByBrick *changeSizeBrick = new ChangeSizeByBrick(spriteReference, formulaTree, script);
            brick->AddBrick(changeSizeBrick);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            brick->Execute();
            Concurrency::wait(100);
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 100.0f));
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 110.0f));
            Concurrency::wait(100);
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 100.0f));
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 110.0f));

            formulaTree = new FormulaTree("NUMBER", "3");
            brick = new RepeatBrick(spriteReference, formulaTree, script);
            formulaTree = new FormulaTree("NUMBER", "10");
            changeSizeBrick = new ChangeSizeByBrick(spriteReference, formulaTree, script);
            brick->AddBrick(changeSizeBrick);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 110.0f));
            brick->Execute();
            Concurrency::wait(100);
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 110.0f));
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 140.0f));
            Concurrency::wait(100);
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 110.0f));
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 140.0f));

            formulaTree = new FormulaTree("NUMBER", "0");
            brick = new RepeatBrick(spriteReference, formulaTree, script);
            formulaTree = new FormulaTree("NUMBER", "10");
            changeSizeBrick = new ChangeSizeByBrick(spriteReference, formulaTree, script);
            brick->AddBrick(changeSizeBrick);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 140.0f));
            brick->Execute();
            Concurrency::wait(100);
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 140.0f));
            Concurrency::wait(100);
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 140.0f));

            formulaTree = new FormulaTree("NUMBER", "-1");
            brick = new RepeatBrick(spriteReference, formulaTree, script);
            formulaTree = new FormulaTree("NUMBER", "10");
            changeSizeBrick = new ChangeSizeByBrick(spriteReference, formulaTree, script);
            brick->AddBrick(changeSizeBrick);

            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 140.0f));
            brick->Execute();
            Concurrency::wait(100);
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 140.0f));
            Concurrency::wait(100);
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 140.0f));
		}

		TEST_METHOD(ControlBricks_WaitBrick)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "1");
			string spriteReference = "";
			Object *object = new Object("TestObject");
            StartScript *script = new StartScript(spriteReference, object);

            WaitBrick *brick = new WaitBrick(spriteReference, formulaTree, script);
            script->addBrick(brick);

            formulaTree = new FormulaTree("NUMBER", "40");
            SetSizeToBrick *setSizeBrick = new SetSizeToBrick(spriteReference, formulaTree, script);
            script->addBrick(setSizeBrick);
            
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            script->Execute();
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 40.0f));
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            Concurrency::wait(100);
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 40.0f));
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            Concurrency::wait(100);
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 40.0f));
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            Concurrency::wait(1000);
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 40.0f));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 100.0f));
		}

        TEST_METHOD(ControlBricks_WaitBrick_UnderflowCheck)
		{
			FormulaTree *formulaTree = new FormulaTree("NUMBER", "-10");
			string spriteReference = "";
			Object *object = new Object("TestObject");
            StartScript *script = new StartScript(spriteReference, object);

            WaitBrick *brick = new WaitBrick(spriteReference, formulaTree, script);
            script->addBrick(brick);

            formulaTree = new FormulaTree("NUMBER", "40");
            SetSizeToBrick *setSizeBrick = new SetSizeToBrick(spriteReference, formulaTree, script);
            script->addBrick(setSizeBrick);
            
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 100.0f));
            script->Execute();
            Concurrency::wait(100);
            Assert::IsTrue(TestHelper::isEqual(object->GetScale(), 40.0f));
            Assert::IsFalse(TestHelper::isEqual(object->GetScale(), 100.0f));
		}
	};
}