#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

#include "Object.h"
#include "FormulaTree.h"
#include "Interpreter.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace PlayerWindowsPhone8Test
{

	const string L_TRUE = "TRUE";
	const string L_FALSE = "FALSE";
	const string L_FUNCTION = "FUNCTION";
	const string L_AND = "LOGICAL_AND";
	const string L_OR = "LOGICAL_OR";
	const string OP = "OPERATOR";
	const string FUN = "FUNCTION";
	const string EQUAL = "EQUAL";

	TEST_CLASS(LogicOperations)
	{
	public:

		TEST_METHOD(Formula_Logic_TRUE_AND_TRUE)
		{
			FormulaTree *formula = new FormulaTree(OP, L_AND);
			formula->SetLeftChild(new FormulaTree(FUN, L_TRUE));
			formula->SetRightChild(new FormulaTree(FUN, L_TRUE));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			//Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, object), TRUE);
			//Assert::IsTrue(interpreter->EvaluateFormulaToBool(formula, object));
			Assert::IsTrue(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_TRUE_AND_FALSE)
		{
			FormulaTree *formula = new FormulaTree(OP, L_AND);
			formula->SetLeftChild(new FormulaTree(FUN, L_TRUE));
			formula->SetRightChild(new FormulaTree(FUN, L_FALSE));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsFalse(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_FALSE_AND_TRUE)
		{
			FormulaTree *formula = new FormulaTree(OP, L_AND);
			formula->SetLeftChild(new FormulaTree(FUN, L_FALSE));
			formula->SetRightChild(new FormulaTree(FUN, L_TRUE));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsFalse(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_FALSE_AND_FALSE)
		{
			FormulaTree *formula = new FormulaTree(OP, L_AND);
			formula->SetLeftChild(new FormulaTree(FUN, L_FALSE));
			formula->SetRightChild(new FormulaTree(FUN, L_FALSE));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsFalse(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_TRUE_OR_TRUE)
		{
			FormulaTree *formula = new FormulaTree(OP, L_OR);
			formula->SetLeftChild(new FormulaTree(FUN, L_TRUE));
			formula->SetRightChild(new FormulaTree(FUN, L_TRUE));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsTrue(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_TRUE_OR_FALSE)
		{
			FormulaTree *formula = new FormulaTree(OP, L_OR);
			formula->SetLeftChild(new FormulaTree(FUN, L_TRUE));
			formula->SetRightChild(new FormulaTree(FUN, L_FALSE));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsTrue(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_FALSE_OR_TRUE)
		{
			FormulaTree *formula = new FormulaTree(OP, L_OR);
			formula->SetLeftChild(new FormulaTree(FUN, L_FALSE));
			formula->SetRightChild(new FormulaTree(FUN, L_TRUE));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsTrue(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_FALSE_OR_FALSE)
		{
			FormulaTree *formula = new FormulaTree(OP, L_OR);
			formula->SetLeftChild(new FormulaTree(FUN, L_FALSE));
			formula->SetRightChild(new FormulaTree(FUN, L_FALSE));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsFalse(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_EQUAL)
		{
			FormulaTree *formula = new FormulaTree(OP, EQUAL);
			formula->SetLeftChild(new FormulaTree("NUMBER", "12"));
			formula->SetRightChild(new FormulaTree("NUMBER", "12"));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsTrue(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_EQUAL_2)
		{
			FormulaTree *formula = new FormulaTree(OP, EQUAL);
			formula->SetLeftChild(new FormulaTree("NUMBER", "12.3"));
			formula->SetRightChild(new FormulaTree("NUMBER", "12.3"));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsTrue(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_EQUAL_3)
		{
			FormulaTree *formula = new FormulaTree(OP, EQUAL);
			formula->SetLeftChild(new FormulaTree("NUMBER", "12.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "12.1"));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsFalse(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_EQUAL_4)
		{
			FormulaTree *formula = new FormulaTree(OP, EQUAL);
			formula->SetLeftChild(new FormulaTree("NUMBER", "12"));
			formula->SetRightChild(new FormulaTree("NUMBER", "12.000"));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsTrue(interpreter->EvaluateFormulaToBool(formula, object));
		}


		TEST_METHOD(Formula_Logic_NOT_EQUAL)
		{
			FormulaTree *formula = new FormulaTree(OP, "NOT_EQUAL");
			formula->SetLeftChild(new FormulaTree("NUMBER", "12"));
			formula->SetRightChild(new FormulaTree("NUMBER", "12"));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsFalse(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_NOT_EQUAL_2)
		{
			FormulaTree *formula = new FormulaTree(OP, "NOT_EQUAL");
			formula->SetLeftChild(new FormulaTree(FUN, L_TRUE));
			formula->SetRightChild(new FormulaTree(FUN, L_TRUE));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsFalse(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_NOT_EQUAL_3)
		{
			FormulaTree *formula = new FormulaTree(OP, "NOT_EQUAL");
			formula->SetLeftChild(new FormulaTree(FUN, L_TRUE));
			formula->SetRightChild(new FormulaTree(FUN, L_FALSE));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsTrue(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_NOT_EQUAL_4)
		{
			FormulaTree *formula = new FormulaTree(OP, "NOT_EQUAL");
			formula->SetLeftChild(new FormulaTree("NUMBER", "12.0000"));
			formula->SetRightChild(new FormulaTree("NUMBER", "12"));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsFalse(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_More_Complex)
		{
			FormulaTree *formula = new FormulaTree(OP, L_AND);
			formula->SetLeftChild(new FormulaTree(FUN, L_TRUE));
			formula->SetRightChild(new FormulaTree(OP, EQUAL));
			formula->GetRightChild()->SetLeftChild(new FormulaTree(FUN, L_FALSE));
			formula->GetRightChild()->SetRightChild(new FormulaTree(FUN, L_TRUE));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsFalse(interpreter->EvaluateFormulaToBool(formula, object));
		}

		TEST_METHOD(Formula_Logic_More_Complex_2)
		{
			FormulaTree *formula = new FormulaTree(OP, L_AND);
			formula->SetLeftChild(new FormulaTree(FUN, L_TRUE));
			formula->SetRightChild(new FormulaTree(OP, EQUAL));
			formula->GetRightChild()->SetLeftChild(new FormulaTree(FUN, L_FALSE));
			formula->GetRightChild()->SetRightChild(new FormulaTree(FUN, L_FALSE));

			auto object(shared_ptr<Object>(new Object("TestObject")));
			Interpreter *interpreter = Interpreter::Instance();

			Assert::IsTrue(interpreter->EvaluateFormulaToBool(formula, object));
		}

	};
}

