#include "pch.h"
#include "BroadcastMessageListener.h"
#include "BroadcastScript.h"

using namespace Core;

BroadcastMessageListener::BroadcastMessageListener()
{
}

void BroadcastMessageListener::HandleBroadcastMessage(BroadcastMessageSender^ messageSender, Platform::String ^message)
{
	m_script->EvaluateMessage(message);
}

void BroadcastMessageListener::SetScript(int script)
{
	m_script = (ProjectStructure::BroadcastScript *) script;
}