#include "pch.h"
#include "BroadcastBrick.h"


BroadcastBrick::BroadcastBrick(string objectReference, std::string broadcastMessage, Script *parent)
	: Brick(TypeOfBrick::BroadcastBrick, objectReference, parent), m_broadcastMessage(broadcastMessage)
{
}

void BroadcastBrick::Execute()
{

}