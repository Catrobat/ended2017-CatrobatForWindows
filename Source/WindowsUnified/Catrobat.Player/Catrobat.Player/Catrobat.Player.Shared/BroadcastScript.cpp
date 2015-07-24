#include "pch.h"
#include "BroadcastScript.h"
#include "BroadcastMessageDaemon.h"

using namespace std;

BroadcastScript::BroadcastScript(std::string receivedMessage, shared_ptr<Object> parent) :
Script(TypeOfScript::BroadcastScript, parent), m_receivedMessage(receivedMessage)
{
    m_broadcastMessageListener = ref new BroadcastMessageListener();
    m_broadcastMessageListener->SetScript((int)(&(*this)));
    BroadcastMessageDaemon::Instance()->Register(m_broadcastMessageListener);
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
