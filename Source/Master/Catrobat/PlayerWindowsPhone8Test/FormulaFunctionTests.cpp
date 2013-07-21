#include "pch.h"
#include "CppUnitTest.h"

#include "Object.h"
#include "FormulaTree.h"
#include "Interpreter.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
	TEST_CLASS(FormulaFunctionTests)
	{
	public:
		
		TEST_METHOD(FunctionTrue)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "TRUE");

			double expected = 1.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionFalse)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "FALSE");

			double expected = 0.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSIN0)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "SIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 0.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSIN90)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "SIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "90.0"));

			double expected = 1.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSIN30)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "SIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "30.0"));

			double expected = 0.5;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionCOS0)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "COS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 1.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionCOS90)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "COS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "90.0"));

			double expected = 1.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionCOS60)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "COS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "60.0"));

			double expected = 0.5;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionTAN0)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "TAN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 0.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionTAN45)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "TAN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "45.0"));

			double expected = 1.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLN1)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "LN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 0.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLN2)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "LN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "2.0"));

			double expected = 0.69314718;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLN0)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "LN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = -1.0; // TODO: test Exception
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLOG1)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "LOG");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 0.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLOG10)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "LOG");
			formula->SetLeftChild(new FormulaTree("NUMBER", "10.0"));

			double expected = 1.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLOG2)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "LOG");
			formula->SetLeftChild(new FormulaTree("NUMBER", "2.0"));

			double expected = 0.301029996;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLOG0)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "LOG");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = -1.0; // TODO: test Exception
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSQRT0)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "SQRT");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 0.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSQRT16)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "SQRT");
			formula->SetLeftChild(new FormulaTree("NUMBER", "16.0"));

			double expected = 4.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSQRT15376)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "SQRT");
			formula->SetLeftChild(new FormulaTree("NUMBER", "15376.0"));

			double expected = 124.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSQRTdezimal)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "SQRT");
			formula->SetLeftChild(new FormulaTree("NUMBER", "151.29"));

			double expected = 12.3;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionRAND)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "RAND");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7.0"));

            double actual = interpreter->EvaluateFormula(formula, object);
			if (actual >= 1.0 && actual <= 7.0)
				Assert::IsTrue(true);
			else
				Assert::Fail();

			double actual2 = interpreter->EvaluateFormula(formula, object);
			if (actual2 >= 1.0 && actual2 <= 7.0 && actual2 != actual)
				Assert::IsTrue(true);
			else
				Assert::Fail();
		}

		TEST_METHOD(FunctionRANDdezimal)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "RAND");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.2"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7.8"));

            double actual = interpreter->EvaluateFormula(formula, object);
			if (actual >= 1.2 && actual <= 7.8)
				Assert::IsTrue(true);
			else
				Assert::Fail();

			double actual2 = interpreter->EvaluateFormula(formula, object);
			if (actual2 >= 1.2 && actual2 <= 7.8 && actual2 != actual)
				Assert::IsTrue(true);
			else
				Assert::Fail();
		}

		TEST_METHOD(FunctionRAND2)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "RAND");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7.0"));

            double actual = interpreter->EvaluateFormula(formula, object);
			if (actual >= -1.0 && actual <= 7.0)
				Assert::IsTrue(true);
			else
				Assert::Fail();

			double actual2 = interpreter->EvaluateFormula(formula, object);
			if (actual2 >= -1.0 && actual2 <= 7.0 && actual2 != actual)
				Assert::IsTrue(true);
			else
				Assert::Fail();
		}

		TEST_METHOD(FunctionABS0)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ABS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 0.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionABSn12)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ABS");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "12.0"));

			double expected = 12.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionABS12)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ABS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "12.0"));

			double expected = 12.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionABSndezimal)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ABS");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "12.346"));

			double expected = 12.346;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionROUND1)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ROUND");
			formula->SetLeftChild(new FormulaTree("NUMBER", "12.4"));

			double expected = 12.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionROUND2)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ROUND");
			formula->SetLeftChild(new FormulaTree("NUMBER", "12.5"));

			double expected = 13.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionPI)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "PI");

			double expected = 3.141592654;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

	};
}