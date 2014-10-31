#pragma once

#include "Brick.h"

class BroadcastBrick :
	public Brick
{
public:
	BroadcastBrick(std::string broadcastMessage, std::shared_ptr<Script> parent);
	void Execute();

private:
	std::string m_broadcastMessage;
};
