#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

#include "XMLParser.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
	TEST_CLASS(XMLParserTests)
	{
	public:
		
        TEST_METHOD(XMLParserTests_FileNotFound)
		{
            XMLParser *parser = new XMLParser();
            parser->loadXML("nonExistingProject.xml");
            Assert::IsTrue(false);
		}

		TEST_METHOD(XMLParserTests_Header)
		{
            XMLParser *parser = new XMLParser();
            parser->loadXML("TestFiles/headerTest.xml");

            Project *project = parser->getProject();
            Assert::AreEqual(project->ScreenHeight(), 1184);
            Assert::AreEqual(project->ScreenWidth(), 768);
		}
	};
}