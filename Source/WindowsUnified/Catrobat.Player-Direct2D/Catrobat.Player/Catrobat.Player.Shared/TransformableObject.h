#pragma once

#include <string>
#include <list>

#include "Look.h"
#include "Script.h"
#include "SoundInfo.h"
#include "UserVariable.h"
#include "WhenScript.h"

class TransformableObject
{
public:
    TransformableObject();

public:
    void SetTranslation(float x, float y);
    void GetTranslation(float &x, float &y);

    void SetTransparency(float transparency);
    float GetTransparency();

    void SetRotation(float rotation);
    float GetRotation();

    void SetScale(float x, float y);
    void GetScale(float &x, float &y);

private:
    float m_opacity;
    float m_rotation;
    D2D1_POINT_2F m_translation;
    D2D1_SIZE_F m_objectScale;
    D2D1::Matrix3x2F m_transformation;
    D2D1_SIZE_F m_ratio;
    D2D1_SIZE_F m_logicalSize;
    D2D1_SIZE_F m_renderTargetSize;

private:
    Look *m_look;
    list<Look*> *m_lookList;
    list<Script*> *m_scripts;
    list<SoundInfo*> *m_soundInfos;
    std::map<std::string, UserVariable*> *m_variableList;
    string m_name;

private:
    void RecalculateTransformation();
};

