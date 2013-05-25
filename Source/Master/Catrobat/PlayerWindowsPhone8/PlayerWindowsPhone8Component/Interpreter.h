#pragma once

#include <map>

class FormulaTree;

class Interpreter
{
private:
	static Interpreter *__instance;

public:
	Interpreter(void);
	~Interpreter(void);
	static Interpreter *Instance();
	
	int EvaluateFormulaToInt(FormulaTree *tree);
	float EvaluateFormulaToFloat(FormulaTree *tree);
	bool EvaluateFormulaToBool(FormulaTree *tree);

};

