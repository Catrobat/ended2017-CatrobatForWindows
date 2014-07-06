#pragma once
#include "BroadcastMessageSender.h"

class BroadcastScript;

ref class BroadcastMessageListener sealed
{
public:
	BroadcastMessageListener();
	void HandleBroadcastMessage(BroadcastMessageSender^ mc, Platform::String^ msg);
	void SetScript(int script);

private:
	BroadcastMessageSender ^m_broadcastMessageSender;
	BroadcastScript *m_script;
};

