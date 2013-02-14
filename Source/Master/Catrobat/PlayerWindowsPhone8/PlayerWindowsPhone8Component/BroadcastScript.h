#pragma once
#include "script.h"
class BroadcastScript :
	public Script
{
public:
	BroadcastScript(string receivedMessage);
	~BroadcastScript();

private:
	string m_receivedMessage;
};

