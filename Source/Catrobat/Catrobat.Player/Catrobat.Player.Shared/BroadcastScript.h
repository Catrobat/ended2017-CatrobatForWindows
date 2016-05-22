#pragma once

#include "Script.h"
#include "BroadcastMessageListener.h"
#include "IBroadcastScript.h"

namespace Core
{
	ref class BroadcastMessageListener;
}
namespace ProjectStructure
{
	class BroadcastScript :
		public Script
	{
	public:
		BroadcastScript(Catrobat_Player::NativeComponent::IBroadcastScript^ script, Object* parent);
		~BroadcastScript();

		void EvaluateMessage(Platform::String ^message);

		std::string GetReceivedMessage();

	private:
		std::string m_receivedMessage;
		Core::BroadcastMessageListener^ m_broadcastMessageListener;
	};
}