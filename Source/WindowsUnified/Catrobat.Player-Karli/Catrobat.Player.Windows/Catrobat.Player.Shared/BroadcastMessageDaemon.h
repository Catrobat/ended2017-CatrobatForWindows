#pragma once
#include "BroadcastMessageSender.h"
#include "BroadcastMessageListener.h"

class BroadcastMessageDaemon
{
public:
	static BroadcastMessageDaemon *Instance();
	void Register(BroadcastMessageListener ^listener);

private:
	BroadcastMessageDaemon();
	
	static BroadcastMessageDaemon *__instance;

public:
	BroadcastMessageSender ^m_broadcastMessageSender;
};

