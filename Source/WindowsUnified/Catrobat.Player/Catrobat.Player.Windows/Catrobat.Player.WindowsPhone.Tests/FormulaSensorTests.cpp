//#include "pch.h"
//#include "CppUnitTest.h"
//#include "TestHelper.h"
//
//#include "FormulaTree.h"
//#include "Object.h"
//#include "Interpreter.h"
//#include "InclinationProvider.h"
//
//using namespace Microsoft::VisualStudio::CppUnitTestFramework;
//
//namespace PlayerWindowsPhone8Test
//{
//	TEST_CLASS(FormulaSensorTests)
//	{
//	public:
//		
//		TEST_METHOD(ReadSensorCompass)
//		{
//			FormulaTree *tree = new FormulaTree("SENSOR", "COMPASS_DIRECTION");
//			Object *object = new Object("TestObject");
//			
//			Interpreter *interpreter = Interpreter::Instance();
//
//			Assert::AreEqual(interpreter->EvaluateFormula(tree, object), double(0.0));
//		}
//
//		TEST_METHOD(ReadSensorInclinationX)
//		{
//			FormulaTree *tree = new FormulaTree("SENSOR", "X_INCLINATION");
//			Object *object = new Object("TestObject");
//			Interpreter *interpreter = Interpreter::Instance();
//
//			Assert::AreEqual(interpreter->EvaluateFormula(tree, object), double(0.0));
//		}
//
//		TEST_METHOD(ReadSensorInclinationY)
//		{
//			FormulaTree *tree = new FormulaTree("SENSOR", "Y_INCLINATION");
//			Object *object = new Object("TestObject");
//			Interpreter *interpreter = Interpreter::Instance();
//
//			Assert::AreEqual(interpreter->EvaluateFormula(tree, object), double(0.0));
//		}
//
//	};
//}
