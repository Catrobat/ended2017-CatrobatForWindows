#include "pch.h"
#include "CppUnitTest.h"

#include "FormulaTree.h"
#include "Object.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
	TEST_CLASS(FormulaTreeTests)
	{
	public:

        TEST_METHOD(FormulaTree_no_Operator)
		{
			FormulaTree *tree = new FormulaTree("NUMBER", "1");

            Assert::IsTrue(tree->GetType() == Type::NUMBER);
            Assert::IsTrue(tree->GetOperator() == Operator::NO_OPERATOR);
		}
		
		TEST_METHOD(FormulaTree_BasicOperators)
		{
			FormulaTree *tree = new FormulaTree("OPERATOR", "PLUS");
            tree->SetLeftChild(new FormulaTree("NUMBER", "1"));
            tree->SetRightChild(new FormulaTree("NUMBER", "2"));

            Assert::IsTrue(tree->GetType() == Type::OPERATOR);
            Assert::IsTrue(tree->GetOperator() == Operator::PLUS);

            tree = new FormulaTree("OPERATOR", "MINUS");
            tree->SetLeftChild(new FormulaTree("NUMBER", "1"));
            tree->SetRightChild(new FormulaTree("NUMBER", "2"));

            Assert::IsTrue(tree->GetType() == Type::OPERATOR);
            Assert::IsTrue(tree->GetOperator() == Operator::MINUS);

            tree = new FormulaTree("OPERATOR", "MULT");
            tree->SetLeftChild(new FormulaTree("NUMBER", "1"));
            tree->SetRightChild(new FormulaTree("NUMBER", "2"));

            Assert::IsTrue(tree->GetType() == Type::OPERATOR);
            Assert::IsTrue(tree->GetOperator() == Operator::MULT);

            tree = new FormulaTree("OPERATOR", "DIVIDE");
            tree->SetLeftChild(new FormulaTree("NUMBER", "1"));
            tree->SetRightChild(new FormulaTree("NUMBER", "2"));

            Assert::IsTrue(tree->GetType() == Type::OPERATOR);
            Assert::IsTrue(tree->GetOperator() == Operator::DIVIDE);

            tree = new FormulaTree("OPERATOR", "POW");
            tree->SetLeftChild(new FormulaTree("NUMBER", "1"));
            tree->SetRightChild(new FormulaTree("NUMBER", "2"));

            Assert::IsTrue(tree->GetType() == Type::OPERATOR);
            Assert::IsTrue(tree->GetOperator() == Operator::POW);
		}

        TEST_METHOD(FormulaTree_Bracket)
        {
            FormulaTree *tree = new FormulaTree("BRACKET", "");
            tree->SetRightChild(new FormulaTree("OPERATOR", "PLUS"));
            tree->GetRightChild()->SetLeftChild(new FormulaTree("NUMBER", "1"));
            tree->GetRightChild()->SetRightChild(new FormulaTree("NUMBER", "2"));

            Assert::IsTrue(tree->GetType() == Type::BRACKET);
            Assert::IsTrue(tree->GetOperator() == Operator::NO_OPERATOR);
            Assert::IsTrue(tree->GetLeftChild() == NULL);
        }

		TEST_METHOD(FormulaTree_Function)
		{
			FormulaTree *tree = new FormulaTree("FUNCTION", "TRUE");

			Assert::IsTrue(tree->GetType() == Type::FUNCTION);
			Assert::IsTrue(tree->GetFunction() == Function::L_TRUE);
			Assert::IsTrue(tree->GetLeftChild() == NULL);
			Assert::IsTrue(tree->GetRightChild() == NULL);

			tree = new FormulaTree("FUNCTION", "FALSE");

			Assert::IsTrue(tree->GetType() == Type::FUNCTION);
			Assert::IsTrue(tree->GetFunction() == Function::L_FALSE);
			Assert::IsTrue(tree->GetLeftChild() == NULL);
			Assert::IsTrue(tree->GetRightChild() == NULL);

			tree = new FormulaTree("NUMBER", "1");

			Assert::IsFalse(tree->GetType() == Type::FUNCTION);
			Assert::IsTrue(tree->GetFunction() == Function::NO_FUNCTION);
			Assert::IsTrue(tree->GetLeftChild() == NULL);
			Assert::IsTrue(tree->GetRightChild() == NULL);
		}

	};
}