#pragma once

#include <string>
#include "IUserVariable.h"

namespace ProjectStructure
{
	class UserVariable
	{
	public:
		UserVariable(Catrobat_Player::NativeComponent::IUserVariable^ userVariable);
		UserVariable(std::string name, std::string value);
		UserVariable(std::pair<std::string, std::string> variable);

		std::string GetName();
		std::string GetValue();
		void SetValue(std::string value);

	private:
		std::string m_name;
		std::string m_value;
	};

}