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
            Assert::IsTrue(tree->getOperator() == Operator::NO_OPERATOR);
		}
		
		TEST_METHOD(FormulaTree_BasicOperators)
		{
			FormulaTree *tree = new FormulaTree("OPERATOR", "PLUS");
            tree->SetLeftChild(new FormulaTree("NUMBER", "1"));
            tree->SetRightChild(new FormulaTree("NUMBER", "2"));

            Assert::IsTrue(tree->GetType() == Type::OPERATOR);
            Assert::IsTrue(tree->getOperator() == Operator::PLUS);

            tree = new FormulaTree("OPERATOR", "MINUS");
            tree->SetLeftChild(new FormulaTree("NUMBER", "1"));
            tree->SetRightChild(new FormulaTree("NUMBER", "2"));

            Assert::IsTrue(tree->GetType() == Type::OPERATOR);
            Assert::IsTrue(tree->getOperator() == Operator::MINUS);

            tree = new FormulaTree("OPERATOR", "MULT");
            tree->SetLeftChild(new FormulaTree("NUMBER", "1"));
            tree->SetRightChild(new FormulaTree("NUMBER", "2"));

            Assert::IsTrue(tree->GetType() == Type::OPERATOR);
            Assert::IsTrue(tree->getOperator() == Operator::MULT);

            tree = new FormulaTree("OPERATOR", "DIVIDE");
            tree->SetLeftChild(new FormulaTree("NUMBER", "1"));
            tree->SetRightChild(new FormulaTree("NUMBER", "2"));

            Assert::IsTrue(tree->GetType() == Type::OPERATOR);
            Assert::IsTrue(tree->getOperator() == Operator::DIVIDE);

            tree = new FormulaTree("OPERATOR", "POW");
            tree->SetLeftChild(new FormulaTree("NUMBER", "1"));
            tree->SetRightChild(new FormulaTree("NUMBER", "2"));

            Assert::IsTrue(tree->GetType() == Type::OPERATOR);
            Assert::IsTrue(tree->getOperator() == Operator::POW);
		}

        TEST_METHOD(FormulaTree_Bracket)
        {
            FormulaTree *tree = new FormulaTree("BRACKET", "");
            tree->SetRightChild(new FormulaTree("OPERATOR", "PLUS"));
            tree->getRightChild()->SetLeftChild(new FormulaTree("NUMBER", "1"));
            tree->getRightChild()->SetRightChild(new FormulaTree("NUMBER", "2"));

            Assert::IsTrue(tree->GetType() == Type::BRACKET);
            Assert::IsTrue(tree->getOperator() == Operator::NO_OPERATOR);
            Assert::IsTrue(tree->getLeftChild() == NULL);
        }

	};
}