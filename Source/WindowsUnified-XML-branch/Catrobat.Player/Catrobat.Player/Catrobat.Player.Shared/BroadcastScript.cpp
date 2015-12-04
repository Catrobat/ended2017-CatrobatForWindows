#include "pch.h"
#include "BroadcastScript.h"
#include "BroadcastMessageDaemon.h"
#include "Helper.h"

using namespace std;
using namespace ProjectStructure;

BroadcastScript::BroadcastScript(Catrobat_Player::NativeComponent::IBroadcastScript^ script, Object* parent) :
	Script(TypeOfScript::BroadcastScript, parent, script),
	m_receivedMessage(Helper::StdString(script->ReceivedMessage))
{
	m_broadcastMessageListener = ref new Core::BroadcastMessageListener();
	m_broadcastMessageListener->SetScript((int)(&(*this)));
	Core::BroadcastMessageDaemon::Instance()->Register(m_broadcastMessageListener);
}

BroadcastScript::~BroadcastScript()
{
}

std::string BroadcastScript::GetReceivedMessage()
{
	return m_receivedMessage;
}

void BroadcastScript::EvaluateMessage(Platform::String ^message)
{
	std::wstring wideString = std::wstring(m_receivedMessage.begin(), m_receivedMessage.end());
	const wchar_t* wideString_char = wideString.c_str();
	Platform::String^ receivedMessage = ref new Platform::String(wideString_char);

	if (message->Equals(receivedMessage))
	{
		Execute();
	}
}
