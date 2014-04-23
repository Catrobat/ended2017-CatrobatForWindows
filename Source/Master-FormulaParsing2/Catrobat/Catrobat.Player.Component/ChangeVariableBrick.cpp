#include "pch.h"
#include "ChangeVariableBrick.h"


ChangeVariableBrick::ChangeVariableBrick(FormulaTree *variableFormula, Script *parent)
	: VariableManagementBrick(TypeOfBrick::SetVariableBrick, variableFormula, parent)
{
}

void ChangeVariableBrick::Execute()
{
    // TODO: typecheck and logic
    m_variable->SetValue(m_variable->GetValue() + m_variableFormula->Value());
}