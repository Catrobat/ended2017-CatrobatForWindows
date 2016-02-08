#include "pch.h"
#include "VariableManagementBrick.h"

using namespace std;
using namespace ProjectStructure;

VariableManagementBrick::VariableManagementBrick(TypeOfBrick brickType, Catrobat_Player::NativeComponent::IVariableManagementBrick^ brick, Script* parent) :
	Brick(brickType, parent),
	m_variableFormula(make_shared<FormulaTree>(brick->VariableFormula)),
	m_variable(make_shared<UserVariable>(brick->Variable))
{
}
