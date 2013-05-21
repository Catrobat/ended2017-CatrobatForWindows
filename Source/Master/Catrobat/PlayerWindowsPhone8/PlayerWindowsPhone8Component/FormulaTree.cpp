#include "pch.h"
#include "FormulaTree.h"

using namespace std;

FormulaTree::FormulaTree(string type, string value)
	: m_type(type), m_value(value)
{
}

void FormulaTree::SetLeftChild(FormulaTree *leftChild)
{
	m_leftChild = leftChild;
}

void FormulaTree::SetRightChild(FormulaTree *rightChild)
{
	m_rightChild = rightChild;
}