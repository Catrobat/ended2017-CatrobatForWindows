#include "pch.h"
#include "TestHelper.h"

bool TestHelper::isEqual(float x, float y)
{
	if (x <= y + EPSILON && x >= y - EPSILON)
		return true;
	return false;
}
