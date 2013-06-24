#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

#include "Object.h"
#include "FormulaTree.h"
#include "Interpreter.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

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


		TEST_METHOD(Formula_Logic_AND)
		{
			FormulaTree *formula = new FormulaTree(OP, L_AND);
			formula->SetLeftChild(new FormulaTree(FUN,L_TRUE));
			formula->SetRightChild(new FormulaTree(OP, EQUAL));
			formula->getRightChild()->SetLeftChild(new FormulaTree(FUN, L_FALSE));
			formula->getRightChild()->SetRightChild(new FormulaTree(FUN, L_TRUE));

			Object *object = new Object("TestObject");
			Interpreter *interpreter = Interpreter::Instance();

			Assert::AreEqual(interpreter->EvaluateFormulaToInt(formula, object), TRUE);
		}


	};
}

