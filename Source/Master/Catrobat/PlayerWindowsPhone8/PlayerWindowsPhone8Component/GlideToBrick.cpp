#include "pch.h"
#include "GlideToBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"
#include <windows.h>
#include <ppltasks.h>

GlideToBrick::GlideToBrick(string spriteReference, FormulaTree *xDestination, FormulaTree *yDestination, FormulaTree *duration, Script *parent) :
	Brick(TypeOfBrick::GlideToBrick, spriteReference, parent),
	m_xDestination(xDestination), m_yDestination(yDestination),
	m_duration(duration)
{
}

void GlideToBrick::Execute()
{
	float steps = Interpreter::Instance()->EvaluateFormulaToFloat(m_duration, m_parent->Parent()) / 20; // 50 Hz

	float x_movement = Interpreter::Instance()->EvaluateFormulaToFloat(m_xDestination, m_parent->Parent()) / steps;
	float y_movement = Interpreter::Instance()->EvaluateFormulaToFloat(m_yDestination, m_parent->Parent()) / steps;

	for (int i = 0; i < steps; i++)
	{
		float currentX, currentY;
		m_parent->Parent()->GetPosition(currentX, currentY);
		m_parent->Parent()->SetPosition(currentX + x_movement, currentY + y_movement);

		Concurrency::wait(20); // 50 Hz
	}
}