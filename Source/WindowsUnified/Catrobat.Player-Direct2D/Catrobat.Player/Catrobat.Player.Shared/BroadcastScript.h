#pragma once

#include "Script.h"
#include "BroadcastMessageListener.h"

class BroadcastScript :
	public Script
{
public:
	BroadcastScript(string receivedMessage, Object *parent);
	~BroadcastScript();

	void EvaluateMessage(Platform::String ^message);

	string GetReceivedMessage();

private:
	string m_receivedMessage;
	BroadcastMessageListener ^m_broadcastMessageListener;
};
