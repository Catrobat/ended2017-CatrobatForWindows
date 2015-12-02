#pragma once

#include "Object.h"
#include "UserVariable.h"
#include "IProject.h"
#include "Helper.h"
#include "Header.h"

#include <vector>

namespace ProjectStructure
{
	class Project
	{
	public:
		Project(Catrobat_Player::NativeComponent::IProject^ project);
		~Project();

		void CheckProjectScreenSize();
		void SetupWindowSizeDependentResources(const std::shared_ptr<DX::DeviceResources>& deviceResources);
		void LoadTextures(const std::shared_ptr<DX::DeviceResources>& deviceResources);
		void StartUp();
		void Render(const std::shared_ptr<DX::DeviceResources>& deviceResources);
		std::shared_ptr<UserVariable> GetVariable(std::string name);

		std::unique_ptr<Header> const & GetHeader() { return m_header; }
		std::map<std::string, std::shared_ptr<Object> > GetObjectList() { return m_objectList; }

	private:
		std::unique_ptr<Header> m_header;
		std::map<std::string, std::shared_ptr<Object> > m_objectList;
		std::map<std::string, std::shared_ptr<Object> > m_objectListInitial;
		std::map<std::string, std::shared_ptr<UserVariable> > m_variableList;
		std::map<std::string, std::string> m_variableListValueInitial;
	};
}