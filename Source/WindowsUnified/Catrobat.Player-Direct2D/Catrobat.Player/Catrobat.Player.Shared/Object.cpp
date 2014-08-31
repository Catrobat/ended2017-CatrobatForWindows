#include "pch.h"
#include "Object.h"
#include "ProjectDaemon.h"
#include "PlayerException.h"

#include <exception>

Object::Object(string name) :
BaseObject(),
m_name(name),
m_opacity(1),
m_rotation(0.0f),
m_look(NULL)
{
    m_lookList = new list<Look*>();
    m_scripts = new list<Script*>();
    m_soundInfos = new list<SoundInfo*>();
    m_variableList = new map<string, UserVariable*>();
}

void Object::AddLook(Look *lookData)
{
    m_lookList->push_back(lookData);
}

void Object::AddScript(Script *script)
{
    m_scripts->push_back(script);
}

void Object::AddSoundInfo(SoundInfo *soundInfo)
{
    m_soundInfos->push_back(soundInfo);
}

string Object::GetName()
{
    return m_name;
}

int Object::GetScriptListSize()
{
    return m_scripts->size();
}

int Object::GetLookDataListSize()
{
    return m_lookList->size();
}

Look *Object::GetLook(int index)
{
    list<Look*>::iterator it = m_lookList->begin();
    advance(it, index);

    if (it != m_lookList->end())
    {
        return *it;
    }

    return NULL;
}

Script *Object::GetScript(int index)
{
    list<Script*>::iterator it = m_scripts->begin();
    advance(it, index);
    return *it;
}

void Object::LoadTextures(const std::shared_ptr<DX::DeviceResources>& deviceResources)
{
    SetTranslation(0.f, 0.f);

    for (int i = 0; i < GetLookDataListSize(); i++) //TODO: implement dummy texture if there is no texture found
    {
        m_look = GetLook(i);

        if (m_look != NULL)
        {
            m_look->LoadTexture(deviceResources);
            m_origin = XMFLOAT2(((float) m_look->GetWidth()) / 2.0f, ((float) m_look->GetHeight()) / 2.0f);
        }
    }
}

double radians(float degree)
{
    return degree * 3.14159265 / 180.0f;
}

/*
Draw the current look of this object.
*/
void Object::Draw(ID2D1DeviceContext1* deviceContext)
{
    if (m_look == NULL)
    {
        return;
    }

    deviceContext->SetTransform(D2D1::Matrix3x2F::Identity());
    deviceContext->Clear(D2D1::ColorF(D2D1::ColorF::White));
    D2D1_SIZE_F size = m_look->GetBitMap()->GetSize();
    D2D1_POINT_2F ulc = D2D1::Point2F(100.f, 10.f);
    deviceContext->DrawBitmap(m_look->GetBitMap(), D2D1::RectF(ulc.x,
        ulc.y, ulc.x + size.width, ulc.y + size.height));
}

XMMATRIX Object::GetWorldMatrix()
{
    XMFLOAT2 position;
    position.x = ProjectDaemon::Instance()->GetProject()->GetScreenWidth() / 2.0f + m_position.x;
    position.y = ProjectDaemon::Instance()->GetProject()->GetScreenHeight() / 2.0f + m_position.y;

    XMMATRIX translation = XMMatrixTranslation(position.x, position.y, 0.0f);
    XMMATRIX rotation = XMMatrixRotationZ(m_rotation);
    XMMATRIX scale = XMMatrixScaling(m_objectScale.x, m_objectScale.y, 1.0f);
    return translation * rotation * scale;
}

void Object::SetLook(int index)
{
    m_look = GetLook(index);
}

int Object::GetLook()
{
    int i = 0;
    list<Look*>::iterator it = m_lookList->begin();

    while ((*it) != m_look)
    {
        it++;
        i++;
    }

    if (it != m_lookList->end())
    {
        return i;
    }

    return 0;
}

int Object::GetLookCount()
{
    return m_lookList->size();
}

Look* Object::GetCurrentLook()
{
    if (!m_look)
    {
        return GetLook(0);
    }

    return m_look;
}

Bounds Object::GetBounds()
{
    Bounds bounds;
    bounds.x = 0.0f;
    bounds.y = 0.0f;
    bounds.width = 0.0f;
    bounds.height = 0.0f;

    auto currentLook = GetCurrentLook();

    if (currentLook != NULL)
    {
        bounds.x = m_position.x - currentLook->GetWidth() / 2.0f;
        bounds.y = m_position.y - currentLook->GetHeight() / 2.0f;
        bounds.width = (float) currentLook->GetWidth();
        bounds.height = (float) currentLook->GetHeight();
    }

    return bounds;
}

//Executes all Startscripts of the object and sets the first look if the
//costume list is not empty.
void Object::StartUp()
{
    if (m_lookList != NULL && m_lookList->size() > 0)
    {
        m_look = m_lookList->front();
    }

    for (int i = 0; i < GetScriptListSize(); i++)
    {
        Script *script = GetScript(i);

        if (script->GetType() == Script::TypeOfScript::StartScript)
        {
            script->Execute();
        }
    }
}

void Object::SetTranslation(float x, float y)
{
    m_translation.x = x;
    m_translation.y = y;

    //TODO: right positioning
    m_position.x = ProjectDaemon::Instance()->GetProject()->GetScreenWidth() / 2.0f + x;
    m_position.y = ProjectDaemon::Instance()->GetProject()->GetScreenHeight() / 2.0f + y;
}

void Object::GetTranslation(float &x, float &y)
{
    x = m_translation.x;
    y = m_translation.y;
}

void Object::GetPosition(float &x, float &y)
{
    x = m_position.x;
    y = m_position.y;
}

void Object::SetTransparency(float transparency)
{
    if ((1.0f - transparency) > 0)
    {
        if ((1.0f - transparency) < 1.0f)
        {
            m_opacity = 1.0f - transparency;
        }
        else
        {
            m_opacity = 1.0f;
        }
    }
    else
    {
        m_opacity = 0.0f;
    }
}

float Object::GetTransparency()
{
    return 1.0f - m_opacity;
}

void Object::SetRotation(float rotation)
{
    m_rotation = rotation;
}

float Object::GetRotation()
{
    return m_rotation;
}

void Object::SetScale(float scale)
{
    if (scale < 0.0f)
    {
        scale = 0.0f;
    }

    m_objectScale.x = m_objectScale.y = scale / 100.0f;
}

float Object::GetScale()
{
    return m_objectScale.x * 100.0f;
}

void Object::AddVariable(string name, UserVariable* variable)
{
    m_variableList->insert(pair<string, UserVariable*>(name, variable));
}

void Object::AddVariable(pair<string, UserVariable*> variable)
{
    m_variableList->insert(variable);
}

UserVariable* Object::GetVariable(string name)
{
    map<string, UserVariable*>::iterator searchItem = m_variableList->find(name);

    if (searchItem != m_variableList->end())
    {
        return searchItem->second;
    }

    return NULL;
}

void Object::SetWhenScript(WhenScript* whenScript)
{
    m_whenScript = whenScript;
}

WhenScript* Object::GetWhenScript()
{
    return m_whenScript;
}

bool Object::IsTapPointHitting(ID3D11DeviceContext1* context, ID3D11Device* device, double xPosition, double yPosition, double resolutionFactor)
{
    bool isHitting = false;

    auto bounds = GetBounds();

    if (context == NULL || device == NULL)
        return false;

    if (m_opacity > 0)
    {
        XMVECTOR sourcePosition = XMVectorSet((float) xPosition, (float) yPosition, 0.f, 0.f);
        FXMVECTOR scale = XMVectorSet(m_objectScale.x, m_objectScale.x, 0.0f, 0.0f);
        FXMVECTOR rotationOrigin = XMVectorSet(m_position.x, m_position.y, 0.0f, 0.0f);
        FXMVECTOR translation = XMVectorSet(0.0f, 0.0f, 0.0f, 0.0f);

        XMMATRIX matrix = XMMatrixTransformation2D(rotationOrigin, 0.0f, scale, rotationOrigin, (float) radians(m_rotation), translation);
        XMVECTOR *determinant = nullptr;
        matrix = XMMatrixInverse(determinant, matrix);
        XMVECTOR newPosition = XMVector2TransformCoord(sourcePosition, matrix);

        //Store to XMLFLOAT2 because of compatibility issues when switching between ARM and Win32
        XMFLOAT2 newPos;
        XMStoreFloat2(&newPos, newPosition);

        //Set the position back because sprites were placed at texture width / 2.0 and texture height / 2.0 before
        newPos.x -= bounds.x;
        newPos.y -= bounds.y;

        D3D11_SHADER_RESOURCE_VIEW_DESC pDesc;

        //if (bounds.x <= xNormalized &&
        //    bounds.y <= yNormalized &&
        //    (bounds.x + bounds.width) >= xNormalized &&
        //    (bounds.y + bounds.height) >= yNormalized)
        //{
        //    //TODO: Calculate bounding box accordingly to the transformation and check if tap point is in between the borders
        //}
        //else
        {
            if (m_look->GetResourceView() == NULL)
            {
                //TODO: throw exception
                isHitting = false;
            }
            else
            {
                m_look->GetResourceView()->GetDesc(&pDesc);
                ID3D11Texture2D* pOurStagingTexture;

                D3D11_TEXTURE2D_DESC stagingTextureDescription;
                stagingTextureDescription.Width = 1;
                stagingTextureDescription.Height = 1;
                stagingTextureDescription.MipLevels = 1;
                stagingTextureDescription.ArraySize = 1;
                stagingTextureDescription.SampleDesc.Count = 1;
                stagingTextureDescription.SampleDesc.Quality = 0;
                stagingTextureDescription.Format = pDesc.Format;

                //D3D11_USAGE_STAGING = a resource that supports data transfer (copy) from the GPU to the CPU.
                stagingTextureDescription.Usage = D3D11_USAGE_STAGING;
                stagingTextureDescription.BindFlags = 0;

                //D3D11_CPU_ACCESS_READ = The resource is to be mappable so that the CPU can read its contents.
                //Resources created with this flag cannot be set as either inputs or outputs to the pipeline
                //and must be created with staging usage.
                stagingTextureDescription.CPUAccessFlags = D3D11_CPU_ACCESS_READ;
                stagingTextureDescription.MiscFlags = 0;

                try
                {
                    HRESULT result = device->CreateTexture2D(&stagingTextureDescription, NULL, &pOurStagingTexture);
                    auto currentLook = GetCurrentLook();

                    //TODO: Delete this - is just for debug reasons
                    float width = currentLook->GetWidth();
                    float height = currentLook->GetHeight();
                    //------------------

                    if (FAILED(result) || newPos.x < 0 || newPos.y < 0 || newPos.x + 1 > currentLook->GetWidth() || newPos.y + 1 > currentLook->GetHeight())
                    {
                        //TODO: Error handling and check if newPos.x + 1 can't exceed the width and height of the texture
                        isHitting = false;
                        if (pOurStagingTexture != NULL)
                            pOurStagingTexture->Release();
                    }
                    else
                    {
                        //TODO: Check srcBox size according to texture bounds (see above)
                        D3D11_BOX srcBox;
                        srcBox.left = newPos.x;
                        srcBox.right = srcBox.left + 1;
                        srcBox.top = newPos.y;
                        srcBox.bottom = srcBox.top + 1;
                        srcBox.front = 0;
                        srcBox.back = 1;

                        if (m_look->GetTexture() == NULL || pOurStagingTexture == NULL)
                        {
                            //TODO: Error handling
                            isHitting = false;
                            if (pOurStagingTexture != NULL)
                                pOurStagingTexture->Release();
                        }
                        else
                        {
                            context->CopySubresourceRegion(pOurStagingTexture, 0, 0, 0, 0, m_look->GetTexture(), 0, &srcBox);

                            if (pOurStagingTexture == NULL)
                            {
                                //TODO: Error handling
                                isHitting = false;
                            }
                            else
                            {
                                D3D11_MAPPED_SUBRESOURCE msr;
                                result = context->Map(pOurStagingTexture, 0, D3D11_MAP_READ, 0, &msr);
                                BYTE *pixel = (BYTE*) msr.pData;

                                if (FAILED(result) || msr.pData == NULL)
                                {
                                    //TODO: Error handling
                                    //auto bla = device->GetDeviceRemovedReason();
                                    isHitting = false;
                                    if (pOurStagingTexture != NULL)
                                        context->Unmap(pOurStagingTexture, 0);
                                }
                                else
                                {
                                    //TODO: Delete unnecessary rgb pixels. Just keep alpha in release version
                                    // copy data
                                    BYTE p1 = pixel[0];
                                    BYTE p2 = pixel[1];
                                    BYTE p3 = pixel[2];
                                    BYTE p4 = pixel[3];

                                    if (pOurStagingTexture != NULL)
                                        context->Unmap(pOurStagingTexture, 0);
                                    isHitting = p4 > 0;
                                }

                                if (pOurStagingTexture != NULL)
                                    pOurStagingTexture->Release();
                            }
                        }
                    }
                }
                catch (exception e)
                {
                    //TODO: Error handling
                }
            }
        }
    }

    return isHitting;
}