#pragma once

#include <string>

namespace Constants
{
    namespace XMLParser
    {
        namespace Header
        {
            static const std::string ApplicationBuildName        =       "applicationBuildName";
            static const std::string ApplicationBuildNumber      =       "applicationBuildNumber";
            static const std::string ApplicationName             =       "applicationName";
            static const std::string ApplicationVersion          =       "applicationVersion";
            static const std::string CatrobatLanguageVersion     =       "catrobatLanguageVersion";
            static const std::string DateTimeUpload              =       "dateTimeUpload";
            static const std::string Description                 =       "description";
            static const std::string DeviceName                  =       "deviceName";
            static const std::string MediaLicense                =       "mediaLicense";
            static const std::string Platform                    =       "platform";
            static const std::string PlatformVersion             =       "platformVersion";
            static const std::string ProgramLicense              =       "programLicense";
            static const std::string ProgramName                 =       "programName";
            static const std::string RemixOf                     =       "remixOf";
            static const std::string ScreenHeight                =       "screenHeight";
            static const std::string ScreenWidth                 =       "screenWidth";
            static const std::string Tags                        =       "tags";
            static const std::string Url                         =       "url";
            static const std::string UserHandle                  =       "userHandle";
            static const std::string Header                      =       "header";
            static const std::string Program                     =       "program";
        };

        namespace Object
        {
            static const std::string Reference                   =       "reference";
            static const std::string Object                      =       "object";
            static const std::string Look                        =       "look";
            static const std::string BrickList                   =       "brickList";
            static const std::string Action                      =       "action";
            static const std::string Name                        =       "name";
            static const std::string ScriptList                  =       "scriptList";
            static const std::string LookList                    =       "lookList";
            static const std::string SoundList                   =       "soundList";
            static const std::string ObjectList                  =       "objectList";
        }

        namespace Look
        {
            static const std::string FileName                    =       "fileName";
            static const std::string Name                        =       "name";
        }

        namespace Formula
        {
            static const std::string FormulaTree                 =       "formulaTree";
            static const std::string LeftChild                   =       "leftChild";
            static const std::string RightChild                  =       "rightChild";
            static const std::string Value                       =       "value";
            static const std::string Type                        =       "type";
            static const std::string Name                        =       "name";
            static const std::string UserVariable                =       "userVariable";
            static const std::string ProgramVariableList         =       "programVariableList";
            static const std::string List                        =       "list";
            static const std::string Entry                       =       "entry";
            static const std::string Variables                   =       "variables";
            static const std::string VariableFormula             =       "variableFormula";
            static const std::string ObjectVariableList          =       "objectVariableList";
            static const std::string True                        =       "true";
        }

        namespace Script
        {
            static const std::string ReceivedMessage             =       "receivedMessage";            
            static const std::string StartScript                 =       "startScript";
            static const std::string WhenScript                  =       "whenScript";
            static const std::string BroadcastScript             =       "broadcastScript";
        }

        namespace Brick
        {
            static const std::string SetLookBrick                =       "setLookBrick";
            static const std::string WaitBrick                   =       "waitBrick";
            static const std::string PlaceAtBrick                =       "placeAtBrick";
            static const std::string SetGhostEffectBrick         =       "setGhostEffectBrick";
            static const std::string PlaySoundBrick              =       "playSoundBrick";
            static const std::string GlideToBrick                =       "glideToBrick";
            static const std::string BroadcastBrick              =       "broadcastBrick";
            static const std::string HideBrick                   =       "hideBrick";
            static const std::string ShowBrick                   =       "showBrick";
            static const std::string IfLogicBeginBrick           =        "ifLogicBeginBrick";
            static const std::string IfLogicElseBrick            =       "ifLogicElseBrick";
            static const std::string IfLogicEndBrick             =       "ifLogicEndBrick";
            static const std::string ForeverBrick                =       "foreverBrick";
            static const std::string LoopEndlessBrick            =       "loopEndlessBrick";
            static const std::string RepeatBrick                 =       "repeatBrick";
            static const std::string LoopEndBrick                =       "loopEndBrick";
            static const std::string SetVariableBrick            =       "setVariableBrick";
            static const std::string ChangeVariableBrick         =       "changeVariableBrick";
            static const std::string ChangeGhostEffectByNBrick   =       "changeGhostEffectByNBrick";
            static const std::string SetSizeToBrick              =       "setSizeToBrick";
            static const std::string ChangeSizeByNBrick          =       "changeSizeByNBrick";
            static const std::string NextLookBrick               =       "nextLookBrick";
            static const std::string SetXBrick                   =       "setXBrick";
            static const std::string SetYBrick                   =       "setYBrick";
            static const std::string ChangeXByNBrick             =       "changeXByNBrick";
            static const std::string ChangeYByNBrick             =       "changeYByNBrick";
            static const std::string PointInDirectionBrick       =       "pointInDirectionBrick";
            static const std::string TurnLeftBrick               =       "turnLeftBrick";
            static const std::string TurnRightBrick              =       "turnRightBrick";
			static const std::string MoveNStepsBrick				=		"moveNStepsBrick";
            static const std::string ChangeGhostEffect           =       "changeGhostEffect";
            static const std::string TimeToWaitInSeconds         =       "timeToWaitInSeconds";
            static const std::string Size                        =       "size";
            static const std::string XPosition                   =       "xPosition";
            static const std::string YPosition                   =       "yPosition";
            static const std::string XMovement                   =       "xMovement";
            static const std::string YMovement                   =       "yMovement";
            static const std::string Degrees                     =       "degrees";
			static const std::string Steps						=		"steps";
            static const std::string XDestination                =       "xDestination";
            static const std::string YDestination                =       "yDestination";
            static const std::string DurationInSeconds			=		"durationInSeconds";
            static const std::string Transparency                =       "transparency";
            static const std::string BroadcastMessage            =       "broadcastMessage";
            static const std::string Sound                       =       "sound";
            static const std::string FileName                    =       "fileName";
            static const std::string Name                        =       "name";
            static const std::string TimesToRepeat               =       "timesToRepeat";
            static const std::string IfCondition                 =       "ifCondition";
        }
    };

    namespace ErrorMessage
    {
         static const std::string Missing                        =       " missing";
    };

	namespace Player
	{
		static const std::string xmlFileName						=		"code.xml";
	};
};