#include "pch.h"
#include "UserVariable.h"

using namespace std;

UserVariable::UserVariable(string name, string value)
	: m_name(name), m_value(value)
{
}

UserVariable::UserVariable(pair<string, string> variable)
	: m_name(variable.first), m_value(variable.second)
{
}

string UserVariable::Value(string name)
{
	return m_value;
}
