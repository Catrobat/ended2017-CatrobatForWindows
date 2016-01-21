#include "pch.h"
#include "SetGhostEffectBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

using namespace ProjectStructure;
using namespace std;

SetGhostEffectBrick::SetGhostEffectBrick(Catrobat_Player::NativeComponent::ISetGhostEffectBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::SetGhostEffectBrick, parent),
	m_transparency(make_shared<FormulaTree>(brick->Transparency))
{
}

void SetGhostEffectBrick::Execute()
{
	m_parent->GetParent()->SetTransparency((Interpreter::Instance()->EvaluateFormulaToFloat(m_transparency.get(), GetParent()->GetParent()) / 100.0f));
}