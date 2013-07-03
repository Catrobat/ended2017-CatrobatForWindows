#include "pch.h"
#include "SetVariableBrick.h"

SetVariableBrick::SetVariableBrick(string objectReference, FormulaTree *variableFormula, Script *parent)
	: VariableManagementBrick(TypeOfBrick::SetVariableBrick, objectReference, variableFormula, parent)
{
}

void SetVariableBrick::Execute()
{
    // TODO: typecheck and logic
	m_variable->SetValue(m_variableFormula->Value());
}

