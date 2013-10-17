#include "pch.h"
#include "RotationProvider.h"


RotationProvider::RotationProvider(ROTATION_PROVIDER_TYPES type, float rotation) : m_rotation(rotation), m_type(type)
{
    if (m_type == COMPASS)
    {
        m_compassProvider = new CompassProvider();
        m_compassProvider->Init();
    }
    else if (m_type != STATIC)
    {
        //TODO: Error
    }
}

RotationProvider::~RotationProvider()
{
    m_compassProvider->Stop();
    delete m_compassProvider;
}

float RotationProvider::GetRotation()
{
    if (m_type == STATIC)
    {
        return m_rotation;
    }
    else if (m_type == COMPASS)//TODO: Check
    {
        return 360.0f - m_compassProvider->GetDirection();
    }
    else //TODO: Error report
    {
        return 0.0;
    }
}

void RotationProvider::SetRotation(float rotation)
{
    m_rotation = rotation;
}

void RotationProvider::AddRotation(float rotation)
{
    m_rotation += rotation;
}