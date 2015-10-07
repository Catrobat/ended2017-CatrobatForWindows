#pragma once

#include "Script.h"
#include "BroadcastMessageListener.h"

class BroadcastScript :
	public Script
{
public:
	BroadcastScript(std::string receivedMessage, std::shared_ptr<Object> parent);
	~BroadcastScript();

	void EvaluateMessage(Platform::String ^message);

	std::string GetReceivedMessage();

private:
	std::string m_receivedMessage;
	BroadcastMessageListener ^m_broadcastMessageListener;
};
