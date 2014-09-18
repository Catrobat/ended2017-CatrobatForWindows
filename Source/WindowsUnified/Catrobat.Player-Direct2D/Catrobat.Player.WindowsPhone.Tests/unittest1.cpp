#include "pch.h"
#include "CppUnitTest.h"

#include "Object.h"
#include "FormulaTree.h"
#include "Interpreter.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace Catrobat_Player_WindowsPhone_Tests
{
    TEST_CLASS(UnitTest1)
    {
    public:
		TEST_METHOD(Formula_BasicArithmetical_Number_int)
		{
			FormulaTree *formula = new FormulaTree("NUMBER", "10");
			Object *object = new Object("TestObject");
			Interpreter *interpreter = Interpreter::Instance();

			Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, NULL), int(10));
		}
    };
}