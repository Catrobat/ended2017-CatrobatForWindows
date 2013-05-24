#pragma once
class FormulaTree;
class Interpreter
{
public:
	Interpreter(void);
	~Interpreter(void);

	int EvaluateFormulaToInt(FormulaTree *tree);
	float EvaluateFormulaToFloat(FormulaTree *tree);
};

