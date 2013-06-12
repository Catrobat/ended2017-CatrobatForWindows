#pragma once
#include <string>
#include <map>

enum Type
{
	OPERATOR,
	NUMBER,
	USER_VARIABLE
};

class FormulaTree
{
public:
	FormulaTree(std::string type, std::string value);

	void SetLeftChild(FormulaTree *leftChild);
	void SetRightChild(FormulaTree *rightChild);
	Type GetType();
    FormulaTree *getLeftChild();
    FormulaTree *getRightChild();
	std::string Value();

private:
	FormulaTree *m_rightChild;
	FormulaTree *m_leftChild;
	Type m_type;
	std::string m_value;
};

