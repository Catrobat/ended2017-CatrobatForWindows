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
}

void FormulaTree::SetLeftChild(FormulaTree *leftChild)
{
	m_leftChild = leftChild;
}

void FormulaTree::SetRightChild(FormulaTree *rightChild)
{
	m_rightChild = rightChild;
}

FormulaTree *FormulaTree::getLeftChild()
{
    if (this->m_leftChild != NULL)
        return this->m_leftChild;
    else
        return NULL;
}

FormulaTree *FormulaTree::getRightChild()
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