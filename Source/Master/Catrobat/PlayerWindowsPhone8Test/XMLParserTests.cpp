#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

#include "XMLParser.h"
#include <ppltasks.h>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
	TEST_CLASS(XMLParserTests)
	{
	public:
		
        TEST_METHOD(XMLParserTests_FileNotFound)
		{
            XMLParser *parser = new XMLParser();
            bool success = parser->loadXML("nonExistingProject.xml");
            Assert::IsFalse(success);
		}

		TEST_METHOD(XMLParserTests_Header)
		{
            XMLParser *parser = new XMLParser();
            bool success = parser->loadXML("TestFiles/HeaderTest.xml");
            Concurrency::wait(1000);
			Assert::IsTrue(success);

            Project *project = parser->getProject();
            Assert::AreEqual(project->GetScreenHeight(), 1184);
            Assert::AreEqual(project->GetScreenWidth(), 768);
		}

		TEST_METHOD(XMLParserTests_SimpleObjectList)
		{
            XMLParser *parser = new XMLParser();
			bool success = parser->loadXML("TestFiles/ObjectListTest1.xml");
			Assert::IsTrue(success);

            Project *project = parser->getProject();
			Assert::AreEqual(project->GetObjectList()->GetSize(), 2);
		}

		TEST_METHOD(XMLParserTests_ObjectList)
		{
            XMLParser *parser = new XMLParser();
			bool success = parser->loadXML("TestFiles/ObjectListTest2.xml");
			Assert::IsTrue(success);

            Project *project = parser->getProject();
			Assert::AreEqual(project->GetObjectList()->GetSize(), 30);
		}

		TEST_METHOD(XMLParserTests_BroadcastTest)
		{
            XMLParser *parser = new XMLParser();
			bool success = parser->loadXML("TestFiles/BroadcastTest.xml");
			Assert::IsTrue(success);

            Project *project = parser->getProject();
			Assert::AreEqual(project->GetObjectList()->GetSize(), 2);
		}
	};
}