#pragma once
#include "script.h"
#include "BroadcastMessageListener.h"

class BroadcastScript :
	public Script
{
public:
	BroadcastScript(string receivedMessage, string spriteReference, Object *parent);
	~BroadcastScript();

	void EvaluateMessage(Platform::String ^message);

	string ReceivedMessage();

private:
	string m_receivedMessage;
	BroadcastMessageListener ^m_broadcastMessageListener;
};
