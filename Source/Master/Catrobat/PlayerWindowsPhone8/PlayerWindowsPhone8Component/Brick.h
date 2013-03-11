#pragma once

#include <string>

using namespace std;

class Brick
{
public:
	enum TypeOfBrick
	{
		CostumeBrick,
		WaitBrick,
		PlaceAtBrick,
		SetGhostEffectBrick,
		PlaySoundBrick
	};

	Brick(TypeOfBrick brickType, string spriteReference);
	
	TypeOfBrick BrickType();

private:
	TypeOfBrick m_brickType;
	string m_spriteReference;
};

