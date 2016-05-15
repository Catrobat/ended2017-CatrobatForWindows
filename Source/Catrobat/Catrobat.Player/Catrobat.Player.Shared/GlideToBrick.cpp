#include "pch.h"
#include "GlideToBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

#include <windows.h>
#include <ppltasks.h>

using namespace ProjectStructure;
using namespace std;

GlideToBrick::GlideToBrick(Catrobat_Player::NativeComponent::IGLideToBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::GlideToBrick, parent),
	m_xDestination(make_shared<FormulaTree>(brick->DestinationX)),
	m_yDestination(make_shared<FormulaTree>(brick->DestinationY)),
	m_duration(make_shared<FormulaTree>(brick->Duration))
{
}

void GlideToBrick::Execute()
{
	float steps = Interpreter::Instance()->EvaluateFormulaToFloat(m_duration, m_parent->GetParent()) / 0.02f; // 50 Hz

	float currentX, currentY;
	m_parent->GetParent()->GetTranslation(currentX, currentY);
	float x_movement = (currentX - Interpreter::Instance()->EvaluateFormulaToFloat(m_xDestination, m_parent->GetParent())) / steps;
	float y_movement = (currentY - Interpreter::Instance()->EvaluateFormulaToFloat(m_yDestination, m_parent->GetParent())) / steps;

	for (int i = 0; i < steps; i++)
	{
		m_parent->GetParent()->GetTranslation(currentX, currentY);
		m_parent->GetParent()->SetTranslation(currentX - x_movement, currentY - y_movement);
		Concurrency::wait(20); // 50 Hz
	}
}