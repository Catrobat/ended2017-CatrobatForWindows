#include "pch.h"
#include "CppUnitTest.h"
#include "Object.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
    TEST_CLASS(LookBricks)
    {
    public:
        TEST_METHOD(ChangeGhostEffectBrick)
        {
            // TODO: Your test code here
			Object *testObject = new Object("testobj");
			Assert::IsNotNull(testObject);
        }

		TEST_METHOD(ChangeSizeByBrick)
        {
            // TODO: Your test code here
        }

		TEST_METHOD(CostumeBrick)
        {
            // TODO: Your test code here
        }

		TEST_METHOD(HideBrick)
        {
            // TODO: Your test code here
        }

		TEST_METHOD(NextLookBrick)
        {
            // TODO: Your test code here
        }

		TEST_METHOD(SetGhostEffectBrick)
        {
            // TODO: Your test code here
        }

		TEST_METHOD(SetSizeToBrick)
        {
            // TODO: Your test code here
        }

		TEST_METHOD(ShowBrick)
        {
            // TODO: Your test code here
        }
    };
}