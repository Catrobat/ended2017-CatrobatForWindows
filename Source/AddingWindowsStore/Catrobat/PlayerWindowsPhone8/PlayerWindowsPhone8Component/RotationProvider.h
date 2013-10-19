#pragma once
#include "CompassProvider.h"

enum ROTATION_PROVIDER_TYPES
{
    STATIC,
    COMPASS
};

class RotationProvider
{
public:
    RotationProvider(ROTATION_PROVIDER_TYPES type, float rotation);
    ~RotationProvider();

    float GetRotation();
    void SetRotation(float rotation);
    void AddRotation(float rotation);
private:
    float m_rotation;
    ROTATION_PROVIDER_TYPES m_type;
    CompassProvider* m_compassProvider;
};

