#pragma once
#include "brick.h"
class BroadcastBrick :
	public Brick
{
public:
	BroadcastBrick(string objectReference, std::string broadcastMessage, Script *parent);
	void Execute();
private:
	std::string m_broadcastMessage;
};
