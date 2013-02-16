#include "pch.h"
#include "BroadcastScript.h"


BroadcastScript::BroadcastScript(string receivedMessage, string spriteReference) : 
	Script(TypeOfScript::BroadcastScript, spriteReference), m_receivedMessage(receivedMessage)
{
}
