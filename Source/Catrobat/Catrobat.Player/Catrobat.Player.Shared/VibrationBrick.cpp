#include "pch.h"
#include "VibrationBrick.h"
#include "Interpreter.h"

using namespace std;
using namespace ProjectStructure;
using namespace Windows::Phone::Devices::Notification;
using namespace Windows::Foundation;

VibrationBrick::VibrationBrick(Catrobat_Player::NativeComponent::IVibrationBrick^ brick, Script* parent) :
    Brick(TypeOfBrick::VibrationBrick, parent),
    m_vibrateDuration(make_shared<FormulaTree>(brick->VibrateDuration))
{
}

void VibrationBrick::Execute()
{
    auto duration = Interpreter::Instance()->EvaluateFormulaToFloat(m_vibrateDuration, m_parent->GetParent());
    if (duration < 0 || duration > 5.0f) return;
    TimeSpan ts;
    ts.Duration = duration;
    VibrationDevice::GetDefault()->Vibrate(ts);
}
