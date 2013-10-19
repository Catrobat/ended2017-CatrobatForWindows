#include "pch.h"
#include "BroadcastMessageSender.h"

BroadcastMessageSender::BroadcastMessageSender()
{
}

void BroadcastMessageSender::SendBroadcastMessage(Platform::String ^message)
{
	Broadcast(this, message);
}