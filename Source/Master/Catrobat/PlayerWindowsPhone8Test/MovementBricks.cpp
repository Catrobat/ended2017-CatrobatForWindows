#include "pch.h"
#include "CppUnitTest.h"
#include "Object.h"
#include "StartScript.h"
#include "ChangeXByBrick.h"
#include "ChangeYByBrick.h"
#include "GlideToBrick.h"
#include "PlaceAtBrick.h"
#include "PointToBrick.h"
#include "SetXBrick.h"
#include "SetYBrick.h"
#include "TurnLeftBrick.h"
#include "TurnRightBrick.h"
#include "FormulaTree.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
    TEST_CLASS(MovementBricks)
    {
    public:
        TEST_METHOD(MovementBricks_ChangeXByBrick)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "42");
			Brick* brick = new ChangeXByBrick("", tree, script);
			
			float old_x = 0;
			float old_y = 0;
			object->SetPosition(old_x, old_y);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(x, old_x + 42);
        }

		TEST_METHOD(MovementBricks_ChangeYByBrick)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "42");
			Brick* brick = new ChangeYByBrick("", tree, script);
			
			float old_x = 0;
			float old_y = 0;
			object->SetPosition(old_x, old_y);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(y, old_y + 42);
        }

		TEST_METHOD(MovementBricks_GlideToBrick)
        {
            // TODO: Your test code here
			Assert::IsTrue(false);
        }

		TEST_METHOD(MovementBricks_PlaceAtBrick)
        {
            // TODO: Your test code here
			Assert::IsTrue(false);
        }

		TEST_METHOD(MovementBricks_PointToBrick)
        {
            // TODO: Your test code here
			Assert::IsTrue(false);
        }

		TEST_METHOD(MovementBricks_SetXBrick)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "42");
			Brick* brick = new SetXBrick("", tree, script);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(x, 42.0f);
        }

		TEST_METHOD(MovementBricks_SetYByBrick)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "42");
			Brick* brick = new SetYBrick("", tree, script);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(y, 42.0f);
        }

		TEST_METHOD(MovementBricks_TurnLeftBrick)
        {
            // TODO: Your test code here
			Assert::IsTrue(false);
        }

		TEST_METHOD(MovementBricks_TurnRightBrick)
        {
            // TODO: Your test code here
			Assert::IsTrue(false);
        }
    };
}