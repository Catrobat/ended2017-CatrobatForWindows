#pragma once

#include <string>
#include <D3D11.h>

#include "TextureDaemon.h"
#include "ILook.h"

class Look
{
public:
	Look(Catrobat_Player::NativeComponent::ILook^ look);

	void LoadTexture(const std::shared_ptr<DX::DeviceResources>& deviceResources);

	std::string GetFileName() { return m_filename; }
	std::string GetName() { return m_name; }
	unsigned int GetWidth();
	unsigned int GetHeight();
	Microsoft::WRL::ComPtr<ID2D1Bitmap> GetBitMap();
	int GetPixelAlphaValue(D2D1_POINT_2F position);

private:
	std::unique_ptr<CatrobatTexture> m_texture;
	std::string m_filename;
	std::string m_name;
	std::vector<std::vector<int>> m_alphamap;

#if PSAPI_VERSION
	// Code for Catrobat.Player.WindowsPhone.Tests
public:
	void SetAlphaMap(std::vector<std::vector<int>> aMap);
	std::vector<std::vector<int>> GetAlphaMap() { return m_alphamap; };
#endif

};
