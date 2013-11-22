#pragma once

#include <list>

#include "Object.h"

using namespace std;

class ObjectList
{
public:
	ObjectList();
	~ObjectList();

	void AddObject(Object *object);
	int GetSize();
	Object *GetObject(int index);
	Object *GetObject(string name);

private:
	list<Object*> *m_objects;
};

