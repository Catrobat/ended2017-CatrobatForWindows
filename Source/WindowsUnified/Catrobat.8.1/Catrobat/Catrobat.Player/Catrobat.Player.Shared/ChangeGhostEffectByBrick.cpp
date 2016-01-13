#include "pch.h"
#include "ChangeGhostEffectByBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

using namespace ProjectStructure;
using namespace std;

ChangeGhostEffectByBrick::ChangeGhostEffectByBrick(Catrobat_Player::NativeComponent::IChangeGhostEffectByBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::ChangeGhostEffectByBrick, parent),
	m_transparency(make_shared<FormulaTree>(brick->Transparency))
{
}

void ChangeGhostEffectByBrick::Execute()
{
	m_parent->GetParent()->SetTransparency(m_parent->GetParent()->GetTransparency() + (Interpreter::Instance()->EvaluateFormulaToFloat(m_transparency.get(), GetParent()->GetParent()) / 100.f));
}