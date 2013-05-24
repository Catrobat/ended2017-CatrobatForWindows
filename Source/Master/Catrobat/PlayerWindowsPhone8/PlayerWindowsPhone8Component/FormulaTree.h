#pragma once
#include <string>
#include <map>

class FormulaTree
{
enum Type
{
	OPERATOR,
	NUMBER,
	USER_VARIABLE
};

public:
	FormulaTree(std::string type, std::string value);

	void SetLeftChild(FormulaTree *leftChild);
	void SetRightChild(FormulaTree *rightChild);
	Type GetType();

private:
	FormulaTree *m_rightChild;
	FormulaTree *m_leftChild;
	Type m_type;
	std::string m_value;
};

