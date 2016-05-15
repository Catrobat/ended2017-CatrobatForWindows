#include "pch.h"
#include "BroadcastMessageDaemon.h"

using namespace Core;

BroadcastMessageDaemon *BroadcastMessageDaemon::__instance = NULL;

BroadcastMessageDaemon::BroadcastMessageDaemon()
{
	m_broadcastMessageSender = ref new BroadcastMessageSender();
}

BroadcastMessageDaemon *BroadcastMessageDaemon::Instance()
{
	if (!__instance)
	{
		__instance = new BroadcastMessageDaemon();
	}

	return __instance;
}

void BroadcastMessageDaemon::Register(BroadcastMessageListener ^listener)
{
	m_broadcastMessageSender->Broadcast += ref new BroadcastMessageEventHandler(listener, &BroadcastMessageListener::HandleBroadcastMessage);
}