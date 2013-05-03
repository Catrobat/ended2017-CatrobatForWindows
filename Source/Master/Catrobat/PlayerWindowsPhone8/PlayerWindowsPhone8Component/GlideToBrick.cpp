#include "pch.h"
#include "GlideToBrick.h"
#include "Script.h"
#include "Sprite.h"
#include <windows.h>
#include <ppltasks.h>

GlideToBrick::GlideToBrick(string spriteReference, float xDestination, float yDestination, float duration, Script *parent) :
	Brick(TypeOfBrick::PlaceAtBrick, spriteReference, parent),
	m_xDestination(xDestination), m_yDestination(yDestination),
	m_duration(duration)
{
}

void GlideToBrick::Execute()
{
	//float steps = m_duration / 20; // 50 Hz

	//float x_movement = m_xDestination / steps;
	//float y_movement = m_yDestination / steps;

	//for (int i = 0; i < steps; i++)
	//{
	//	float currentX, currentY;
	//	m_parent->Parent()->GetPosition(currentX, currentY);
	//	m_parent->Parent()->SetPosition(currentX + x_movement, currentY + y_movement);

	//	Concurrency::wait(20); // 50 Hz
	//}
}