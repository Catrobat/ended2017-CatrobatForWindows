#pragma once
#include "brick.h"

class BroadcastBrick :
	public Brick
{
public:
	BroadcastBrick(std::string broadcastMessage, Script *parent);
	void Execute();

private:
	std::string m_broadcastMessage;
};
