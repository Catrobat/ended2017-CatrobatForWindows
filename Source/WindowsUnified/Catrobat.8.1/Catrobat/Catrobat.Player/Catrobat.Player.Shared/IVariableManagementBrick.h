#pragma once

#include "IBrick.h"
#include "IFormulaTree.h"
#include "IUserVariable.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IVariableManagementBrick : public IBrick
		{
			virtual property IUserVariable^ Variable;
			virtual property IFormulaTree^ VariableFormula;
		};
	}
}
