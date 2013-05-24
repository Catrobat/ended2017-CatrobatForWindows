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

FormulaTree::Type FormulaTree::GetType()
{
	return m_type;
}