#include "pch.h"
#include "FormulaTree.h"

using namespace std;

FormulaTree::FormulaTree(string type, string value)
	: m_value(value)
{
	if (type == "NUMBER")
		m_type = Type::NUMBER;
	else if (type == "OPERATOR")
		m_type = Type::OPERATOR;
	else if (type == "USER_VARIABLE")
		m_type = Type::USER_VARIABLE;
    else if (type == "BRACKET")
        m_type = Type::BRACKET;
	else if (type == "FUNCTION")
		m_type = Type::FUNCTION;

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
}

void FormulaTree::SetLeftChild(FormulaTree *leftChild)
{
	m_leftChild = leftChild;
}

void FormulaTree::SetRightChild(FormulaTree *rightChild)
{
	m_rightChild = rightChild;
}

FormulaTree *FormulaTree::GetLeftChild()
{
    if (this->m_leftChild != NULL)
        return this->m_leftChild;
    else
        return NULL;
}

FormulaTree *FormulaTree::GetRightChild()
{
    if (this->m_rightChild != NULL)
        return this->m_rightChild;
    else
        return NULL;
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
