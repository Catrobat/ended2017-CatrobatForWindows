#include "pch.h"
#include "Brick.h"


Brick::Brick(TypeOfBrick brickType, string spriteReference) :
	BaseObject(200, 500, 1, 1, 0, 0),
	m_brickType(brickType), m_spriteReference(spriteReference)
{
}

Brick::TypeOfBrick Brick::BrickType()
{
	return m_brickType;
}

void Brick::Render(SpriteBatch *spriteBatch)
{
	Draw(spriteBatch);
}

void Brick::LoadTextures(ID3D11Device* d3dDevice)
{
	LoadTexture(d3dDevice);
}

void Brick::Draw(SpriteBatch *spriteBatch)
{
	spriteBatch->Draw(m_Texture, m_position, nullptr, Colors::Wheat, 0.0f, m_sourceOrigin, m_objectScale, SpriteEffects_None, 0.0f);
}

void Brick::LoadTexture(ID3D11Device* d3dDevice)
{
	
	Windows::ApplicationModel::Package^ package = Windows::ApplicationModel::Package::Current;
	Windows::Storage::StorageFolder^ installedLocation = package->InstalledLocation;
	auto files = installedLocation->GetFilesAsync();
	auto appInstallDirectory = Windows::Storage::ApplicationData::Current->LocalFolder;
	Platform::String^ output = "Installed Location: " + installedLocation->Path;
	CreateDDSTextureFromFile(d3dDevice, L"testProject/images/34A109A82231694B6FE09C216B390570_normalCat.dds", nullptr, &m_Texture, MAXSIZE_T);
	//CreateDDSTextureFromFile(d3dDevice, L"brick.dds", nullptr, &m_Texture, MAXSIZE_T);
}

