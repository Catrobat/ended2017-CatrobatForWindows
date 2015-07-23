#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

#include "XMLParser.h"
#include <ppltasks.h>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
    TEST_CLASS(IfLogicBeginBrick)
    {
    public:

        TEST_METHOD(IfLogicBeginBrick_Test)
        {
            XMLParser *parser = new XMLParser();
            bool success = parser->LoadXML("TestFiles/IfLogicBeginTest.xml");
			Assert::IsTrue(success);

            Project *project = parser->GetProject();
            Assert::AreEqual(project->GetObjectList().size(), size_t(1));
        }
    };
}