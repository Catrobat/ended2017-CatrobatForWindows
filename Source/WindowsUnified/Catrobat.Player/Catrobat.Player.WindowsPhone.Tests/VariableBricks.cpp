#include "pch.h"
#include "CppUnitTest.h"
#include "TestHelper.h"

#include "Object.h"
#include "StartScript.h"
#include "UserVariable.h"

#include "ChangeVariableBrick.h"
#include "SetVariableBrick.h"
#include "VariableManagementBrick.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace PlayerWindowsPhone8Test
{
	TEST_CLASS(VariableBricks)
	{
    private:

        string m_variableName;
        string m_defaultValue;
        string m_formulaValue;
        UserVariable *m_variable;
        FormulaTree *m_formulaTree;
        Object *m_object;
        std::shared_ptr<StartScript> m_script;

        TEST_METHOD_INITIALIZE(TestInitialization)
        {
            m_defaultValue = "5";
            m_variableName = "testVariable";
            m_variable = new UserVariable(m_variableName, m_defaultValue);
            m_object = new Object("TestObject");
            m_script = std::shared_ptr<StartScript>(new StartScript(m_object));
            m_formulaTree = NULL;
        }

        TEST_METHOD_CLEANUP(TestCleanup)
        {
         
        }


	public:
		
		TEST_METHOD(VariableBricks_SetVariableBrick_SimpleCheck)
		{
            m_formulaValue = "10";
            m_formulaTree = new FormulaTree("NUMBER", m_formulaValue);
            SetVariableBrick *brick = new SetVariableBrick(m_formulaTree, m_script);
            brick->SetVariable(m_variable);

            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_defaultValue.c_str()));
            brick->Execute();
            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_formulaValue.c_str()));
		}

        TEST_METHOD(VariableBricks_SetVariableBrick_NegativCheck)
		{
            m_formulaValue = "-20";
            m_formulaTree = new FormulaTree("NUMBER", m_formulaValue);
            SetVariableBrick *brick = new SetVariableBrick(m_formulaTree, m_script);
            brick->SetVariable(m_variable);

            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_defaultValue.c_str()));
            brick->Execute();
            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_formulaValue.c_str()));
		}

        TEST_METHOD(VariableBricks_ChangeVariableBrick_SimpleCheck)
		{
            m_formulaValue = "10";
            m_formulaTree = new FormulaTree("NUMBER", m_formulaValue);
            ChangeVariableBrick *brick = new ChangeVariableBrick(m_formulaTree, m_script);
            brick->SetVariable(m_variable);

            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_defaultValue.c_str()));
            brick->Execute();
            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_defaultValue.c_str()) + atof(m_formulaValue.c_str()));
            //delete brick;
		}

        TEST_METHOD(VariableBricks_ChangeVariableBrick_NegativCheck)
		{
            m_formulaValue = "-20";
            m_formulaTree = new FormulaTree("NUMBER", m_formulaValue);
            ChangeVariableBrick *brick = new ChangeVariableBrick(m_formulaTree, m_script);
            brick->SetVariable(m_variable);

            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_defaultValue.c_str()));
            brick->Execute();
            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_defaultValue.c_str()) + atof(m_formulaValue.c_str()));
            //delete brick;
		}

        TEST_METHOD(VariableBricks_ChangeVariableBrick_FormulaNoNumberCheck)
        {        
            m_formulaValue = "0x10";
            m_formulaTree = new FormulaTree("NUMBER", m_formulaValue);
            ChangeVariableBrick *brick = new ChangeVariableBrick(m_formulaTree, m_script);
            brick->SetVariable(m_variable);

            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_defaultValue.c_str()));
            brick->Execute();
            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_defaultValue.c_str()));
            //delete brick;
        }

        TEST_METHOD(VariableBricks_ChangeVariableBrick_OverFlowCheck)
        {
            m_formulaValue = std::to_string(DBL_MAX);
            m_formulaTree = new FormulaTree("NUMBER", m_formulaValue);
            ChangeVariableBrick *brick = new ChangeVariableBrick(m_formulaTree, m_script);
            brick->SetVariable(m_variable);

            // TODO due to wrong conversion, no actual overflow
            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_defaultValue.c_str()));
            brick->Execute();
            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_defaultValue.c_str()));
            //delete brick;
        }

        TEST_METHOD(VariableBricks_ChangeVariableBrick_UnderFlowCheck)
        {
            m_defaultValue = "-10";
            m_variable->SetValue(m_defaultValue);

            m_formulaValue = std::to_string(-DBL_MAX); // TODO conversion of DBL_MIN does not work
            m_formulaTree = new FormulaTree("NUMBER", m_formulaValue);
            ChangeVariableBrick *brick = new ChangeVariableBrick(m_formulaTree, m_script);
            brick->SetVariable(m_variable);
            // TODO due to wrong conversion, no actual underflow
            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_defaultValue.c_str()));
            brick->Execute();
            Assert::AreEqual(atof(m_variable->GetValue().c_str()), atof(m_defaultValue.c_str()));
            //delete brick;
        }
	};
}