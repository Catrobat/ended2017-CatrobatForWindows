#include "pch.h"
#include "SetSizeToBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

using namespace ProjectStructure;
using namespace std;

SetSizeToBrick::SetSizeToBrick(Catrobat_Player::NativeComponent::ISetSizeToBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::SetGhostEffectBrick, parent),
	m_scale(make_shared<FormulaTree>(brick->Scale))
{
}

void SetSizeToBrick::Execute()
{
	float scale = Interpreter::Instance()->EvaluateFormulaToFloat(m_scale, m_parent->GetParent()) / 100;
	m_parent->GetParent()->SetScale(scale, scale);
}