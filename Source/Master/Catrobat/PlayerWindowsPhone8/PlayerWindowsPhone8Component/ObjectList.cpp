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