#pragma once

#include "Brick.h"
#include "IMoveNStepsBrick.h"

namespace ProjectStructure
{
	class MoveNStepsBrick :
		public Brick
	{
	public:
		MoveNStepsBrick(Catrobat_Player::NativeComponent::IMoveNStepsBrick^ brick, Script* parent);
		virtual ~MoveNStepsBrick();
		void	Execute();

	private:
		std::shared_ptr<FormulaTree> m_steps;
		void CalculateNewCoordinates(float &x, float &y);
	};

}