#pragma once

#include <string>

using namespace std;

class Brick
{
public:
	Brick(string type);
	~Brick();

private:
	string m_type;
};

