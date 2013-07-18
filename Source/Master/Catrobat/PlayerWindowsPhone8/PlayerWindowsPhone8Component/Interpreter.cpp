#include "pch.h"
#include "Interpreter.h"
#include "FormulaTree.h"
#include "ProjectDaemon.h"
#include <string.h>
#include <sstream>
#include <cmath>

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

double Interpreter::EvaluateFormula(FormulaTree *tree, Object *object)
{
	Type type = tree->GetType();
	switch (type)
	{
	case OPERATOR:
		return InterpretOperator(tree, object);
	case NUMBER:
		return atof(tree->Value().c_str());
	case USER_VARIABLE:
		{
			string varName = tree->Value();
			UserVariable *var = object->GetVariable(varName);
			if (var)
				return atof(var->GetValue().c_str());
            var = ProjectDaemon::Instance()->GetProject()->GetVariable(varName);
			if (var)
				return atof(var->GetValue().c_str());

            // TODO: Check logic here (What should we do when variable is not found)
            return 0;
		}
		break;
    case BRACKET:
        return this->EvaluateFormula(tree->GetRightChild(), object);
    case FUNCTION:
		return InterpretFunction(tree, object);
	default:
		break;
	}
    // TODO: What should we do when we get a invalid tree here?
	throw "Exception in Interpreter.cpp: No such type available";
}

int Interpreter::EvaluateFormulaToInt(FormulaTree *tree, Object *object)
{
	return (int) (this->EvaluateFormula(tree, object));
}

float Interpreter::EvaluateFormulaToFloat(FormulaTree *tree, Object *object)
{
	return (float) (this->EvaluateFormula(tree, object));
}

bool Interpreter::EvaluateFormulaToBool(FormulaTree *tree, Object *object)
{
	double result = this->EvaluateFormula(tree, object);

	if (result != 0)
		return true;
	else
		return false;
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

double Interpreter::InterpretOperator(FormulaTree *tree, Object *object)
{
    FormulaTree *leftChild = tree->GetLeftChild();
    double leftValue = 0.0;
    if (tree->GetLeftChild() != NULL)
        leftValue = this->EvaluateFormula(leftChild, object);
    FormulaTree *rightChild = tree->GetRightChild();
    double rightValue = this->EvaluateFormula(rightChild, object);

    double returnValue = 0.0;

    switch (tree->GetOperator())
    {
    case Operator::PLUS:
        returnValue = leftValue + rightValue;
        break;
    case Operator::MINUS:
        returnValue = leftValue - rightValue;
        break;
    case Operator::MULT:
        returnValue = leftValue * rightValue;
        break;
    case Operator::DIVIDE:
        if (rightValue == 0)
            return 0.0f;
        returnValue = leftValue / rightValue;
        break;
    case Operator::POW:
        returnValue = pow(leftValue, rightValue);
		break;
	case Operator::LOGICAL_AND:
		returnValue = leftValue && rightValue;
		break;
	case Operator::LOGICAL_OR:
		returnValue = leftValue || rightValue;
		break;
	case Operator::EQUAL:
		returnValue = leftValue == rightValue;
		break;
	case Operator::NOT_EQUAL:
		returnValue = leftValue != rightValue;
		break;
    default:
        break;
    }

    return returnValue;
}

double Interpreter::InterpretFunction(FormulaTree *tree, Object *object)
{
	double returnValue = 0.0;
	FormulaTree *leftChild = tree->GetLeftChild();
	FormulaTree *rightChild = tree->GetRightChild();

	double leftValue = 0.0;
	if (leftChild != NULL)
		leftValue = this->EvaluateFormula(leftChild, object);
	double rightValue = 0.0;
	if (rightChild != NULL)
		rightValue = this->EvaluateFormula(rightChild, object);

	switch (tree->GetFunction())
	{
	case Function::L_TRUE:
		returnValue = 1.0;
		break;
	case Function::L_FALSE: 
		returnValue = 0.0;
		break;
	case Function::SIN:
		returnValue = sin(leftValue);
		break;
	case Function::COS: 
		returnValue = cos(leftValue);
		break;
	case Function::TAN: 
		returnValue = tan(leftValue);
		break;
	case Function::LN: 
	case Function::LOG:
		returnValue = log(leftValue);
		break;
	case Function::SQRT:
		returnValue = sqrt(leftValue);
		break;
	case Function::RAND: 
		break;
	case Function::ABS: 
		returnValue = abs(leftValue);
		break;
	case Function::ROUND: 
		break;
	case Function::PI: 
		break;
	case Function::MOD: 
		break;
	case Function::ARCSIN: 
		break;
	case Function::ARCCOS: 
		break;
	case Function::ARCTAN: 
		break;
	case Function::EXP: 
		break;
	case Function::MAX: 
		break;
	case Function::MIN: 
		break;
	default:
		break;
	}

	return returnValue;
}
