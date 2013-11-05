#include "pch.h"
#include "BroadcastScript.h"
#include "BroadcastMessageDaemon.h"

BroadcastScript::BroadcastScript(string receivedMessage, Object *parent) :
Script(TypeOfScript::BroadcastScript, parent), m_receivedMessage(receivedMessage)
{
    m_broadcastMessageListener = ref new BroadcastMessageListener();
    m_broadcastMessageListener->SetScript((int)(&(*this)));
    BroadcastMessageDaemon::Instance()->Register(m_broadcastMessageListener);
}

string BroadcastScript::GetReceivedMessage()
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
