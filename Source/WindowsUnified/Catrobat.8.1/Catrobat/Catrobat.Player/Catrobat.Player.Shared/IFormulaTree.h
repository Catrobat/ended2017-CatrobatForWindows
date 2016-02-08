#pragma once

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IFormulaTree
		{
		public:
			virtual property IFormulaTree^ RightChild;
			virtual property IFormulaTree^ LeftChild;
			virtual property Platform::String^ VariableType;
			virtual property Platform::String^ VariableValue;
		};
	}
}