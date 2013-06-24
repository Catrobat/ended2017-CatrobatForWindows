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
			// TODO: Your test code here
			Assert::IsTrue(false);
		}

		TEST_METHOD(ControlBricks_IfBrick)
		{
			// TODO: Your test code here
			Assert::IsTrue(false);
		}

		TEST_METHOD(ControlBricks_RepeatBrick)
		{
			// TODO: Your test code here
			Assert::IsTrue(false);
		}

		TEST_METHOD(ControlBricks_WaitBrick)
		{
			// TODO: Your test code here
			Assert::IsTrue(false);
		}
	};
}