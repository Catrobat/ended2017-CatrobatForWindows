#include "pch.h"
#include "BroadcastBrick.h"
#include "BroadcastMessageDaemon.h"

BroadcastBrick::BroadcastBrick(std::string broadcastMessage, Script *parent)
	: Brick(TypeOfBrick::BroadcastBrick,parent), m_broadcastMessage(broadcastMessage)
{
}

void BroadcastBrick::Execute()
{
	std::wstring wideString = std::wstring(m_broadcastMessage.begin(), m_broadcastMessage.end());
	const wchar_t* wideString_char = wideString.c_str();
	Platform::String^ message = ref new Platform::String(wideString_char);
	BroadcastMessageDaemon::Instance()->m_broadcastMessageSender->SendBroadcastMessage(message);
}
