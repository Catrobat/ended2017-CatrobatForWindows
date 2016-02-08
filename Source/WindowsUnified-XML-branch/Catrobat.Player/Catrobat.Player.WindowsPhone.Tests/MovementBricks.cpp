#include "pch.h"
#include "CppUnitTest.h"
#include "Object.h"
#include "StartScript.h"
#include "ChangeXByBrick.h"
#include "ChangeYByBrick.h"
#include "GlideToBrick.h"
#include "TestHelper.h"
#include "PlaceAtBrick.h"
#include "PointToBrick.h"
#include "SetXBrick.h"
#include "SetYBrick.h"
#include "TurnLeftBrick.h"
#include "TurnRightBrick.h"

#include <math.h>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace PlayerWindowsPhone8Test
{
    TEST_CLASS(MovementBricks)
    {
    private:
		shared_ptr<Object> m_object;
        shared_ptr<Script> m_script;
        FormulaTree* m_tree;
        FormulaTree* m_tree2;
        FormulaTree* m_tree3;
        Brick* m_brick;

        TEST_METHOD_INITIALIZE(TestInitialization)
        {
			m_object = shared_ptr<Object>(new Object(""));
            m_script = std::shared_ptr<StartScript>(new StartScript(m_object));
            m_tree = NULL;
            m_tree2 = NULL;
            m_tree3 = NULL;
            m_brick = NULL;
        }

        TEST_METHOD_CLEANUP(TestCleanup)
        {

        }

    public:
        TEST_METHOD(MovementBricks_ChangeXByBrick)
        {
            m_tree = new FormulaTree("NUMBER", "42");
            m_brick = new ChangeXByBrick(m_tree, m_script);

            float old_x = 0;
            float old_y = 0;
            float x = 0;
            float y = 0;

            m_object->SetTranslation(old_x, old_y);

            m_brick->Execute();
            m_object->GetTranslation(x, y);
            Assert::AreEqual(old_x + 42, x);
            Assert::AreEqual(old_y, y);

            m_brick->Execute();
            m_object->GetTranslation(x, y);
            Assert::AreEqual(old_x + 84, x);
            Assert::AreEqual(old_y, y);
        }

        TEST_METHOD(MovementBricks_ChangeXByBrick_Negative)
        {
            m_tree = new FormulaTree("NUMBER", "-42");
            m_brick = new ChangeXByBrick(m_tree, m_script);

            float old_x = 0;
            float old_y = 0;
            float x = 0;
            float y = 0;

            m_object->SetTranslation(old_x, old_y);

            m_brick->Execute();
            m_object->GetTranslation(x, y);
            Assert::AreEqual(x, old_x - 42);
            Assert::AreEqual(y, old_y);

            m_brick->Execute();
            m_object->GetTranslation(x, y);
            Assert::AreEqual(x, old_x - 84);
            Assert::AreEqual(y, old_y);
        }

        TEST_METHOD(MovementBricks_ChangeXByBrick_zero)
        {
            m_tree = new FormulaTree("NUMBER", "0");
            m_brick = new ChangeXByBrick(m_tree, m_script);

            float old_x = 0;
            float old_y = 0;
            float x = 0;
            float y = 0;

            m_object->SetTranslation(old_x, old_y);

            m_brick->Execute();
            m_object->GetTranslation(x, y);
            Assert::AreEqual(x, old_x);
            Assert::AreEqual(y, old_y);

            m_brick->Execute();
            m_object->GetTranslation(x, y);
            Assert::AreEqual(x, old_x);
            Assert::AreEqual(y, old_y);
        }

        TEST_METHOD(MovementBricks_ChangeYByBrick)
        {
            m_tree = new FormulaTree("NUMBER", "42");
            m_brick = new ChangeYByBrick(m_tree, m_script);

            float old_x = 0;
            float old_y = 0;
            float x = 0;
            float y = 0;

            m_object->SetTranslation(old_x, old_y);

            m_brick->Execute();
            m_object->GetTranslation(x, y);
            Assert::AreEqual(x, old_x);
            Assert::AreEqual(y, old_y - 42);

            m_brick->Execute();
            m_object->GetTranslation(x, y);
            Assert::AreEqual(x, old_x);
            Assert::AreEqual(y, old_y - 84);
        }

        TEST_METHOD(MovementBricks_ChangeYByBrick_Negative)
        {
            m_tree = new FormulaTree("NUMBER", "-42");
            m_brick = new ChangeYByBrick(m_tree, m_script);

            float old_x = 0;
            float old_y = 0;
            float x = 0;
            float y = 0;

            m_object->SetTranslation(old_x, old_y);

            m_brick->Execute();
            m_object->GetTranslation(x, y);
            Assert::AreEqual(x, old_x);
            Assert::AreEqual(y, old_y + 42);

            m_brick->Execute();
            m_object->GetTranslation(x, y);
            Assert::AreEqual(x, old_x);
            Assert::AreEqual(y, old_y + 84);
        }

        TEST_METHOD(MovementBricks_ChangeYByBrick_Zero)
        {
            m_tree = new FormulaTree("NUMBER", "0");
            m_brick = new ChangeYByBrick(m_tree, m_script);

            float old_x = 0;
            float old_y = 0;
            float x = 0;
            float y = 0;

            m_object->SetTranslation(old_x, old_y);

            m_brick->Execute();
            m_object->GetTranslation(x, y);
            Assert::AreEqual(x, old_x);
            Assert::AreEqual(y, old_y);

            m_brick->Execute();
            m_object->GetTranslation(x, y);
            Assert::AreEqual(x, old_x);
            Assert::AreEqual(y, old_y);
        }

        TEST_METHOD(MovementBricks_GlideToBrick_Variance1)
        {
            m_tree = new FormulaTree("NUMBER", "42");
            m_tree2 = new FormulaTree("NUMBER", "23");
            m_tree3 = new FormulaTree("NUMBER", "1");
            Brick* brick = new GlideToBrick(m_tree, m_tree2, m_tree3, m_script);
            m_object->SetTranslation(0, 0);

            brick->Execute();

            float x = 0;
            float y = 0;
            m_object->GetTranslation(x, y);

            auto expected = 42.0f;
            auto actual = std::round(x);
            Assert::AreEqual(expected, actual);

            expected = 23.0f;
            actual = std::round(y);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(MovementBricks_GlideToBrick_Variance2)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<StartScript>(new StartScript(object)));
            auto tree1 = new FormulaTree("NUMBER", "123");
            auto tree2 = new FormulaTree("NUMBER", "456");
            auto tree3 = new FormulaTree("NUMBER", "1");
            auto brick = new GlideToBrick(tree1, tree2, tree3, script);
            object->SetTranslation(10, 20);

            brick->Execute();

            auto x = 0.0f;
            auto y = 0.0f;
            object->GetTranslation(x, y);

            auto expected = 123.0f;
            auto actual = std::round(x);
            Assert::AreEqual(expected, actual);

            expected = 456.0f;
            actual = std::round(y);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(MovementBricks_PlaceAtBrick_Variance1)
        {
            auto object(shared_ptr<Object>(new Object("")));
			auto script(shared_ptr<Script>(new StartScript(object)));
            FormulaTree* tree1 = new FormulaTree("NUMBER", "23");
            FormulaTree* tree2 = new FormulaTree("NUMBER", "32");
            Brick* brick = new PlaceAtBrick(tree1, tree2, script);

            brick->Execute();

            float x = 0;
            float y = 0;
            object->GetTranslation(x, y);

            auto expected = 23.0f;
            auto actual = std::round(x);

            Assert::AreEqual(expected, actual);

            expected = 32.0f;
            actual = std::round(y);

            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(MovementBricks_PlaceAtBrick_Variance2)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            FormulaTree* tree1 = new FormulaTree("NUMBER", "23");
            FormulaTree* tree2 = new FormulaTree("NUMBER", "32");
            Brick* brick = new PlaceAtBrick(tree1, tree2, script);

            brick->Execute();

            float x, y;
            object->GetTranslation(x, y);

            auto expected = 23.0f;
            auto actual = std::round(x);
            Assert::AreEqual(expected, actual);

            expected = 32.0f;
            actual = std::round(y);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(MovementBricks_PointToBrick_Variance1)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            FormulaTree* tree = new FormulaTree("NUMBER", "42");
            Brick* brick = new PointToBrick(tree, script);
            object->SetRotation(0.0f);

            brick->Execute();

            float r = object->GetRotation();

            Assert::AreEqual(42.0f, r);

            brick->Execute();

            r = object->GetRotation();

            Assert::AreEqual(42.0f, r);
        }

        TEST_METHOD(MovementBricks_PointToBrick_Variance2)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            FormulaTree* tree = new FormulaTree("NUMBER", "23");
            Brick* brick = new PointToBrick(tree, script);
            object->SetRotation(0.0f);

            brick->Execute();

            float r = object->GetRotation();

            Assert::AreEqual(23.0f, r);

            brick->Execute();

            r = object->GetRotation();

            Assert::AreEqual(23.0f, r);
        }

        TEST_METHOD(MovementBricks_SetXBrick)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            FormulaTree* tree = new FormulaTree("NUMBER", "42");
            Brick* brick = new SetXBrick(tree, script);

            brick->Execute();

            float x = 0;
            float y = 0;
            object->GetTranslation(x, y);

            Assert::AreEqual(42.0f, x);
            Assert::AreEqual(0.0f, y);
        }

        TEST_METHOD(MovementBricks_SetXBrick_Negative)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            FormulaTree* tree = new FormulaTree("NUMBER", "-42");
            Brick* brick = new SetXBrick(tree, script);

            brick->Execute();

            float x = 0;
            float y = 0;
            object->GetTranslation(x, y);

            Assert::AreEqual(-42.0f, x);
            Assert::AreEqual(0.0f, y);
        }

        TEST_METHOD(MovementBricks_SetXBrick_Zero)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            FormulaTree* tree = new FormulaTree("NUMBER", "0");
            Brick* brick = new SetXBrick(tree, script);

            brick->Execute();

            float x = 0;
            float y = 0;
            object->GetTranslation(x, y);

            Assert::AreEqual(0.0f, x);
            Assert::AreEqual(0.0f, y);
        }

        TEST_METHOD(MovementBricks_SetXBrick_FromOtherPosition)
        {
            auto wrongX = 27.5f;
            auto expectedX = 15.1f;
            auto expectedY = 12.4f;

            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            FormulaTree* tree = new FormulaTree("NUMBER",
                TestHelper::ConvertPlatformStringToString(expectedX.ToString()));
            Brick* brick = new SetXBrick(tree, script);

            object->SetTranslation(wrongX, expectedY);

            brick->Execute();

            float actualX, actualY;
            object->GetTranslation(actualX, actualY);

            Assert::AreEqual(expectedX, actualX);
            Assert::AreEqual(expectedY, actualY);
        }

        TEST_METHOD(MovementBricks_SetYBrick)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            auto tree = new FormulaTree("NUMBER", "42");
            auto brick = new SetYBrick(tree, script);

            brick->Execute();

            float actualX, actualY;
            object->GetTranslation(actualX, actualY);

            auto expectedY = 42.0f;

            Assert::AreEqual(actualY, expectedY);
        }

        TEST_METHOD(MovementBricks_SetYBrick_Negative)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            auto tree = new FormulaTree("NUMBER", "-42");
            auto brick = new SetYBrick(tree, script);

            brick->Execute();

            float actualX, actualY;
            object->GetTranslation(actualX, actualY);

            auto expectedY = -42.0f;

            Assert::AreEqual(actualY, expectedY);
        }

        TEST_METHOD(MovementBricks_SetYBrick_Zero)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            auto tree = new FormulaTree("NUMBER", "0");
            auto brick = new SetYBrick(tree, script);

            brick->Execute();

            float actualX, actualY;
            object->GetTranslation(actualX, actualY);

            auto expectedY = 0.0f;

            Assert::AreEqual(expectedY, actualY);
        }

        TEST_METHOD(MovementBricks_TurnLeftBrick)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            auto tree = new FormulaTree("NUMBER", "42");
            auto brick = new TurnLeftBrick(tree, script);
            object->SetRotation(0.0f);

            brick->Execute();

            auto actual = object->GetRotation();
            auto expected = 360 - 42.0f;

            Assert::AreEqual(expected, actual);

            brick->Execute();

            actual = object->GetRotation();
            expected -= 42.0;

            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(MovementBricks_TurnLeftBrick_Zero)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            auto tree = new FormulaTree("NUMBER", "0");
            auto brick = new TurnLeftBrick(tree, script);
            object->SetRotation(23.0f);

            brick->Execute();

            auto actual = object->GetRotation();
            auto expected = 23.0f;

            Assert::AreEqual(expected, actual);

            brick->Execute();

            actual = object->GetRotation();

            Assert::AreEqual(actual, expected);
        }

        TEST_METHOD(MovementBricks_TurnLeftBrick_Negative)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            auto tree = new FormulaTree("NUMBER", "-42");
            auto brick = new TurnLeftBrick(tree, script);
            object->SetRotation(0.0f);

            brick->Execute();

            auto r = object->GetRotation();

            Assert::AreEqual(r, 42.0f);

            brick->Execute();

            r = object->GetRotation();

            Assert::AreEqual(r, 84.0f);
        }

        TEST_METHOD(MovementBricks_TurnRightBrick)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            auto tree = new FormulaTree("NUMBER", "42");
            auto brick = new TurnRightBrick(tree, script);
            object->SetRotation(0.0f);

            brick->Execute();

            auto r = object->GetRotation();

            Assert::AreEqual(r, 42.0f);

            brick->Execute();

            r = object->GetRotation();

            Assert::AreEqual(r, 84.0f);
        }

        TEST_METHOD(MovementBricks_TurnRightBrick_Zero)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            auto tree = new FormulaTree("NUMBER", "0");
            auto brick = new TurnRightBrick(tree, script);
            object->SetRotation(23.0f);

            brick->Execute();

            auto r = object->GetRotation();

            Assert::AreEqual(r, 23.0f);

            brick->Execute();

            r = object->GetRotation();

            Assert::AreEqual(r, 23.0f);
        }

        TEST_METHOD(MovementBricks_TurnRightBrick_Negative)
        {
            auto object(shared_ptr<Object>(new Object("")));
            auto script(shared_ptr<Script>(new StartScript(object)));
            auto tree = new FormulaTree("NUMBER", "-42");
            auto brick = new TurnRightBrick(tree, script);
            object->SetRotation(0.0f);

            brick->Execute();

            auto r = object->GetRotation();

            Assert::AreEqual(r, -42.0f);

            brick->Execute();

            r = object->GetRotation();

            Assert::AreEqual(r, -84.0f);
        }
    };
}