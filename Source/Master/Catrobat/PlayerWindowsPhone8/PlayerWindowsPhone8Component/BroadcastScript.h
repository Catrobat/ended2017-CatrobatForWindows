#pragma once
#include "script.h"
class BroadcastScript :
	public Script
{
public:
	BroadcastScript(string receivedMessage, string spriteReference, Sprite *parent);
	~BroadcastScript();

	void Execute();

	string ReceivedMessage();

private:
	string m_receivedMessage;
};
