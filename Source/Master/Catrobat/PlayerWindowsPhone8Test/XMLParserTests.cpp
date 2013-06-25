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
		
		TEST_METHOD(XMLParserTests_SampleTest)
		{
			// TODO: Your test code here
            XMLParser *parser = new XMLParser();
            Assert::IsTrue(false);
		}
	};
}