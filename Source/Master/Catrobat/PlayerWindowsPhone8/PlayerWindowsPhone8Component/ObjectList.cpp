#include "pch.h"
#include "ObjectList.h"

ObjectList::ObjectList()
{
	m_objects = new list<Object*>();
}

void ObjectList::addObject(Object *object)
{
	m_objects->push_back(object);
}

int ObjectList::Size()
{
	return m_objects->size();
}

Object *ObjectList::getObject(int index)
{
	list<Object*>::iterator it = m_objects->begin();
	advance(it, index);
	return *it;
}

Object *ObjectList::getObject(string name)
{
	list<Object*>::iterator it = m_objects->begin();
	for (it = m_objects->begin(); it != m_objects->end(); it++)
	{
		if ((*it)->getName() == name)
			return *it;
	}
    return NULL;
}