#include "pch.h"
#include "BroadcastBrick.h"
#include "BroadcastMessageDaemon.h"
#include "Helper.h"

using namespace ProjectStructure;

BroadcastBrick::BroadcastBrick(Catrobat_Player::NativeComponent::IBroadcastBrick^ brick, Script* parent)
	: Brick(TypeOfBrick::BroadcastBrick, parent), m_broadcastMessage(Helper::StdString(brick->BroadcastMessage))
{
}

void BroadcastBrick::Execute()
{
	std::wstring wideString = std::wstring(m_broadcastMessage.begin(), m_broadcastMessage.end());
	const wchar_t* wideString_char = wideString.c_str();
	Platform::String^ message = ref new Platform::String(wideString_char);
	Core::BroadcastMessageDaemon::Instance()->m_broadcastMessageSender->SendBroadcastMessage(message);
}
