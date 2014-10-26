#pragma once

#include "Brick.h"
#include "UserVariable.h"
#include "FormulaTree.h"

class VariableManagementBrick :
	public Brick
{
public:
	VariableManagementBrick(TypeOfBrick brickType, FormulaTree *variableFormula, std::shared_ptr<Script> parent);
	virtual void Execute() = 0;

	void SetVariable(UserVariable *variable);

protected:
	UserVariable *m_variable;
	FormulaTree *m_variableFormula;
};
