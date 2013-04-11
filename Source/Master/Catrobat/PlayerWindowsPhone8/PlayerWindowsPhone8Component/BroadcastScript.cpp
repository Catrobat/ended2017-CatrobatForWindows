#include "pch.h"
#include "BroadcastScript.h"

BroadcastScript::BroadcastScript(string receivedMessage, string spriteReference, Sprite *parent) :
	Script(TypeOfScript::BroadcastScript, spriteReference, parent), m_receivedMessage(receivedMessage)
{
}

string BroadcastScript::ReceivedMessage()
{
	return m_receivedMessage;
}

void BroadcastScript::Execute()
{

}