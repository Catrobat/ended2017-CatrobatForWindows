#pragma once
#include <string>
#include <map>

enum Type
{
	OPERATOR,
	NUMBER,
	USER_VARIABLE, 
    BRACKET, 
	FUNCTION
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

enum Function
{
	L_TRUE, 
	L_FALSE, 

	NO_FUNCTION
};

class FormulaTree
{
public:
	FormulaTree(std::string type, std::string value);

	void SetLeftChild(FormulaTree *leftChild);
	void SetRightChild(FormulaTree *rightChild);
	Type GetType();
    FormulaTree *GetLeftChild();
    FormulaTree *GetRightChild();
	std::string Value();

    //returns Operator if m_type is set to OPERATOR, instead NO_OPERATOR
    Operator GetOperator();

	//returns Function if m_type is set to FUNCTION, instead NO_FUNCTION
	Function GetFunction();

private:
	FormulaTree *m_rightChild;
	FormulaTree *m_leftChild;
	Type m_type;
	std::string m_value;
    Operator m_operator;
	Function m_function;
};

