#include "pch.h"
#include "Script.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>
#include <ppltasks.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;

Script::Script(TypeOfScript scriptType, Object *parent) :
m_scriptType(scriptType), m_parent(parent)
{
    m_brickList = new list<Brick*>();
}

void Script::AddBrick(Brick *brick)
{
    m_brickList->push_back(brick);
}

void Script::AddSpriteReference(string spriteReference)
{
    m_spriteReference = spriteReference;
}

Script::TypeOfScript Script::GetType()
{
    return m_scriptType;
}

int Script::GetBrickListSize()
{
    return m_brickList->size();
}

Brick *Script::GetBrick(int index)
{
    list<Brick*>::iterator it = m_brickList->begin();
    advance(it, index);
    return *it;
}

void Script::Execute()
{
    auto workItem = ref new WorkItemHandler(
        [this](IAsyncAction^ workItem)
    {
        for (int i = 0; i < GetBrickListSize(); i++)
        {
            GetBrick(i)->Execute();
        }

        Concurrency::wait(10); //TODO: neccessary?
    });

    m_threadPoolWorkItem = ThreadPool::RunAsync(workItem);
}

Object *Script::GetParent()
{
    return m_parent;
}

bool Script::IsRunning()
{
    if (m_threadPoolWorkItem == nullptr || m_threadPoolWorkItem->Status != Windows::Foundation::AsyncStatus::Started)
    {
        return false;
    }

    return true;
}