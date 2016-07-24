#pragma once

#include "IBrick.h"

namespace Catrobat_Player
{
    namespace NativeComponent
    {
        public interface class IVibrationBrick : public IBrick
        {
            public:
                virtual property IFormulaTree^ VibrateDuration;
        };
    }
}
