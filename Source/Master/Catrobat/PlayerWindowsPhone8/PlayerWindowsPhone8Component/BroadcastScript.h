#pragma once
#include "script.h"
class BroadcastScript :
	public Script
{
public:
	BroadcastScript(string receivedMessage, string spriteReference, Object *parent);
	~BroadcastScript();

	string ReceivedMessage();

private:
	string m_receivedMessage;
};
