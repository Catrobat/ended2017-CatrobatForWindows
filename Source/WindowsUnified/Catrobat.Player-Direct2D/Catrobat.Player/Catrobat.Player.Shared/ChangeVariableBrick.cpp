#include "pch.h"
#include "ChangeVariableBrick.h"
#include "FormulaTree.h"
#include "Helper.h"

ChangeVariableBrick::ChangeVariableBrick(FormulaTree *variableFormula, Script *parent)
	: VariableManagementBrick(TypeOfBrick::SetVariableBrick, variableFormula, parent)
{
}

void ChangeVariableBrick::Execute()
{
    // TODO: typecheck and logic
    
    if (m_variableFormula->GetType() == Type::NUMBER)
    {
       double current;
       double to_add;

        // TODO check: negative m_variable allowed?
        // TODO check expected behaviour --> e.g. what if current is no number but to_add or vice-versa?
        if (Helper::ConvertStringToDouble(m_variable->GetValue(), &current) 
            && Helper::ConvertStringToDouble(m_variableFormula->Value(), &to_add)
            && ((current > 0 && to_add > 0 && DBL_MAX - current >= to_add)
                || (current < 0 && to_add < 0 && DBL_MIN - current <= to_add ) 
                || (current > 0 && to_add < 0) || (current < 0 && to_add > 0)))
        {
            m_variable->SetValue(std::to_string(current + to_add));
        }
    }
}     