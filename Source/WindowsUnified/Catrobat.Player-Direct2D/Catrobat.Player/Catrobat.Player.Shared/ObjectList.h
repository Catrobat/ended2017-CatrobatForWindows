#pragma once

#include <list>

#include "Object.h"

class ObjectList
{
public:
	ObjectList();
	~ObjectList();

	void AddObject(Object *object);
	int GetSize();
	Object *GetObject(int index);
	Object *GetObject(std::string name);

private:
	std::list<Object*> *m_objects;
};

