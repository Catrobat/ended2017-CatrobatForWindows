#include "pch.h"
#include "BroadcastScript.h"


BroadcastScript::BroadcastScript(string receivedMessage) : 
	Script(TypeOfScript::BroadcastScript), m_receivedMessage(receivedMessage)
{
}
