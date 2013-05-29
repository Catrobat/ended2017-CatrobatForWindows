#include "pch.h"
#include "Interpreter.h"
#include "FormulaTree.h"
#include "ProjectDaemon.h"

using namespace std;
using namespace Windows::Devices::Sensors;

Interpreter *Interpreter::__instance = NULL;

Interpreter *Interpreter::Instance()
{
	if (!__instance)
		__instance = new Interpreter();
	return __instance;
}

Interpreter::Interpreter()
{
	m_accelerometer = Windows::Devices::Sensors::Accelerometer::GetDefault();
}


int Interpreter::EvaluateFormulaToInt(FormulaTree *tree, Object *object)
{
	Type type = tree->GetType();
	switch (type)
	{
	case OPERATOR:
		break;
	case NUMBER:
		break;
	case USER_VARIABLE:
		{
			string varName = tree->Value();
			UserVariable *var = object->Variable(varName);
			if (!var)
				var = ProjectDaemon::Instance()->getProject()->Variable(varName);
			if (var)
				return atoi(var->Value().c_str());
		}
		break;
	default:
		break;
	}
	return 1;
}

float Interpreter::EvaluateFormulaToFloat(FormulaTree *tree, Object *object)
{
	Type type = tree->GetType();
	switch (type)
	{
	case OPERATOR:
		break;
	case NUMBER:
		break;
	case USER_VARIABLE:
		{
			string varName = tree->Value();
			UserVariable *var = object->Variable(varName);
			if (!var)
				var = ProjectDaemon::Instance()->getProject()->Variable(varName);
			if (var)
				return atof(var->Value().c_str());
		}
		break;
	default:
		break;
	}
	return 1.0f;
}

bool Interpreter::EvaluateFormulaToBool(FormulaTree *tree, Object *object)
{
	return true;
}

void Interpreter::ReadAcceleration()
{
	// Reading Accelerometer Data
	if (m_accelerometer != nullptr)
    {
		try
		{
			m_accReading = m_accelerometer->GetCurrentReading();
			Platform::String ^acceleration = L"Acceleration: " + "X: " + m_accReading->AccelerationX + " Y: " + m_accReading->AccelerationY + " Z: " + m_accReading->AccelerationZ;
		}
		catch(Platform::Exception^ e)
		{
			// there is a bug tracking this issue already
			// we need to remove this try\catch once the bug # 158858 hits our branch
			// For now, to make this App work, catching the exception
			// The reverting is tracked by WP8 # 159660
		}
	}	
}