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
	m_programVariableList = new map<string, string>();
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

void Interpreter::addProgrammVariable(string name, string value)
{
	m_programVariableList->insert(pair<string, string>(name, value));
}

string Interpreter::ProgrammVariable(string name)
{
	map<string, string>::iterator searchItem = m_programVariableList->find(name);
	return searchItem->second;
}