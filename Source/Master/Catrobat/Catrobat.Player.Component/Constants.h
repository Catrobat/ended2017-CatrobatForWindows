#pragma once

#include <string>

using namespace std;

namespace Constants
{
    namespace XMLParser
    {
        namespace Header
        {
            static const string ApplicationBuildName        =       "applicationBuildName";
            static const string ApplicationBuildNumber      =       "applicationBuildNumber";
            static const string ApplicationName             =       "applicationName";
            static const string ApplicationVersion          =       "applicationVersion";
            static const string CatrobatLanguageVersion     =       "catrobatLanguageVersion";
            static const string DateTimeUpload              =       "dateTimeUpload";
            static const string Description                 =       "description";
            static const string DeviceName                  =       "deviceName";
            static const string MediaLicense                =       "mediaLicense";
            static const string Platform                    =       "platform";
            static const string PlatformVersion             =       "platformVersion";
            static const string ProgramLicense              =       "programLicense";
            static const string ProgramName                 =       "programName";
            static const string RemixOf                     =       "remixOf";
            static const string ScreenHeight                =       "screenHeight";
            static const string ScreenWidth                 =       "screenWidth";
            static const string Tags                        =       "tags";
            static const string Url                         =       "url";
            static const string UserHandle                  =       "userHandle";
            static const string Header                      =       "header";
            static const string Program                     =       "program";
        };

        namespace Object
        {
            static const string Reference                   =       "reference";
            static const string Object                      =       "object";
            static const string Look                        =       "look";
            static const string BrickList                   =       "brickList";
            static const string Action                      =       "action";
            static const string Name                        =       "name";
            static const string ScriptList                  =       "scriptList";
            static const string LookList                    =       "lookList";
            static const string SoundList                   =       "soundList";
            static const string ObjectList                  =       "objectList";
        }

        namespace Look
        {
            static const string FileName                    =       "fileName";
            static const string Name                        =       "name";
        }

        namespace Formula
        {
            static const string FormulaTree                 =       "formulaTree";
            static const string LeftChild                   =       "leftChild";
            static const string RightChild                  =       "rightChild";
            static const string Value                       =       "value";
            static const string Type                        =       "type";
            static const string Name                        =       "name";
            static const string UserVariable                =       "userVariable";
            static const string ProgramVariableList         =       "programVariableList";
            static const string List                        =       "list";
            static const string Entry                       =       "entry";
            static const string Variables                   =       "variables";
            static const string VariableFormula             =       "variableFormula";
            static const string ObjectVariableList          =       "objectVariableList";
            static const string True                        =       "true";
        }

        namespace Script
        {
            static const string ReceivedMessage             =       "receivedMessage";            
            static const string StartScript                 =       "startScript";
            static const string WhenScript                  =       "whenScript";
            static const string BroadcastScript             =       "broadcastScript";
        }

        namespace Brick
        {
            static const string SetLookBrick                =       "setLookBrick";
            static const string WaitBrick                   =       "waitBrick";
            static const string PlaceAtBrick                =       "placeAtBrick";
            static const string SetGhostEffectBrick         =       "setGhostEffectBrick";
            static const string PlaySoundBrick              =       "playSoundBrick";
            static const string GlideToBrick                =       "glideToBrick";
            static const string BroadcastBrick              =       "broadcastBrick";
            static const string HideBrick                   =       "hideBrick";
            static const string ShowBrick                   =       "showBrick";
            static const string IfLogicBeginBrick           =        "ifLogicBeginBrick";
            static const string IfLogicElseBrick            =       "ifLogicElseBrick";
            static const string IfLogicEndBrick             =       "ifLogicEndBrick";
            static const string ForeverBrick                =       "foreverBrick";
            static const string LoopEndlessBrick            =       "loopEndlessBrick";
            static const string RepeatBrick                 =       "repeatBrick";
            static const string LoopEndBrick                =       "loopEndBrick";
            static const string SetVariableBrick            =       "setVariableBrick";
            static const string ChangeVariableBrick         =       "changeVariableBrick";
            static const string ChangeGhostEffectByNBrick   =       "changeGhostEffectByNBrick";
            static const string SetSizeToBrick              =       "setSizeToBrick";
            static const string ChangeSizeByNBrick          =       "changeSizeByNBrick";
            static const string NextLookBrick               =       "nextLookBrick";
            static const string SetXBrick                   =       "setXBrick";
            static const string SetYBrick                   =       "setYBrick";
            static const string ChangeXByNBrick             =       "changeXByNBrick";
            static const string ChangeYByNBrick             =       "changeYByNBrick";
            static const string PointInDirectionBrick       =       "pointInDirectionBrick";
            static const string TurnLeftBrick               =       "turnLeftBrick";
            static const string TurnRightBrick              =       "turnRightBrick";
            static const string ChangeGhostEffect           =       "changeGhostEffect";
            static const string TimeToWaitInSeconds         =       "timeToWaitInSeconds";
            static const string Size                        =       "size";
            static const string XPosition                   =       "xPosition";
            static const string YPosition                   =       "yPosition";
            static const string XMovement                   =       "xMovement";
            static const string YMovement                   =       "yMovement";
            static const string Degrees                     =       "degrees";
            static const string XDestination                =       "xDestination";
            static const string YDestination                =       "yDestination";
            static const string DurationInSeconds      =            "durationInSeconds";
            static const string Transparency                =       "transparency";
            static const string BroadcastMessage            =       "broadcastMessage";
            static const string Sound                       =       "sound";
            static const string FileName                    =       "fileName";
            static const string Name                        =       "name";
            static const string TimesToRepeat               =       "timesToRepeat";
            static const string IfCondition                 =       "ifCondition";
        }
    };

    namespace ErrorMessage
    {
         static const string Missing                        =       " missing";
    };

	namespace Player
	{
		static const string xmlFileName						=		"code.xml";
	};
};