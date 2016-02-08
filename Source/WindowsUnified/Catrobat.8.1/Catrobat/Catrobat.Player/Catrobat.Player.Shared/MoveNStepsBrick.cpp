#include "pch.h"
#include "MoveNStepsBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"
#include <cmath>
#include <windows.h>
#include <ppltasks.h>

using namespace ProjectStructure;
using namespace std;

MoveNStepsBrick::MoveNStepsBrick(Catrobat_Player::NativeComponent::IMoveNStepsBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::MoveNStepsBrick, parent),
	m_steps(make_shared<FormulaTree>(brick->Steps))
{
}


MoveNStepsBrick::~MoveNStepsBrick()
{
}

void MoveNStepsBrick::Execute()
{
	auto x(0.0f);
	auto y(0.0f);
	m_parent->GetParent()->GetTranslation(x, y);
	CalculateNewCoordinates(x, y);
	m_parent->GetParent()->SetTranslation(x, y);
}

void MoveNStepsBrick::CalculateNewCoordinates(float &x, float &y)
{
	auto steps = Interpreter::Instance()->EvaluateFormula(m_steps.get(), m_parent->GetParent());

	if (!steps)
	{
		return;
	}

	auto rotation = static_cast<int>(m_parent->GetParent()->GetRotation()) % 360;

	//case of negative rotation
	rotation = rotation < 0 ? 360 - rotation : rotation;

	//Law of sines
	auto gamma = 90;
	auto beta = static_cast<int>(rotation) % 90;
	auto alpha = abs(gamma - beta);

	auto c = 1.0f;
	auto a = c / gamma * alpha;
	auto b = c / gamma * beta;

	//check quadrant
	if (rotation <= 90) // first quadrant
	{
		x += static_cast<float>(a * steps);
		y += static_cast<float>(b * steps);
	}
	else if (rotation <= 180) //second quadrant
	{
		x -= static_cast<float>(b * steps);
		y += static_cast<float>(a * steps);
	}
	else if (rotation <= 270) //third...
	{
		x -= static_cast<float>(a * steps);
		y -= static_cast<float>(b * steps);
	}
	else
	{
		x += static_cast<float>(b * steps);
		y -= static_cast<float>(a * steps);
	}
}

