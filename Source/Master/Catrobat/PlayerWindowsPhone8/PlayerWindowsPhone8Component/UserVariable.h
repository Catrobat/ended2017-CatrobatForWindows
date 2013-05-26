#pragma once

#include <string>

class UserVariable
{
public:
	UserVariable(std::string name, std::string value);
	UserVariable(std::pair<std::string, std::string> variable);

	std::string Value(std::string name);

private:
	std::string m_name;
	std::string m_value;
};

