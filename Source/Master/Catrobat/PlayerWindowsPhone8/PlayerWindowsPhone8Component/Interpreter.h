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

	void addProgrammVariable(std::string name, std::string value);
	std::string ProgrammVariable(std::string name);


private:
	std::map<std::string, std::string> *m_programVariableList;
};

