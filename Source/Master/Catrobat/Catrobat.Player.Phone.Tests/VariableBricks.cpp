#include "pch.h"
//#include "CppUnitTest.h"
//#include "TestHelper.h"
//
//#include "Object.h"
//#include "StartScript.h"
//#include "UserVariable.h"
//
//#include "ChangeVariableBrick.h"
//#include "SetVariableBrick.h"
//#include "VariableManagementBrick.h"
//
//using namespace Microsoft::VisualStudio::CppUnitTestFramework;
//
//namespace PlayerWindowsPhone8Test
//{
//	TEST_CLASS(VariableBricks)
//	{
//	public:
//		
//		TEST_METHOD(VariableBricks_SetVariableBrick_SimpleCheck)
//		{
//            string variableName = "testVariable";
//            UserVariable *variable = new UserVariable(variableName, "5");
//            FormulaTree *formulaTree = new FormulaTree("USER_VARIABLE", variableName);
//			string spriteReference = "";
//			Object *object = new Object("TestObject"); 
//
//			StartScript *script = new StartScript(object);
//            formulaTree = new FormulaTree("NUMBER", "10");
//            SetVariableBrick *brick = new SetVariableBrick(spriteReference, formulaTree, script);
//            brick->SetVariable(variable);
//
//            Assert::AreEqual(atoi(variable->GetValue().c_str()), 5);
//            brick->Execute();
//            Assert::AreEqual(atoi(variable->GetValue().c_str()), 10);
//		}
//
//        TEST_METHOD(VariableBricks_SetVariableBrick_NegativCheck)
//		{
//            string variableName = "testVariable";
//            UserVariable *variable = new UserVariable(variableName, "5");
//            FormulaTree *formulaTree = new FormulaTree("USER_VARIABLE", variableName);
//			string spriteReference = "";
//			Object *object = new Object("TestObject"); 
//
//			StartScript *script = new StartScript(object);
//            formulaTree = new FormulaTree("NUMBER", "-20");
//            SetVariableBrick *brick = new SetVariableBrick(spriteReference, formulaTree, script);
//            brick->SetVariable(variable);
//
//            Assert::AreEqual(atoi(variable->GetValue().c_str()), 5);
//            brick->Execute();
//            Assert::AreEqual(atoi(variable->GetValue().c_str()), -20);
//		}
//
//        TEST_METHOD(VariableBricks_ChangeVariableBrick_SimpleCheck)
//		{
//            string variableName = "testVariable";
//            UserVariable *variable = new UserVariable(variableName, "5");
//            FormulaTree *formulaTree = new FormulaTree("USER_VARIABLE", variableName);
//			string spriteReference = "";
//			Object *object = new Object("TestObject"); 
//
//			StartScript *script = new StartScript(object);
//            formulaTree = new FormulaTree("NUMBER", "10");
//            ChangeVariableBrick *brick = new ChangeVariableBrick(spriteReference, formulaTree, script);
//            brick->SetVariable(variable);
//
//            Assert::AreEqual(atoi(variable->GetValue().c_str()), 5);
//            brick->Execute();
//            Assert::AreEqual(atoi(variable->GetValue().c_str()), 15);
//		}
//
//        TEST_METHOD(VariableBricks_ChangeVariableBrick_NegativCheck)
//		{
//			string variableName = "testVariable";
//            UserVariable *variable = new UserVariable(variableName, "5");
//            FormulaTree *formulaTree = new FormulaTree("USER_VARIABLE", variableName);
//			string spriteReference = "";
//			Object *object = new Object("TestObject"); 
//
//			StartScript *script = new StartScript(object);
//            formulaTree = new FormulaTree("NUMBER", "-20");
//            ChangeVariableBrick *brick = new ChangeVariableBrick(spriteReference, formulaTree, script);
//            brick->SetVariable(variable);
//
//            Assert::AreEqual(atoi(variable->GetValue().c_str()), 5);
//            brick->Execute();
//            Assert::AreEqual(atoi(variable->GetValue().c_str()), -15);
//		}
//	};
//}