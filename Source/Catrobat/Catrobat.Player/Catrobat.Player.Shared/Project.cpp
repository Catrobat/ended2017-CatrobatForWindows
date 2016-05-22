#include "pch.h"
#include "Project.h"
#include "Constants.h"

using namespace std;
using namespace ProjectStructure;
using namespace Catrobat_Player::NativeComponent;


Project::Project(IProject^ project) :
	m_header(make_unique<Header>(project->Header))
{
	for each (Catrobat_Player::NativeComponent::IObject^ object in project->Objects)
	{
		m_objectList.insert(std::pair<std::string, std::shared_ptr<Object> >(Helper::StdString(object->Name), make_shared<Object>(object)));
	}
}

Project::~Project()
{
}

void Project::CheckProjectScreenSize()
{
	if (GetHeader()->GetScreenHeight() == 0 || GetHeader()->GetScreenWidth() == 0)
	{
		GetHeader()->SetDefaultScreenSize();
	}
}

void Project::SetupWindowSizeDependentResources(
	const shared_ptr<DX::DeviceResources>& deviceResources)
{
	for each (pair<string, shared_ptr<Object>> obj in m_objectList)
	{
		obj.second->SetupWindowSizeDependentResources(deviceResources);
	}
}

void Project::LoadTextures(const shared_ptr<DX::DeviceResources>& deviceResources)
{
	for each (pair<string, shared_ptr<Object>> obj in m_objectList)
	{
		obj.second->LoadTextures(deviceResources);
	}
}

void Project::StartUp()
{
	for each (pair<string, shared_ptr<Object>> obj in m_objectList)
	{
		obj.second->StartUp();
	}
}

void Project::Render(const shared_ptr<DX::DeviceResources>& deviceResources)
{
	for each (pair<string, shared_ptr<Object>> obj in m_objectList)
	{
		obj.second->Draw(deviceResources);
	}
}

shared_ptr<UserVariable> Project::GetVariable(string name)
{
	map<string, shared_ptr<UserVariable >>::iterator searchItem = m_variableList.find(name);
	if (searchItem != m_variableList.end())
		return searchItem->second;
	return NULL;
}

