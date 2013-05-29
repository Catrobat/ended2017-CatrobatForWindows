#include "pch.h"
#include "SetVariableBrick.h"


SetVariableBrick::SetVariableBrick(string objectReference, FormulaTree *variableFormula, Script *parent)
	: VariableManagementBrick(TypeOfBrick::SetVariableBrick, objectReference, variableFormula, parent)
{
}

void SetVariableBrick::Execute()
{
	Type test = m_variableFormula->GetType();
	m_variable->SetValue(m_variableFormula->Value());
}

