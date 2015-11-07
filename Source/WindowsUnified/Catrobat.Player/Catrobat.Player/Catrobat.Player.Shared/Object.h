#pragma once

#include <string>
#include <list>
#include <D3D11.h>
#include <windows.foundation.h>

#include "Common\DeviceResources.h"
#include "Look.h"
#include "SoundInfo.h"
#include "UserVariable.h"
#include "WhenScript.h"
#include "IObject.h"

class Script;
class Object
{
public:
	Object(Catrobat_Player::NativeComponent::IObject^ object);
	~Object();

public:
	void SetTranslation(float x, float y);
	void GetTranslation(float &x, float &y);

	void SetTransparency(float transparency);
	float GetTransparency();

	void SetRotation(float rotation);
	float GetRotation();

	void SetScale(float x, float y);
	void GetScale(float &x, float &y);

public:
	void LoadTextures(const std::shared_ptr<DX::DeviceResources>& deviceResources);
	void SetupWindowSizeDependentResources(const std::shared_ptr<DX::DeviceResources>& deviceResources);
	void StartUp();
	void Draw(const std::shared_ptr<DX::DeviceResources>& deviceResources);

public:
	void SetLook(int index);
	std::string GetName() { return m_name; }
	int GetLookListSize() { return m_lookList.size(); }
	int GetScriptListSize() { return m_scripts.size(); }
	std::list<SoundInfo*>* GetSoundInfos() { return m_soundInfos; };
	std::shared_ptr<Script> GetScript(int index);
	std::shared_ptr<UserVariable> GetVariable(std::string name);
	int GetIndexOfCurrentLook();
	std::shared_ptr<Look> GetCurrentLook() { return m_look; };
	bool IsObjectHit(D2D1_POINT_2F position);

private:
	void RecalculateTransformation();
	D2D1::Matrix3x2F CalculateTranslationMatrix();
	static double Radians(float degree) { return degree * DirectX::XM_PI / 180.0f; }

private:
	std::string m_name;
	std::shared_ptr<Look> m_look;
	std::list<std::shared_ptr<Look> > m_lookList;
	std::list<std::shared_ptr<Script> > m_scripts;
	std::map<std::string, std::shared_ptr<UserVariable> > m_variableList;
	std::list<SoundInfo*> *m_soundInfos;

private:
	float m_opacity;
	float m_rotation;
	D2D1_POINT_2F m_translation;
	D2D1_SIZE_F m_objectScale;
	D2D1::Matrix3x2F m_transformation;
	D2D1_SIZE_F m_ratio;
	D2D1_SIZE_F m_logicalSize;
	D2D1_SIZE_F m_renderTargetSize;
};
