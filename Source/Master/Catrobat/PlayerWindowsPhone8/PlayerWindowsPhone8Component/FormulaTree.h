#pragma once
#include <string>

class FormulaTree
{
public:
	FormulaTree(std::string type, std::string value);

	void SetLeftChild(FormulaTree *leftChild);
	void SetRightChild(FormulaTree *rightChild);

private:
	FormulaTree *m_rightChild;
	FormulaTree *m_leftChild;
	std::string m_type;
	std::string m_value;
};

