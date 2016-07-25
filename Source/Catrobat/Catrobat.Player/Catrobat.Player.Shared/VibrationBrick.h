#pragma once

#include "Brick.h"
#include "IVibrationBrick.h"

namespace ProjectStructure
{
    class VibrationBrick :
        public Brick
    {
    public:
        VibrationBrick(Catrobat_Player::NativeComponent::IVibrationBrick^ brick, Script* parent);
        void Execute();

    private: 
        std::shared_ptr<FormulaTree> m_vibrateDuration;
    };
}
