#include "pch.h"
#include "CppUnitTest.h"
#include <fstream>
#include <iostream>
#include <ppltasks.h>

#include "Look.h"
#include "XMLParser.h"
#include "TextureDaemon.h"
#include "ProjectDaemon.h"
#include "Content\Basic2DRenderer.h"
#include "Common\DeviceResources.h"
#include "OutOfBoundsException.h"
#include "PlayerException.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace CatrobatPlayerWindowsPhoneTests
{
	TEST_CLASS(LookTests)
	{
	public:

		TEST_METHOD(Constructor)
		{
			std::shared_ptr<Look> look = std::shared_ptr<Look>(new Look("noFile.png", "background"));
			std::string expected = "noFile.png";
			Assert::AreEqual(expected, look->GetFileName());
			expected = "background";
			Assert::AreEqual(expected, look->GetName());
			Assert::IsTrue(look->GetAlphaMap().empty());
		}

		TEST_METHOD(SetAlphaMap)
		{
			std::shared_ptr<Look> look = std::shared_ptr<Look>(new Look("noFile", "nothing"));
			std::vector<std::vector<int>> alphamap = { { 240, 12, 7 },{ 13, 77, 18 } };
			look->SetAlphaMap(alphamap);

			Assert::IsFalse(look->GetAlphaMap().empty());
			Assert::AreEqual(240, look->GetAlphaMap().at(0).at(0));
			Assert::AreEqual(12, look->GetAlphaMap().at(0).at(1));
			Assert::AreEqual(18, look->GetAlphaMap().at(1).at(2));

			std::vector<std::vector<int>> nomap;
			look->SetAlphaMap(nomap);
			Assert::IsTrue(look->GetAlphaMap().empty());
		}

		TEST_METHOD(GetPixelAlphaValue)
		{
			std::shared_ptr<Look> look = std::shared_ptr<Look>(new Look("noFile", "nothing"));
			std::vector<std::vector<int>> alphamap = { {240, 12, 7}, {13, 77, 18} };
			look->SetAlphaMap(alphamap);
			
			D2D1_POINT_2F position = { -1, 0 };
			Assert::AreEqual(0, look->GetPixelAlphaValue(position));

			position = { 0, -1 };
			Assert::AreEqual(0, look->GetPixelAlphaValue(position));

			position = { 0, 0 };
			Assert::AreEqual(240, look->GetPixelAlphaValue(position));

			position = { 2,  1 };
			Assert::AreEqual(18, look->GetPixelAlphaValue(position));

			position = { 3, 0 };
			Assert::AreEqual(0, look->GetPixelAlphaValue(position));

			position = { 0, 2 };
			Assert::AreEqual(0, look->GetPixelAlphaValue(position));
		}

		TEST_METHOD(GetBitMap_Exception)
		{
			std::shared_ptr<Look> look = std::shared_ptr<Look>(new Look("noFile", "nothing"));
			std::string errorMsg;
			try
			{
				look->GetBitMap();
				errorMsg = "passed";
			}
			catch (PlayerException e)
			{
				errorMsg = e.GetErrorMessage();
			}
			Assert::AreEqual((std::string)"Look::GetTexture called with no texture defined.", errorMsg);
		}

		TEST_METHOD(GetWidth_Exception)
		{
			std::shared_ptr<Look> look = std::shared_ptr<Look>(new Look("noFile", "nothing"));
			std::string errorMsg;
			try
			{
				look->GetWidth();
			}
			catch (PlayerException e)
			{
				errorMsg = e.GetErrorMessage();
			}
			Assert::AreEqual((std::string)"Look::GetWidth called with no texture defined.", errorMsg);
		}

		TEST_METHOD(GetHeight_Exception)
		{
			std::shared_ptr<Look> look = std::shared_ptr<Look>(new Look("noFile", "nothing"));
			std::string errorMsg;
			try
			{
				look->GetHeight();
			}
			catch (PlayerException e)
			{
				errorMsg = e.GetErrorMessage();
			}
			Assert::AreEqual((std::string)"Look::GetHeight called with no texture defined.", errorMsg);
		}

	};
}