#include "pch.h"
#include "UserVariable.h"

using namespace std;


UserVariable::UserVariable(Catrobat_Player::NativeComponent::IUserVariable^ userVariable)
{
}

UserVariable::UserVariable(string name, string value)
	: m_name(name), m_value(value)
{
}

UserVariable::UserVariable(pair<string, string> variable)
	: m_name(variable.first), m_value(variable.second)
{
}

string UserVariable::GetName()
{
	return m_name;
}

string UserVariable::GetValue()
{
	return m_value;
}

void UserVariable::SetValue(string value)
{
	m_value = value;
}
