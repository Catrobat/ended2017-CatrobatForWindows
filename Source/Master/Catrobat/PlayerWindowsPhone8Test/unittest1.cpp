#include "pch.h"
#include "CppUnitTest.h"
#include "Object.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
    TEST_CLASS(UnitTest1)
    {
    public:
        TEST_METHOD(hallo)
        {
            // TODO: Your test code here
			Object *testObject = new Object("testobj");
			Assert::IsNotNull(testObject);
        }
    };
}