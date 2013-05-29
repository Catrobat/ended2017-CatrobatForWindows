#include "pch.h"
#include "Interpreter.h"
#include "FormulaTree.h"
#include "ProjectDaemon.h"

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

int Interpreter::EvaluateFormulaToInt(FormulaTree *tree, Object *object)
{
	Type test = tree->GetType();
	if (test == Type::USER_VARIABLE)
	{
		string varName = tree->Value();
		UserVariable *var = object->Variable(varName);
		if (!var)
			var = ProjectDaemon::Instance()->getProject()->Variable(varName);
		if (var)
			return atoi(var->Value().c_str());
	}
	return 1;
}

float Interpreter::EvaluateFormulaToFloat(FormulaTree *tree, Object *object)
{
	Type test = tree->GetType();
	if (test == Type::USER_VARIABLE)
	{
		string varName = tree->Value();
		UserVariable *var = object->Variable(varName);
		if (!var)
			var = ProjectDaemon::Instance()->getProject()->Variable(varName);
		if (var)
			return atof(var->Value().c_str());
	}
	return 1.0f;
}

bool Interpreter::EvaluateFormulaToBool(FormulaTree *tree, Object *object)
{
	return true;
}