#include "pch.h"
#include "ObjectList.h"

using namespace std;

ObjectList::ObjectList()
{
	m_objects = new list<Object*>();
}

void ObjectList::AddObject(Object *object)
{
	m_objects->push_back(object);
}

int ObjectList::GetSize()
{
	return m_objects->size();
}

Object *ObjectList::GetObject(int index)
{
	list<Object*>::iterator it = m_objects->begin();
	advance(it, index);
	return *it;
}

Object *ObjectList::GetObject(string name)
{
	list<Object*>::iterator it = m_objects->begin();
	for (it = m_objects->begin(); it != m_objects->end(); it++)
	{
		if ((*it)->GetName() == name)
			return *it;
	}
    return NULL;
}