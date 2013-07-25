#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

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

			double expected = 0.69314718055994530941723212145818;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionLN0)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "LN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			//double expected = -1.0; // TODO: test Exception
			double expected = NULL;
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

			
			//s//leep(1000);

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

			//Sleep(1000);

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

			//Sleep(1000);

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

		TEST_METHOD(FunctionROUNDNegative1)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ROUND");
			formula->SetLeftChild(new FormulaTree("NUMBER", "-12.5"));

			double expected = -13.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}
	    
		TEST_METHOD(FunctionROUNDNegative2)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ROUND");
			formula->SetLeftChild(new FormulaTree("NUMBER", "-12.4"));

			double expected = -12.0;
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

		TEST_METHOD(FunctionMOD1)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "MOD");
			formula->SetLeftChild(new FormulaTree("NUMBER", "17.4"));
			formula->SetRightChild(new FormulaTree("NUMBER", "3.0"));

			double expected = 2.4;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionMOD4)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "MOD");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "17.4"));
			formula->SetRightChild(new FormulaTree("NUMBER", "3.0"));

			double expected = 0.6;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCSIN1)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCSIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 0.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCSIN2)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCSIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 90.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCSIN3)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCSIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.5"));

			double expected = 30.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCSIN4)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCSIN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.4"));

			double expected = 23.57817848;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCSIN5)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCCOS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 0.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCCOS2)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCCOS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.5"));

			double expected = 30.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCCOS3)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCCOS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 90.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCCOS4)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCCOS");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "0.5"));

			double expected = 120.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCCOS5)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCCOS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.4"));

			double expected = 66.42182152;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCTAN1)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCTAN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 0.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCTAN2)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCTAN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 45.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionARCTAN3)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "ARCTAN");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.5"));

			double expected = 26.56505118;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionEXP1)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "EXP");
			formula->SetLeftChild(new FormulaTree("NUMBER", "0.0"));

			double expected = 1.0;
            double actual = interpreter->EvaluateFormula(formula, object);
            Assert::AreEqual(expected, actual);
		}

		TEST_METHOD(FunctionEXP2)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "EXP");
			formula->SetLeftChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 2.718281828;
            double actual = interpreter->EvaluateFormula(formula, object);
			Assert::IsTrue(TestHelper::isEqual((float)expected, (float)actual));
		}

		TEST_METHOD(FunctionEXP3)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "EXP");
			formula->SetLeftChild(new FormulaTree("NUMBER", "2.0"));

			double expected = 7.389056099;
            double actual = interpreter->EvaluateFormula(formula, object);
			Assert::IsTrue(TestHelper::isEqual((float)expected, (float)actual));
		}

		TEST_METHOD(FunctionEXP4)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

			FormulaTree *formula = new FormulaTree("FUNCTION", "EXP");
			formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
			formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));

			double expected = 0.367879441;
            double actual = interpreter->EvaluateFormula(formula, object);
			Assert::IsTrue(TestHelper::isEqual((float)expected, (float)actual));
		}

		TEST_METHOD(FunctionMAX1)
		{
			Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

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
            Object *object = new Object("TestObject");

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