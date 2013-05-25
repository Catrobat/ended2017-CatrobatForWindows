#include "pch.h"
#include "Interpreter.h"
#include "FormulaTree.h"

using namespace std;

Interpreter *Interpreter::__instance = NULL;

Interpreter *Interpreter::Instance()
{
	if (!__instance)
		__instance = new Interpreter();
	return __instance;
}

Interpreter::Interpreter(void)
{
}

Interpreter::~Interpreter(void)
{
}

int Interpreter::EvaluateFormulaToInt(FormulaTree *tree)
{
	return 1;
}

float Interpreter::EvaluateFormulaToFloat(FormulaTree *tree)
{
	return 1.0f;
}

bool Interpreter::EvaluateFormulaToBool(FormulaTree *tree)
{
	return true;
}