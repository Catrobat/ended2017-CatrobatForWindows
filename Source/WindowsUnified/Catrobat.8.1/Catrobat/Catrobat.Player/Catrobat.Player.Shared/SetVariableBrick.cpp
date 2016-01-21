#include "pch.h"
#include "SetVariableBrick.h"

using namespace ProjectStructure;

SetVariableBrick::SetVariableBrick(Catrobat_Player::NativeComponent::ISetVariableBrick^ brick, Script* parent)
	: VariableManagementBrick(TypeOfBrick::SetVariableBrick, brick, parent)
{
}

void SetVariableBrick::Execute()
{
	// TODO: typecheck and logic
	m_variable->SetValue(m_variableFormula->Value());
}

