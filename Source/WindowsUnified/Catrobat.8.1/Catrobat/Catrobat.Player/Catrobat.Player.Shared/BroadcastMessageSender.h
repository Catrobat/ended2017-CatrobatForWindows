#pragma once

namespace Core
{
	ref class BroadcastMessageSender;
	delegate void BroadcastMessageEventHandler(BroadcastMessageSender^ sender, Platform::String ^message);

	ref class BroadcastMessageSender sealed
	{
	public:
		BroadcastMessageSender();

		event BroadcastMessageEventHandler^ Broadcast;

		void SendBroadcastMessage(Platform::String ^message);
	};
}