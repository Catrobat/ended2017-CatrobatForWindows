#include "pch.h"
#include "Interpreter.h"
#include "FormulaTree.h"
#include "ProjectDaemon.h"
#include "CompassProvider.h"
#include "string"
#include <sstream>
#include <random>
#include <cmath>
#include <ctime>
#include <math.h>

using namespace std;
using namespace Windows::Devices::Sensors;

Interpreter *Interpreter::__instance = nullptr;

Interpreter *Interpreter::Instance()
{
    if (!__instance)
        __instance = new Interpreter();
    return __instance;
}

Interpreter::Interpreter()
{
	m_accelerometerProvider = new AccelerometerProvider();
    m_compassProvider = new CompassProvider();
	m_inclinationProvider = ref new InclinationProvider();
	m_loudnessProvider = ref new LoudnessCapture();
}

Interpreter::~Interpreter()
{
	m_loudnessProvider->StopCapture();
	delete m_accelerometerProvider;
	delete m_compassProvider;
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
    case SENSOR:
		return InterpretSensor(tree, object);
    default:
        break;
    }
    // TODO: What should we do when we get a invalid tree here?
    throw "Exception in Interpreter.cpp: No such type available";
}

int Interpreter::EvaluateFormulaToInt(FormulaTree *tree, Object *object)
{
    return static_cast<int>(this->EvaluateFormula(tree, object));
}

float Interpreter::EvaluateFormulaToFloat(FormulaTree *tree, Object *object)
{
    return static_cast<float>(this->EvaluateFormula(tree, object));
}

bool Interpreter::EvaluateFormulaToBool(FormulaTree *tree, Object *object)
{
    double result = this->EvaluateFormula(tree, object);

    if (result != 0)
        return true;
    else
        return false;
}

float Interpreter::ReadCompass()
{
    return m_compassProvider->GetDirection();
}

float Interpreter::ReadInclination(Inclination inclinationType)
{
	if (inclinationType == Inclination::Pitch)
	{
		return m_inclinationProvider->GetPitch();
	}
	else if (inclinationType == Inclination::Roll)
	{
		return m_inclinationProvider->GetRoll();
	}
	else
	{
		return m_inclinationProvider->GetYaw();
	}
}

double Interpreter::InterpretOperator(FormulaTree *tree, Object *object)
{
    FormulaTree *leftChild = tree->GetLeftChild();
    auto leftValue = 0.0;
    if (tree->GetLeftChild() != NULL)
        leftValue = this->EvaluateFormula(leftChild, object);
    FormulaTree *rightChild = tree->GetRightChild();
    double rightValue = this->EvaluateFormula(rightChild, object);

    auto returnValue = 0.0;

	switch (tree->GetOperator())
	{
	case Operator::PLUS:
		if (this->TestChilds(tree, Childs::LeftAndRightChild))
			returnValue = leftValue + rightValue;
		break;
	case Operator::MINUS:
		if (this->TestChilds(tree, Childs::LeftAndRightChild) ||
			this->TestChilds(tree, Childs::RightChild))  
			returnValue = leftValue - rightValue;
		break;
	case Operator::MULT:
		if (this->TestChilds(tree, Childs::LeftAndRightChild))
			returnValue = leftValue * rightValue;
		break;
	case Operator::DIVIDE:
		if (this->TestChilds(tree, Childs::LeftAndRightChild))
		{
			if (rightValue == 0)
				return 0.0f;
			returnValue = leftValue / rightValue;
		}
		break;
	case Operator::POW:
		if (this->TestChilds(tree, Childs::LeftAndRightChild))
			returnValue = pow(leftValue, rightValue);
		break;
	case Operator::LOGICAL_AND:
		if (this->TestChilds(tree, Childs::LeftAndRightChild))
			returnValue = leftValue && rightValue;
		break;
	case Operator::LOGICAL_OR:
		if (this->TestChilds(tree, Childs::LeftAndRightChild))
			returnValue = leftValue || rightValue;
		break;
	case Operator::EQUAL:
		if (this->TestChilds(tree, Childs::LeftAndRightChild))
			returnValue = leftValue == rightValue;
		break;
	case Operator::NOT_EQUAL:
		if (this->TestChilds(tree, Childs::LeftAndRightChild))
			returnValue = leftValue != rightValue;
		break;
	case Operator::GREATER_THAN:
		if (this->TestChilds(tree, Childs::LeftAndRightChild))
			if (leftValue > rightValue)
				returnValue = 1.0;
			else
				returnValue = 0.0;
		break;
	case Operator::GREATER_OR_EQUAL:
		if (this->TestChilds(tree, Childs::LeftAndRightChild))
			if (leftValue >= rightValue)
				returnValue = 1.0;
			else
				returnValue = 0.0;
		break;
	case Operator::SMALLER_THAN:
		if (this->TestChilds(tree, Childs::LeftAndRightChild))
			if (leftValue < rightValue)
				returnValue = 1.0;
			else
				returnValue = 0.0;
		break;
	case Operator::SMALLER_OR_EQUAL:
		if (this->TestChilds(tree, Childs::LeftAndRightChild))
			if (leftValue <= rightValue)
				returnValue = 1.0;
			else
				returnValue = 0.0;
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
    auto pi = 4.0 * std::atan(1.0);

    auto leftValue = 0.0;
    if (leftChild != NULL)
        leftValue = this->EvaluateFormula(leftChild, object);
    auto rightValue = 0.0;
    if (rightChild != NULL)
        rightValue = this->EvaluateFormula(rightChild, object);

    switch (tree->GetFunction())
    {
    case Function::L_TRUE:
        if (this->TestChilds(tree, Childs::NoChild))
            returnValue = 1.0;
        break;
    case Function::L_FALSE: 
        if (this->TestChilds(tree, Childs::NoChild))
            returnValue = 0.0;
        break;
    case Function::SIN:
        if (this->TestChilds(tree, Childs::LeftChild))
            returnValue = sin(leftValue * pi / 180.0);
        break;
    case Function::COS: 
        if (this->TestChilds(tree, Childs::LeftChild))
            returnValue = this->CalculateCosinus(leftValue);
        break;
    case Function::TAN: 
        if (this->TestChilds(tree, Childs::LeftChild))
            returnValue = tan(leftValue * pi / 180.0);
        break;
    case Function::LN: 
        if (this->TestChilds(tree, Childs::LeftChild))
        {
            if (leftValue <= 0)
                returnValue = -1.0; //TODO: exception!
            else
                returnValue = log(leftValue);
        }
        break;
    case Function::LOG:
        if (this->TestChilds(tree, Childs::LeftChild))
        {
            if (leftValue <= 0)
                returnValue = -1.0; //TODO: exception!
            else
                returnValue = log10(leftValue);
        }
        break;
    case Function::SQRT:
        if (this->TestChilds(tree, Childs::LeftChild))
            returnValue = sqrt(leftValue);
        break;
    case Function::RAND:
        if (this->TestChilds(tree, Childs::LeftAndRightChild))
            returnValue = this->CalculateRand(leftValue, rightValue);
        break;
    case Function::ABS: 
        if (this->TestChilds(tree, Childs::LeftChild))
            returnValue = abs(leftValue);
        break;
    case Function::ROUND: 
        if (this->TestChilds(tree, Childs::LeftChild))
            returnValue = this->RoundDoubleToInt(leftValue);
        break;
    case Function::PI: 
        if (this->TestChilds(tree, Childs::NoChild))
            returnValue = 4.0 * std::atan(1.0);
        break;
    case Function::MOD: 
        if (this->TestChilds(tree, Childs::LeftAndRightChild))
            returnValue = this->CalculateModulo(leftValue, rightValue);
        break;
    case Function::ARCSIN: 
        if (this->TestChilds(tree, Childs::LeftChild))
            returnValue = asin(leftValue) * 180 / pi;
        break;
    case Function::ARCCOS: 
        if (this->TestChilds(tree, Childs::LeftChild))
            returnValue = acos(leftValue) * 180 / pi;
        break;
    case Function::ARCTAN: 
        if (this->TestChilds(tree, Childs::LeftChild))
            returnValue = atan(leftValue) * 180 / pi;
        break;
    case Function::EXP: 
		if (this->TestChilds(tree, Childs::LeftChild))
			returnValue = exp(leftValue);
        break;
    case Function::MAX: 
        if (this->TestChilds(tree, Childs::LeftAndRightChild))
            returnValue = this->CalculateMax(leftValue, rightValue);
        break;
    case Function::MIN: 
        if (this->TestChilds(tree, Childs::LeftAndRightChild))
            returnValue = this->CalculateMin(leftValue, rightValue);
        break;
    default:
        break;
    }

    return returnValue;
}

double Interpreter::InterpretSensor(FormulaTree *tree, Object *object)
{
	double returnValue = 0;

	switch (tree->GetSensor())
	{
	case Sensor::COMPASS_DIRECTION:
		returnValue = static_cast<double>(this->ReadCompass());
		break;
	case Sensor::X_INCLINATION:
		returnValue = static_cast<double>(m_inclinationProvider->GetRoll());
		break;
	case Sensor::Y_INCLINATION:
		returnValue = static_cast<double>(m_inclinationProvider->GetPitch());
		break;
	case Sensor::X_ACCELERATION:
		returnValue = m_accelerometerProvider->GetX();
		break;
	case Sensor::Y_ACCELERATION:
		returnValue = m_accelerometerProvider->GetY();
		break;
	case Sensor::Z_ACCELERATION:
		returnValue = m_accelerometerProvider->GetZ();
		break;
	case Sensor::LOUDNESS:
		m_loudnessProvider->StartCapture();
		returnValue = m_loudnessProvider->GetLoudness();
		break;
	default:
		returnValue = 0;
		break;
	}

	return returnValue;
}

bool Interpreter::TestChilds(FormulaTree *tree, Childs childs) 
{
    auto returnValue = false;

    switch (childs)
    {
    case LeftChild:
        if (tree->GetLeftChild() != NULL && tree->GetRightChild() == NULL)
            returnValue = true;
        break;
    case RightChild:
        if (tree->GetLeftChild() == NULL && tree->GetRightChild() != NULL)
            returnValue = true;
        break;
    case LeftAndRightChild:
        if (tree->GetLeftChild() != NULL && tree->GetRightChild() != NULL)
            returnValue = true;
        break;
    case NoChild:
        if (tree->GetLeftChild() == NULL && tree->GetRightChild() == NULL)
            returnValue = true;
        break;
    default:
        returnValue = false;
        break;
    }

    return returnValue;
}

double Interpreter::CalculateMax(double value1, double value2)
{
    if (value1 < value2)
        return value2;
    else
        return value1;
}

double Interpreter::CalculateMin(double value1, double value2)
{
    if (value1 < value2)
        return value1;
    else 
        return value2;
}

double Interpreter::CalculateRand(double value1, double value2)
{
    double min = this->CalculateMin(value1, value2);
    double max = this->CalculateMax(value1, value2);

	std::random_device rd;
	std::mt19937 gen(rd());
	if (OnlyIntegerValues(value1, value2))
	{
		std::uniform_int_distribution<> dis(min, max);
		return dis(gen);
	}
	else
	{
		std::uniform_real_distribution<> dis(min, max);
		return dis(gen);
	}
}

//compatibility method (pocket code for android)
bool Interpreter::OnlyIntegerValues(double value1, double value2)
{
	int int1 = abs(static_cast<int>(value1));
	int int2 = abs(static_cast<int>(value2));

	value1 -= int1;
	value2 -= int2;

	if (value1 > 0.0 || value2 > 0.0)
	{
		return false;
	}

	return true;
}

double Interpreter::CalculateModulo(double dividend, double divisor)
{
	int integerQuotient = static_cast<int>(dividend / divisor);
    if ((dividend < 0 || divisor < 0) && !(dividend < 0 && divisor < 0) && fmod(dividend, divisor) != 0)
        integerQuotient -= 1;
	double returnValue = dividend - static_cast<double>(divisor * integerQuotient);

    return returnValue; 
}

double Interpreter::CalculateCosinus(double degree)
{
    auto pi = 4.0 * std::atan(1.0);

    if (this->CalculateModulo(degree + 90.0, 180) == 0.0)
        return 0.0;
    else
        return cos(degree * pi / 180.0);
}

double Interpreter::RoundDoubleToInt(double value)
{
	auto roundedNumber = 0.0;

    if(value >= 0)
		roundedNumber = static_cast<int>(value + 0.5);
    else
		roundedNumber = static_cast<int>(value - 0.5);
    return roundedNumber;
}
