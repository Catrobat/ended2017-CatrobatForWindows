#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

#include "Object.h"
#include "FormulaTree.h"
#include "Interpreter.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
	enum Type
	{
		OPERATOR,
		NUMBER,
		USER_VARIABLE
	};

	TEST_CLASS(BasicArithmeticalOperations)
	{
	public:
		
		TEST_METHOD(Formula_BasicArithmetical_Number_int)
		{
			FormulaTree *formula = new FormulaTree("NUMBER", "10");
			Object *object = new Object("TestObject");
			Interpreter *interpreter = Interpreter::Instance();

			Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, NULL), int(10));
		}

        //10 + 7 = 17
		TEST_METHOD(Formula_BasicArithmetical_Add1_int)
		{
			FormulaTree *formula = new FormulaTree("OPERATOR", "PLUS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "10"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7"));
			Object *object = new Object("TestObject");
			Interpreter *interpreter = Interpreter::Instance();

			int expected = 10+7;

			Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, object), expected);
		}

        //0 + 3 = 3
		TEST_METHOD(Formula_BasicArithmetical_Add2_int)
		{
			FormulaTree *formula = new FormulaTree("OPERATOR", "PLUS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "3"));
			Object *object = new Object("TestObject");
			Interpreter *interpreter = Interpreter::Instance();

			int expected = 0+3;

			Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, object), expected);
		}

        //-3 + 7 = 4
		TEST_METHOD(Formula_BasicArithmetical_Add3_int)
		{
			FormulaTree *formula = new FormulaTree("OPERATOR", "PLUS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "-3"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7"));
			Object *object = new Object("TestObject");
			Interpreter *interpreter = Interpreter::Instance();

			int expected = 4;

			Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, object), expected);
		}

        //-10 + 7 = -3
		TEST_METHOD(Formula_BasicArithmetical_Add4_int)
		{
			FormulaTree *formula = new FormulaTree("OPERATOR", "PLUS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "-10"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7"));
			Object *object = new Object("TestObject");
			Interpreter *interpreter = Interpreter::Instance();

			int expected = -3; 

			Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, object), expected);
		}

        //10 + 3 + 7 = 20
        TEST_METHOD(Formula_BasicArithmetical_Add_Rekursion_int)
		{
			FormulaTree *formula = new FormulaTree("OPERATOR", "PLUS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "10"));
			formula->SetRightChild(new FormulaTree("OPERATOR", "PLUS"));
            formula->getRightChild()->SetLeftChild(new FormulaTree("NUMBER", "3"));
            formula->getRightChild()->SetRightChild(new FormulaTree("NUMBER", "7"));
			Object *object = new Object("TestObject");
			Interpreter *interpreter = Interpreter::Instance();

			int expected = 20;

			Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, object), expected);
		}

	};
}
