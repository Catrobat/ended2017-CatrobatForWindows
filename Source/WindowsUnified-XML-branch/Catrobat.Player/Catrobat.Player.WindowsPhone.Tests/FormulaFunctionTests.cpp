#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

#include "Object.h"
#include "FormulaTree.h"
#include "Interpreter.h"
#include <ctime>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace PlayerWindowsPhone8Test
{
	TEST_CLASS(FormulaFunctionTests)
	{
	public:

		TEST_METHOD(FunctionTrue)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "TRUE");

			double expected = 1.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionFalse)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "FALSE");

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSIN1)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "SIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSIN2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "SIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "90.0"));

			double expected = 1.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSIN3)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "SIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "30.0"));

			float expected = (float)0.5;
			float actual = (float) interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionCOS1)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "COS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 1.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionCOS2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "COS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "90.0"));

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionCOS3)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "COS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "60.0"));

			float expected = (float)0.5;
			float actual = (float) interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionCOS4)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "COS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "45.0"));

			float expected = (float)0.707106781;
			float actual = (float) interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionCOS5)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "COS");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "4.0"));

			float expected = (float)0.99756405;
			float actual = (float) interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionCOS6)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "COS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "270.0"));

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionCOS7)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "COS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "180.0"));

			double expected = -1.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionTAN1)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "TAN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionTAN2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "TAN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "45.0"));

			float expected = (float)1.0;
			float actual = (float) interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLN1)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "LN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLN2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "LN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "2.0"));

			float expected = (float)0.693147181;
			float actual = (float) interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLN3)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "LN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "2.718281828"));

			float expected = (float)1.0;
			float actual = (float) interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLN4)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "LN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = -1.0; // TODO: test Exception
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLOG1)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "LOG");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLOG2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "LOG");
			formula->SetLeftChild(new FormulaTree("NUMBER", "10.0"));

			double expected = 1.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLOG3)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "LOG");
			formula->SetLeftChild(new FormulaTree("NUMBER", "2.0"));

			float expected = (float)0.301029996;
			float actual = (float) interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLOG4)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "LOG");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = -1.0; // TODO: test Exception
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSQRT0)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "SQRT");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSQRT16)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "SQRT");
			formula->SetLeftChild(new FormulaTree("NUMBER", "16.0"));

			double expected = 4.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSQRT15376)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "SQRT");
			formula->SetLeftChild(new FormulaTree("NUMBER", "15376.0"));

			double expected = 124.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionSQRTdezimal)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "SQRT");
			formula->SetLeftChild(new FormulaTree("NUMBER", "151.29"));

			double expected = 12.3;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual((float) expected, (float) actual);
		}

		TEST_METHOD(FunctionRAND)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "RAND");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7.0"));

			for (int i = 0; i < 100; i++)
			{

				double actual = interpreter->EvaluateFormula(formula, object);
				if (actual >= 1.0 && actual <= 7.0)
					Assert::IsTrue(true);
				else
					Assert::Fail();
			}
		}

		TEST_METHOD(FunctionRANDdezimal)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "RAND");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.2"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7.8"));


			for (int i = 0; i < 100; i++)
			{

				double actual = interpreter->EvaluateFormula(formula, object);
				if (actual >= 1.2 && actual <= 7.8)
					Assert::IsTrue(true);
				else
					Assert::Fail();
			}
		}

		TEST_METHOD(FunctionRAND2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "RAND");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7.0"));

			for (int i = 0; i < 100; i++)
			{
				double actual = interpreter->EvaluateFormula(formula, object);
				if (actual >= -1.0 && actual <= 7.0)
					Assert::IsTrue(true);
				else
					Assert::Fail();
			}
		}

		TEST_METHOD(FunctionABS0)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ABS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionABSn12)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

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
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ABS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "12.0"));

			double expected = 12.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionABSndezimal)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

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
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ROUND");
			formula->SetLeftChild(new FormulaTree("NUMBER", "12.4"));

			double expected = 12.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionROUND2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ROUND");
			formula->SetLeftChild(new FormulaTree("NUMBER", "12.5"));

			double expected = 13.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionROUNDNegative1)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ROUND");
			formula->SetLeftChild(new FormulaTree("NUMBER", "-12.5"));

			double expected = -13.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionROUNDNegative2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ROUND");
			formula->SetLeftChild(new FormulaTree("NUMBER", "-12.4"));

			double expected = -12.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}


		TEST_METHOD(FunctionPI)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "PI");

			float expected = (float)3.141592654;
			float actual = (float) interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMOD1)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MOD");
			formula->SetLeftChild(new FormulaTree("NUMBER", "12.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "3.0"));

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMOD2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MOD");
			formula->SetLeftChild(new FormulaTree("NUMBER", "12.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "5.0"));

			double expected = 2.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMOD3)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MOD");
			formula->SetLeftChild(new FormulaTree("NUMBER", "17.4"));
			formula->SetRightChild(new FormulaTree("NUMBER", "3.0"));

			float expected = (float)2.4;
			float actual = (float) interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMOD4)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MOD");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "12.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "3.0"));

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMOD5)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MOD");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "12.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7.0"));

			double expected = 2.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMOD6)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MOD");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "17.4"));
			formula->SetRightChild(new FormulaTree("NUMBER", "3.0"));

			float expected = (float)0.6;
			float actual = (float) interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMOD7)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MOD");
			formula->SetLeftChild(new FormulaTree("NUMBER", "12.0"));
			formula->SetRightChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "5.0"));

			double expected = -3.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMOD8)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MOD");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "12.0"));
			formula->SetRightChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "5.0"));

			double expected = -2.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCSIN1)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCSIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCSIN2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCSIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 90.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCSIN3)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCSIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.5"));

			double expected = 30.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual((float) expected, (float) actual);
		}

		TEST_METHOD(FunctionARCSIN4)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCSIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.4"));

			double expected = 23.57817848;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual((float) expected, (float) actual);
		}

		TEST_METHOD(FunctionARCSIN5)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCSIN");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));

			double expected = -90.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCCOS1)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCCOS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCCOS2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCCOS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.5"));

			double expected = 60.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual((float) expected, (float) actual);
		}

		TEST_METHOD(FunctionARCCOS3)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCCOS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 90.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCCOS4)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCCOS");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "0.5"));

			double expected = 120.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual((float) expected, (float) actual);
		}

		TEST_METHOD(FunctionARCCOS5)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCCOS");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 180.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCCOS6)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCCOS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.4"));

			double expected = 66.42182152;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual((float) expected, (float) actual);
		}

		TEST_METHOD(FunctionARCTAN1)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCTAN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 0.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCTAN2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCTAN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 45.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCTAN3)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCTAN");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));

			double expected = -45.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCTAN4)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCTAN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.5"));

			double expected = 26.56505118;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual((float) expected, (float) actual);
		}

		TEST_METHOD(FunctionEXP1)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "EXP");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 1.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionEXP2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "EXP");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 2.718281828;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual((float) expected, (float) actual);
		}

		TEST_METHOD(FunctionEXP3)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "EXP");
			formula->SetLeftChild(new FormulaTree("NUMBER", "2.0"));

			double expected = 7.389056099;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual((float) expected, (float) actual);
		}

		TEST_METHOD(FunctionEXP4)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "EXP");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 0.367879441;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual((float) expected, (float) actual);
		}

		TEST_METHOD(FunctionMAX1)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MAX");
			formula->SetLeftChild(new FormulaTree("NUMBER", "2.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7.0"));

			double expected = 7.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMAX2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MAX");
			formula->SetLeftChild(new FormulaTree("NUMBER", "7.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "2.0"));

			double expected = 7.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMAX3)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MAX");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7.0"));

			double expected = 7.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMAX4)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MAX");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));
			formula->SetRightChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "7.0"));

			double expected = -1.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMIN1)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "2.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7.0"));

			double expected = 2.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMIN2)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "7.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "2.0"));

			double expected = 2.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMIN3)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MIN");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7.0"));

			double expected = -1.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMIN4)
		{
			Interpreter *interpreter = Interpreter::Instance();
			auto object(shared_ptr<Object>(new Object("TestObject")));

			FormulaTree *formula = new FormulaTree("FUNCTION", "MIN");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));
			formula->SetRightChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "7.0"));

			double expected = -7.0;
			double actual = interpreter->EvaluateFormula(formula, object);
			Assert::AreEqual(expected, actual);
		}

	};
}
