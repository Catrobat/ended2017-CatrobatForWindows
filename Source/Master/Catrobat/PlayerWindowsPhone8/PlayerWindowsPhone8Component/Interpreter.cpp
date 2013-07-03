#include "pch.h"
#include "Interpreter.h"
#include "FormulaTree.h"
#include "ProjectDaemon.h"
#include <string.h>
#include <sstream>

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
        return interpretOperator(tree, object);
	case NUMBER:
		return atoi(tree->Value().c_str());
	case USER_VARIABLE:
		{
			string varName = tree->Value();
			UserVariable *var = object->GetVariable(varName);
			if (var)
				return atoi(var->GetValue().c_str());
            var = ProjectDaemon::Instance()->GetProject()->GetVariable(varName);
			if (var)
				return atoi(var->GetValue().c_str());

            // TODO: Check logic here (What should we do when variable is not found)
            return 0;
		}
		break;
    case BRACKET:
        return this->EvaluateFormulaToInt(tree->GetRightChild(), object);
	default:
		break;
	}

    // TODO: What should we do when we get a invalid tree here?
	return 1;
}

float Interpreter::EvaluateFormulaToFloat(FormulaTree *tree, Object *object)
{
	Type type = tree->GetType();
	switch (type)
	{
	case OPERATOR:
        return interpretOperatorFloat(tree, object);
	case NUMBER:
        return (float)atof(tree->Value().c_str());
	case USER_VARIABLE:
		{
			string varName = tree->Value();
			UserVariable *var = object->GetVariable(varName);
			if (var)
				return (float)atof(var->GetValue().c_str());
            var = ProjectDaemon::Instance()->GetProject()->GetVariable(varName);
            if (var)
                return (float)atof(var->GetValue().c_str());

            // TODO: Check logic here (What should we do when variable is not found)
            return 0.0f;
		}
		break;
    case BRACKET:
        return this->EvaluateFormulaToFloat(tree->GetRightChild(), object);
	default:
		break;
	}

    // TODO: What should we do when we get a invalid tree here?
	return 1.0f;
}

bool Interpreter::EvaluateFormulaToBool(FormulaTree *tree, Object *object)
{
	Type type = tree->GetType();
	switch (type)
	{
	case OPERATOR:
		return interpretOperatorBool(tree, object);
	case NUMBER:
		return atof(tree->Value().c_str()) > 0.0f;
	case FUNCTION:
		return interpretFunctionBool(tree, object);
	default:
		break;
	}

	// TODO: What should we do when we get a invalid tree here?
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

int Interpreter::interpretOperator(FormulaTree *tree, Object *object)
{
    FormulaTree *leftChild = tree->GetLeftChild();
    int leftValue = 0;
    if (tree->GetLeftChild() != NULL)
        leftValue = this->EvaluateFormulaToInt(leftChild, object);
    FormulaTree *rightChild = tree->GetRightChild();
    int rightValue = this->EvaluateFormulaToInt(rightChild, object);
    int returnValue = -1;

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
            return -1;
        returnValue = leftValue / rightValue;
        break;
    case Operator::POW:
        returnValue = (int) pow(leftValue, rightValue);
    default:
        break;
    }
    
    return returnValue;
}

float Interpreter::interpretOperatorFloat(FormulaTree *tree, Object *object)
{
    FormulaTree *leftChild = tree->GetLeftChild();
    float leftValue = 0.0f;
    if (tree->GetLeftChild() != NULL)
        leftValue = this->EvaluateFormulaToFloat(leftChild, object);
    FormulaTree *rightChild = tree->GetRightChild();
    float rightValue = this->EvaluateFormulaToFloat(rightChild, object);

    float returnValue = 0.0f;

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
    default:
        break;
    }

    return (float)returnValue;
}

bool Interpreter::interpretOperatorBool(FormulaTree *tree, Object *object)
{
	FormulaTree *leftChild = tree->GetLeftChild();
    bool leftValue = false;
    if (tree->GetLeftChild() != NULL)
        leftValue = this->EvaluateFormulaToBool(leftChild, object);
    FormulaTree *rightChild = tree->GetRightChild();
    bool rightValue = false;
	if (tree->GetRightChild() != NULL)
		rightValue = this->EvaluateFormulaToBool(rightChild, object);

	bool returnValue = false;

	switch (tree->GetOperator())
	{
	case Operator::LOGICAL_AND:
		returnValue = leftValue && rightValue;
		break;
	default: 
		returnValue = interpretOperatorFloat(tree, object) > 0.0f;
		break;
	}

	return returnValue;
}

bool Interpreter::interpretFunctionBool(FormulaTree *tree, Object *object)
{
	FormulaTree *leftChild = tree->GetLeftChild();
    bool leftValue = false;
    if (tree->GetLeftChild() != NULL)
        leftValue = this->EvaluateFormulaToBool(leftChild, object);
    FormulaTree *rightChild = tree->GetRightChild();
    bool rightValue = false;
	if (tree->GetRightChild() != NULL)
		rightValue = this->EvaluateFormulaToBool(rightChild, object);

	bool returnValue = false;

	switch (tree->GetFunction())
	{
	case Function::L_TRUE:
		returnValue = true;
		break;
	case Function::L_FALSE:
		returnValue = false;
		break;
	default:
		break;
	}

	return returnValue;
}
