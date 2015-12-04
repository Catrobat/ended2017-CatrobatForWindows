#pragma once

#include "Script.h"
#include "IWhenScript.h"

namespace ProjectStructure
{
	class WhenScript :
		public Script
	{
	public:
		enum Action
		{
			Tapped
		};

		WhenScript(Catrobat_Player::NativeComponent::IWhenScript^ whenScript, Object* parent);
		~WhenScript();

		int GetAction();

	private:
		int m_action;
	};
}