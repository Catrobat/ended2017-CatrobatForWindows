#include "pch.h"
#include "FormulaTree.h"
#include "Helper.h"

using namespace std;

FormulaTree::FormulaTree(Catrobat_Player::NativeComponent::IFormulaTree^ formulaTree) :
	m_value(Helper::StdString(formulaTree->VariableValue))
{
	if (formulaTree->LeftChild)
	{
		m_leftChild = make_shared<FormulaTree>(formulaTree->LeftChild);
	}
	if (formulaTree->RightChild)
	{
		m_rightChild = make_shared<FormulaTree>(formulaTree->RightChild);
	}

	if (formulaTree->VariableType == "NUMBER")
		m_type = Type::NUMBER;
	else if (formulaTree->VariableType == "OPERATOR")
		m_type = Type::OPERATOR;
	else if (formulaTree->VariableType == "USER_VARIABLE")
		m_type = Type::USER_VARIABLE;
	else if (formulaTree->VariableType == "BRACKET")
		m_type = Type::BRACKET;
	else if (formulaTree->VariableType == "FUNCTION")
		m_type = Type::FUNCTION;
	else if (formulaTree->VariableType == "SENSOR")
		m_type = Type::SENSOR;

	if (m_type == Type::OPERATOR)
	{
		if (m_value == "PLUS")
			m_operator = Operator::PLUS;
		else if (m_value == "MINUS")
			m_operator = Operator::MINUS;
		else if (m_value == "MULT")
			m_operator = Operator::MULT;
		else if (m_value == "DIVIDE")
			m_operator = Operator::DIVIDE;
		else if (m_value == "POW")
			m_operator = Operator::POW;
		else if (m_value == "EQUAL")
			m_operator = Operator::EQUAL;
		else if (m_value == "NOT_EQUAL")
			m_operator = Operator::NOT_EQUAL;
		else if (m_value == "GREATER_THAN")
			m_operator = Operator::GREATER_THAN;
		else if (m_value == "SMALLER_THAN")
			m_operator = Operator::SMALLER_THAN;
		else if (m_value == "SMALLER_OR_EQUAL")
			m_operator = Operator::SMALLER_OR_EQUAL;
		else if (m_value == "LOGICAL_AND")
			m_operator = Operator::LOGICAL_AND;
		else if (m_value == "LOGICAL_OR")
			m_operator = Operator::LOGICAL_OR;
		else if (m_value == "LOGICAL_NOT")
			m_operator = Operator::LOGICAL_NOT;
	}
	else
	{
		m_operator = Operator::NO_OPERATOR;
	}

	if (m_type == Type::FUNCTION)
	{
		if (m_value == "TRUE")
			m_function = Function::L_TRUE;
		else if (m_value == "FALSE")
			m_function = Function::L_FALSE;
		else if (m_value == "SIN")
			m_function = Function::SIN;
		else if (m_value == "COS")
			m_function = Function::COS;
		else if (m_value == "TAN")
			m_function = Function::TAN;
		else if (m_value == "LN")
			m_function = Function::LN;
		else if (m_value == "LOG")
			m_function = Function::LOG;
		else if (m_value == "SQRT")
			m_function = Function::SQRT;
		else if (m_value == "RAND")
			m_function = Function::RAND;
		else if (m_value == "ABS")
			m_function = Function::ABS;
		else if (m_value == "ROUND")
			m_function = Function::ROUND;
		else if (m_value == "PI")
			m_function = Function::PI;
		else if (m_value == "MOD")
			m_function = Function::MOD;
		else if (m_value == "ARCSIN")
			m_function = Function::ARCSIN;
		else if (m_value == "ARCCOS")
			m_function = Function::ARCCOS;
		else if (m_value == "ARCTAN")
			m_function = Function::ARCTAN;
		else if (m_value == "EXP")
			m_function = Function::EXP;
		else if (m_value == "MAX")
			m_function = Function::MAX;
		else if (m_value == "MIN")
			m_function = Function::MIN;
	}
	else
	{
		m_function = Function::NO_FUNCTION;
	}

	if (m_type == Type::SENSOR)
	{
		if (m_value == "COMPASS_DIRECTION")
			m_sensor = Sensor::COMPASS_DIRECTION;
		else if (m_value == "COMPASSDIRECTION") //has to be removed; workaround for testing [Bug: 407]
			m_sensor = Sensor::COMPASS_DIRECTION; //--
		else if (m_value == "X_ACCELERATION")
			m_sensor = Sensor::X_ACCELERATION;
		else if (m_value == "Y_ACCELERATION")
			m_sensor = Sensor::Y_ACCELERATION;
		else if (m_value == "Z_ACCELERATION")
			m_sensor = Sensor::Z_ACCELERATION;
		else if (m_value == "X_INCLINATION")
			m_sensor = Sensor::X_INCLINATION;
		else if (m_value == "Y_INCLINATION")
			m_sensor = Sensor::Y_INCLINATION;
		else if (m_value == "INCLINATION_X") //has to be removed; workaround for testing [Bug: 409]
			m_sensor = Sensor::X_INCLINATION; //--
		else if (m_value == "INCLINATION_Y") //has to be removed; workaround for testing [Bug: 409]
			m_sensor = Sensor::Y_INCLINATION; //--
		else if (m_value == "LOUDNESS")
			m_sensor = Sensor::LOUDNESS;
		else if (m_value == "ACCELERATION_X") //has to be removed; workaround for testing [Bug: 408]
			m_sensor = Sensor::X_ACCELERATION; //--
		else if (m_value == "ACCELERATION_Y") //has to be removed; workaround for testing [Bug: 408]
			m_sensor = Sensor::Y_ACCELERATION; //--
		else if (m_value == "ACCELERATION_Z") //has to be removed; workaround for testing [Bug: 408]
			m_sensor = Sensor::Z_ACCELERATION; //--
	}
	else
	{
		m_sensor = Sensor::NO_SENSOR;
	}
}

FormulaTree *FormulaTree::GetLeftChild()
{
	return this->m_leftChild.get();
}

FormulaTree *FormulaTree::GetRightChild()
{
	return this->m_rightChild.get();
}

Type FormulaTree::GetType()
{
	return m_type;
}

string FormulaTree::Value()
{
	return m_value;
}

Operator FormulaTree::GetOperator()
{
	return this->m_operator;
}

Function FormulaTree::GetFunction()
{
	return this->m_function;
}

Sensor FormulaTree::GetSensor()
{
	return this->m_sensor;
}

