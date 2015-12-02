#include "pch.h"
#include "BroadcastMessageSender.h"

using namespace Core;

BroadcastMessageSender::BroadcastMessageSender() {}

void BroadcastMessageSender::SendBroadcastMessage(Platform::String ^message)
{
	Broadcast(this, message);
}