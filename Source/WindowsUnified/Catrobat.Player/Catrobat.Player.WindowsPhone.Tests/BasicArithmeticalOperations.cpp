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
            formula->GetRightChild()->SetLeftChild(new FormulaTree("NUMBER", "3"));
            formula->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "7"));
			Object *object = new Object("TestObject");
			Interpreter *interpreter = Interpreter::Instance();

			int expected = 20;

			Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, object), expected);
		}

		TEST_METHOD(Formula_BasicArithmetical_Minus1_int)
		{
			int num1 = 10;
			int num2 = 7;

			FormulaTree *formula = new FormulaTree("OPERATOR", "MINUS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "10"));
			formula->SetRightChild(new FormulaTree("NUMBER", "7"));
			Object *object = new Object("TestObject");
			Interpreter *interpreter = Interpreter::Instance();

			int expected = num1 - num2;

			Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, object), expected);
		}

		TEST_METHOD(Formula_BasicArithmetical_Minus2_int)
		{
			int num1 = 7;
			int num2 = 10;

			FormulaTree *formula = new FormulaTree("OPERATOR", "MINUS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "7"));
			formula->SetRightChild(new FormulaTree("NUMBER", "10"));
			Object *object = new Object("TestObject");
			Interpreter *interpreter = Interpreter::Instance();

			int expected = num1 - num2;

			Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, object), expected);
		}

		TEST_METHOD(Formula_BasicArithmetical_Minus3_int)
		{
			int num1 = -7;
			int num2 = 10;

			FormulaTree *formula = new FormulaTree("OPERATOR", "MINUS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "-7"));
			formula->SetRightChild(new FormulaTree("NUMBER", "10"));
			Object *object = new Object("TestObject");
			Interpreter *interpreter = Interpreter::Instance();

			int expected = num1 - num2;

			Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, object), expected);
		}

        //5 + (10 - 6) * 2 = 13
        //Test without brackets, rules in Tree ??
        TEST_METHOD(Formula_BasicArithmetical_mixed_int)
		{
			FormulaTree *formula = new FormulaTree("OPERATOR", "PLUS");
			formula->SetLeftChild(new FormulaTree("NUMBER", "5"));
			formula->SetRightChild(new FormulaTree("OPERATOR", "MULT"));
            formula->GetRightChild()->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
            formula->GetRightChild()->GetLeftChild()->SetLeftChild(new FormulaTree("NUMBER", "10"));
            formula->GetRightChild()->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "6"));
            formula->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "2"));
			Object *object = new Object("TestObject");
			Interpreter *interpreter = Interpreter::Instance();

			int expected = 13;

			Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, object), expected);
		}

        TEST_METHOD(Formula_BasicArithmetical_NegativeNumber_int)
		{
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //-18
			FormulaTree *formula = new FormulaTree("OPERATOR", "MINUS");
			formula->SetRightChild(new FormulaTree("NUMBER", "18"));
			int expected = -18;
            int actual = interpreter->EvaluateFormulaToInt(formula, object);
            Assert::AreEqual(expected, actual);
		}

        TEST_METHOD(Formula_BasicArithmetical_NegativeNumberMixed_int)
		{
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //-2 * 12 / 4 = -6
			FormulaTree *formula = new FormulaTree("OPERATOR", "MULT");
            formula->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
            formula->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "2"));
			formula->SetRightChild(new FormulaTree("OPERATOR", "DIVIDE"));
            formula->GetRightChild()->SetLeftChild(new FormulaTree("NUMBER", "12"));
            formula->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "4"));
			int expected = -6;
            int actual = interpreter->EvaluateFormulaToInt(formula, object);
            Assert::AreEqual(expected, actual);
		}

        TEST_METHOD(Formula_Basic_PLUS_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //7.2 + 1.0 + 3.5 = 11.7
            FormulaTree *tree = new FormulaTree("OPERATOR", "PLUS");
            tree->SetLeftChild(new FormulaTree("NUMBER", "7.2"));
            tree->SetRightChild(new FormulaTree("OPERATOR", "PLUS"));
            tree->GetRightChild()->SetLeftChild(new FormulaTree("NUMBER", "1.0"));
            tree->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "3.5"));
            float expected = 11.7f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);
        }
		
        TEST_METHOD(Formula_Basic_PLUSwithMINUS_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //7.2 + 1.0 + 3.5 + -1.0 = 10.7
            FormulaTree *tree = new FormulaTree("OPERATOR", "PLUS");
            tree->SetLeftChild(new FormulaTree("OPERATOR", "PLUS"));
            tree->GetLeftChild()->SetLeftChild(new FormulaTree("OPERATOR", "PLUS"));
            tree->GetLeftChild()->GetLeftChild()->SetLeftChild(new FormulaTree("NUMBER", "7.2"));
            tree->GetLeftChild()->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));
            tree->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "3.5"));
            tree->SetRightChild(new FormulaTree("OPERATOR", "MINUS"));  
            tree->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));
            float expected = 10.7f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_MINUS_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //7.2 - 1.0 - 3.5 = 2.7   FAIL!!!
            //        MINUS
            //        /   \
            //      7,2  MINUS
            //           /   \
            //         1,0   3,5
            // --> 7,2 - (1,0 - 3,5) = 9,7
            FormulaTree *tree = new FormulaTree("OPERATOR", "MINUS");
            tree->SetLeftChild(new FormulaTree("NUMBER", "7.2"));
            tree->SetRightChild(new FormulaTree("OPERATOR", "MINUS"));
            tree->GetRightChild()->SetLeftChild(new FormulaTree("NUMBER", "1.0"));
            tree->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "3.5"));
            float expected = 9.7f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_MINUS2_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //7.2 - 1.0 - 3.5 = 2.7
            FormulaTree *tree = new FormulaTree("OPERATOR", "MINUS");
            tree->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
            tree->GetLeftChild()->SetLeftChild(new FormulaTree("NUMBER", "7.2"));
            tree->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));
            tree->SetRightChild(new FormulaTree("NUMBER", "3.5"));
            double expected = 2.7;
            double actual = interpreter->EvaluateFormula(tree, object);
            //Assert::AreEqual(expected, actual));
			Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_MINUSsimple_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //7.2 - 1.5 = 5.7
            FormulaTree *tree = new FormulaTree("OPERATOR", "MINUS");
            tree->SetLeftChild(new FormulaTree("NUMBER", "7.2"));
            tree->SetRightChild(new FormulaTree("NUMBER", "1.5"));
            float expected = 5.7f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_MINUSsimple2_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //7.2 - 10.5 = -3.3
            FormulaTree *tree = new FormulaTree("OPERATOR", "MINUS");
            tree->SetLeftChild(new FormulaTree("NUMBER", "7.2"));
            tree->SetRightChild(new FormulaTree("NUMBER", "10.5"));
            float expected = -3.3f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_MINUSwithMINUS_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //7.2 - 1.0 - 3.5 - -1.0= 3.7
            FormulaTree *tree = new FormulaTree("OPERATOR", "MINUS");
            tree->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
            tree->GetLeftChild()->SetLeftChild(new FormulaTree("OPERATOR", "MINUS"));
            tree->GetLeftChild()->GetLeftChild()->SetLeftChild(new FormulaTree("NUMBER", "7.2"));
            tree->GetLeftChild()->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));
            tree->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "3.5"));
            tree->SetRightChild(new FormulaTree("OPERATOR", "MINUS"));  
            tree->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));   
            float expected = 3.7f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_MULT_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //7.2 * 1.0 * 3.5 = 25.2
            FormulaTree *tree = new FormulaTree("OPERATOR", "MULT");
            tree->SetLeftChild(new FormulaTree("NUMBER", "7.2"));
            tree->SetRightChild(new FormulaTree("OPERATOR", "MULT"));
            tree->GetRightChild()->SetLeftChild(new FormulaTree("NUMBER", "1.0"));
            tree->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "3.5"));
            float expected = 25.2f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);   
        }

        TEST_METHOD(Formula_Basic_MULTsimple_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //7.2 * 3.4 = 24.48
            FormulaTree *tree = new FormulaTree("OPERATOR", "MULT");
            tree->SetLeftChild(new FormulaTree("NUMBER", "7.2"));
            tree->SetRightChild(new FormulaTree("NUMBER", "3.4"));
            float expected = 24.48f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);  
        }

        TEST_METHOD(Formula_Basic_MULTwithMINUS_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //7.2 * 1.0 * 3.5 * -1.0 = -25.2
            FormulaTree *tree = new FormulaTree("OPERATOR", "MULT");
            tree->SetLeftChild(new FormulaTree("OPERATOR", "MULT"));
            tree->GetLeftChild()->SetLeftChild(new FormulaTree("OPERATOR", "MULT"));
            tree->GetLeftChild()->GetLeftChild()->SetLeftChild(new FormulaTree("NUMBER", "7.2"));
            tree->GetLeftChild()->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));
            tree->GetLeftChild()->SetRightChild(new FormulaTree("NUMBER", "3.5"));
            tree->SetRightChild(new FormulaTree("OPERATOR", "MINUS"));  
            tree->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));   
            float expected = -25.2f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_DIVIDE_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //7.2 / 2.0 = 3.6
            FormulaTree *tree = new FormulaTree("OPERATOR", "DIVIDE");
            tree->SetLeftChild(new FormulaTree("NUMBER", "7.2"));
            tree->SetRightChild(new FormulaTree("NUMBER", "2.0"));
            float expected = 3.6f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            //Assert::AreEqual(expected, actual));
			Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_POW_int)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //2 ^ 3 = 8;
            FormulaTree *tree = new FormulaTree("OPERATOR", "POW");
            tree->SetLeftChild(new FormulaTree("NUMBER", "2"));
            tree->SetRightChild(new FormulaTree("NUMBER", "3"));
            int expected = 8;
            int actual = interpreter->EvaluateFormulaToInt(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_POW2_int)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //2 ^ 0 = 1;
            FormulaTree *tree = new FormulaTree("OPERATOR", "POW");
            tree->SetLeftChild(new FormulaTree("NUMBER", "2"));
            tree->SetRightChild(new FormulaTree("NUMBER", "0"));
            int expected = 1;
            int actual = interpreter->EvaluateFormulaToInt(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_POW3_int)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //0 ^ 3 = 8;
            FormulaTree *tree = new FormulaTree("OPERATOR", "POW");
            tree->SetLeftChild(new FormulaTree("NUMBER", "0"));
            tree->SetRightChild(new FormulaTree("NUMBER", "3"));
            int expected = 0;
            int actual = interpreter->EvaluateFormulaToInt(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_POW_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //2.1 ^ 3 = 9.261;
            FormulaTree *tree = new FormulaTree("OPERATOR", "POW");
            tree->SetLeftChild(new FormulaTree("NUMBER", "2.1"));
            tree->SetRightChild(new FormulaTree("NUMBER", "3.0"));
            float expected = 9.261f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_POW2_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //2.3 ^ 0 = 1;
            FormulaTree *tree = new FormulaTree("OPERATOR", "POW");
            tree->SetLeftChild(new FormulaTree("NUMBER", "2.3"));
            tree->SetRightChild(new FormulaTree("NUMBER", "0.0"));
            float expected = 1.0f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_POW3_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //0 ^ 3.4 = 8;
            FormulaTree *tree = new FormulaTree("OPERATOR", "POW");
            tree->SetLeftChild(new FormulaTree("NUMBER", "0.0"));
            tree->SetRightChild(new FormulaTree("NUMBER", "3.4"));
            float expected = 0.0f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_POW4_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //4.1 ^ 3.4 = 121.18953325391327628525330849374;
            FormulaTree *tree = new FormulaTree("OPERATOR", "POW");
            tree->SetLeftChild(new FormulaTree("NUMBER", "4.1"));
            tree->SetRightChild(new FormulaTree("NUMBER", "3.4"));
            float expected = 121.18953325391327628525330849374f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_BRACKET_int)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //2 * (3 - 1) = 4
            FormulaTree *tree = new FormulaTree("OPERATOR", "MULT");
            tree->SetLeftChild(new FormulaTree("NUMBER", "2"));
            tree->SetRightChild(new FormulaTree("BRACKET", ""));
            tree->GetRightChild()->SetRightChild(new FormulaTree("OPERATOR", "MINUS"));
            tree->GetRightChild()->GetRightChild()->SetLeftChild(new FormulaTree("NUMBER", "3"));
            tree->GetRightChild()->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "1"));
            int expected = 4;
            int actual = interpreter->EvaluateFormulaToInt(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_BRACKET_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //2.0 * (3.0 - 1.0) = 4.0
            FormulaTree *tree = new FormulaTree("OPERATOR", "MULT");
            tree->SetLeftChild(new FormulaTree("NUMBER", "2.0"));
            tree->SetRightChild(new FormulaTree("BRACKET", ""));
            tree->GetRightChild()->SetRightChild(new FormulaTree("OPERATOR", "MINUS"));
            tree->GetRightChild()->GetRightChild()->SetLeftChild(new FormulaTree("NUMBER", "3.0"));
            tree->GetRightChild()->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "1.0"));
            float expected = 4.0;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);
        }

        TEST_METHOD(Formula_Basic_MIXEDwithBracket_float)
        {
            Interpreter *interpreter = Interpreter::Instance();
            Object *object = new Object("TestObject");

            //10 + (7 - 3 * 2) = 11
            FormulaTree *tree = new FormulaTree("OPERATOR", "PLUS");
            tree->SetLeftChild(new FormulaTree("NUMBER", "10"));
            tree->SetRightChild(new FormulaTree("BRACKET", ""));
            tree->GetRightChild()->SetRightChild(new FormulaTree("OPERATOR", "MINUS"));
            tree->GetRightChild()->GetRightChild()->SetLeftChild(new FormulaTree("NUMBER", "7"));
            tree->GetRightChild()->GetRightChild()->SetRightChild(new FormulaTree("OPERATOR", "MULT"));
            tree->GetRightChild()->GetRightChild()->GetRightChild()->SetLeftChild(new FormulaTree("NUMBER", "3"));
            tree->GetRightChild()->GetRightChild()->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "2"));
            float expected = 11.0f;
            float actual = interpreter->EvaluateFormulaToFloat(tree, object);
            Assert::AreEqual(expected, actual);
        }
		
	};
}
