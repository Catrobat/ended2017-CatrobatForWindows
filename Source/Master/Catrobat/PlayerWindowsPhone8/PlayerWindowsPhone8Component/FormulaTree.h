#pragma once
#include <string>
#include <map>

enum Type
{
	OPERATOR,
	NUMBER,
	USER_VARIABLE
};

enum Operator
{
    //basic arithmetical
    PLUS, 
    MINUS, 
    MULT, 
    DIVIDE, 
    POW, 

    //logic
    EQUAL, 
    NOT_EQUAL, 
    GREATER_THAN, 
    GREATER_OR_EQUAL, 
    SMALLER_THAN, 
    SMALLER_OR_EQUAL, 
    LOGICAL_AND, 
    LOGICAL_OR, 
    LOGICAL_NOT, 

    NO_OPERATOR
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

    //returns Operator if m_type is set to OPERATOR, NO_OPERATOR
    Operator getOperator();

private:
	FormulaTree *m_rightChild;
	FormulaTree *m_leftChild;
	Type m_type;
	std::string m_value;
    Operator m_operator;
};

