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
			virtual property int Type;
			virtual property Platform::String^ Value;
			virtual property int Operator;
			virtual property int Function;
			virtual property int Sensor;
		};
	}
}