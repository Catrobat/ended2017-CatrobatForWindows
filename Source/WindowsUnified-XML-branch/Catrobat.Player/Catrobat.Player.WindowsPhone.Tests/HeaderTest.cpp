#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

#include "XMLParser.h"
#include <time.h>
#include <ppltasks.h>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
	TEST_CLASS(HeaderTest)
	{
	public:
		TEST_METHOD(Header_Width_Height)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			Assert::AreEqual(project->GetScreenHeight(), 800);
			Assert::AreEqual(project->GetScreenWidth(), 480);
		}


		TEST_METHOD(Header_applicationBuildName)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::string empty_string = "";
			Assert::AreEqual(project->GetApplicationBuildName(), empty_string);
		}

		TEST_METHOD(Header_applicationBuildNumber)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			Assert::AreEqual(project->GetApplicationBuildNumber(), 0);
		}

		TEST_METHOD(Header_applicationName)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::string app_name = "Pocket Code";
			Assert::AreEqual(project->GetApplicationName(), app_name);
		}

		TEST_METHOD(Header_applicationVersion)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::string version = "0.9.14";
			Assert::AreEqual(project->GetApplicationVersion(), version);
		}

		TEST_METHOD(Header_CatrobatLanguageVersion)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::string version = "0.92";
			Assert::AreEqual(project->GetCatrobatLanguageVersion(), version);
		}

		/*TEST_METHOD(Header_UploadTime)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			time_t time = parser->ParseDateTime("");
			Assert::IsTrue(project->GetDateTimeUpload() == time);
		}*/
		
		TEST_METHOD(Header_Description)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::string description = "";
			Assert::AreEqual(project->GetDescription(), description);
		}

		TEST_METHOD(Header_DeviceName)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::string device = "LIFETAB_E733X";
			Assert::AreEqual(project->GetDeviceName(), device);
		}

		TEST_METHOD(Header_MediaLicense)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::string license = "";
			Assert::AreEqual(project->GetMediaLicense(), license);
		}

		TEST_METHOD(Header_Platform)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::string platform = "Android";
			Assert::AreEqual(project->GetPlatform(), platform);
		}

		TEST_METHOD(Header_PlatformVersion)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			Assert::AreEqual(project->GetPlatformVersion(), 19);
		}

		TEST_METHOD(Header_ProgramLicense)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::string license = "";
			Assert::AreEqual(project->GetProgramLicense(), license);
		}

		TEST_METHOD(Header_ProgramName)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::string name = "My first program";
			Assert::AreEqual(project->GetProgramName(), name);
		}

		TEST_METHOD(Header_RemixOf)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::string remix_of = "";
			Assert::AreEqual(project->GetRemixOf(), remix_of);
		}

		TEST_METHOD(Header_Tags)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::vector<std::string> tags = std::vector<std::string>();
			Assert::IsTrue((project->GetTags()) == tags);
		}

		TEST_METHOD(Header_Url)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::string url = "";
			Assert::AreEqual(project->GetUrl(), url);
		}

		TEST_METHOD(Header_UserHandle)
		{
			XMLParser *parser = new XMLParser();
			bool success = parser->LoadXML("Mole.xml");
			Concurrency::wait(1000);
			Assert::IsTrue(success);

			Project *project = parser->GetProject();
			std::string url = "";
			Assert::AreEqual(project->GetUserHandle(), url);
		}
	};


}