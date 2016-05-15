#pragma once
#include "BroadcastMessageSender.h"
#include "BroadcastScript.h"

// TODO: What namespace should we use here?
namespace ProjectStructure
{
	class BroadcastScript;
}
namespace Core
{
	ref class BroadcastMessageListener sealed
	{
	public:
		BroadcastMessageListener();
		void HandleBroadcastMessage(BroadcastMessageSender^ mc, Platform::String^ msg);
		void SetScript(int script);

	private:
		BroadcastMessageSender ^m_broadcastMessageSender;
		ProjectStructure::BroadcastScript *m_script;
	};
}