#pragma once

#include <list>

#include "Object.h"

class ObjectList
{
public:
	ObjectList();
	~ObjectList();

	void AddObject(Object *object);

    // Getters
	int GetSize();
	Object *GetObject(int index);
	Object *GetObject(std::string name);
    std::list<Object*>* GetObjects() { return m_objects; }

private:
	std::list<Object*>* m_objects;
};

