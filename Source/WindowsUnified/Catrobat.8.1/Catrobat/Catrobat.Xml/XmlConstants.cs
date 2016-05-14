using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Catrobat.IDE.Core
{
    public static class XmlConstants
    {
        #region Application Info

        // Application Info
        public const string ApplicationName = "Pocket Code";

        public const string CurrentAppVersion = "0.20";

        public const int CurrentAppBuildNumber = 1;

        public const string CurrentAppBuildName = "0.20";

        #endregion

        #region Catrobat XML Version

        // Vatrobat XML Version

        public const double MinimumCodeVersion = 0.91;

        public static readonly ReadOnlyCollection<string> SupportedXMLVersions = new ReadOnlyCollection<string>
            (new List<String> { "0.93", "0.95", "0.97" });

        public const string TargetIDEVersion = "0.93";

        public const string TargetOutputVersion = "0.93";

        #endregion

        #region HeaderTags

        public const string ApplicationBuildName = "applicationBuildName";
        public const string ApplicationBuildNumber = "applicationBuildNumber";
        //Evt applicationName ändern??
        public const string ApplicationNameText = "applicationName";
        public const string ApplicationVersion = "applicationVersion";
        public const string CatrobatLanguageVersion = "catrobatLanguageVersion";
        public const string DateTimeUpload = "dateTimeUpload";
        public const string Description = "description";
        public const string DeviceName = "deviceName";
        public const string MediaLicense = "mediaLicense";
        public const string Platform = "platform";
        public const string PlatformVersion = "platformVersion";
        public const string ProgramLicense = "programLicense";
        public const string ProgramName = "programName";
        public const string RemixOf = "remixOf";
        public const string ScreenHeight = "screenHeight";
        public const string ScreenWidth = "screenWidth";
        public const string Tags = "tags";
        public const string Url = "url";
        public const string userHandle = "userHandle";

        #endregion

        #region Formula

        public const string TimesToRepeat = "TIMES_TO_REPEAT";//timesToRepeat
        public const string TimeToWaitInSeconds = "TIME_TO_WAIT_IN_SECONDS";//timeToWaitInSeconds 
        public const string Volume = "VOLUME";//volume
        public const string VolumeChange = "VOLUME_CHANGE";
        public const string VariableChange = "VARIABLE_CHANGE";//v91: variableFormula
        public const string Variable = "VARIABLE";//variableFormula
        public const string ChangeBrightness = "CHANGE_BRIGHTNESS";//changeBrightness
        public const string ChangeGhostEffect = "TRANSPARENCY_CHANGE";//changeGhostEffect
        public const string SizeChange = "SIZE_CHANGE"; //size
        public const string XPositionChange = "X_POSITION_CHANGE";//xMovement
        public const string YPositionChange = "Y_POSITION_CHANGE";//yMovement
        public const string XDestination = "X_DESTINATION";//xDestination
        public const string YDestination = "Y_DESTINATION";//yDestination
        public const string DurationInSeconds = "DURATION_IN_SECONDS";//durationInSeconds
        public const string Steps = "STEPS";//steps
        public const string XPosition = "X_POSITION";//xPosition
        public const string YPosition = "Y_POSITION";//yPosition
        public const string Degrees = "DEGREES";//degrees
        public const string Brightness = "BRIGHTNESS";//brightness
        public const string Transparency = "TRANSPARENCY";//transparency
        public const string Size = "SIZE";//size
        public const string TurnLeftDegrees = "TURN_LEFT_DEGREES"; //degrees
        public const string TurnRightDegrees = "TURN_RIGHT_DEGREES";//degrees
        public const string Note = "NOTE"; //note
        public const string Speak = "SPEAK"; //speak


        public const string FormulaList = "formulaList"; //new list containing the new formula type v91->v93
        public const string Formula = "formula"; //old formulatree - attention there was also a useless formulaclass which just called formula tree v91->v93
        public const string Category = "category";
        public const string LeftChild = "leftChild";
        public const string RightChild = "rightChild";
        public const string Value = "value";

        

        #endregion

        #region XmlObjects

        public const string ScriptList = "scriptList";

        public const string BrickList = "brickList";
        public const string Brick = "brick";       
        public const string Type = "type";

        public const string XmlNoteBrickType = "NoteBrick";

        public const string XmlLookListType = "lookList";
        public const string XmlLookType = "look";

        public const string XmlSoundList = "soundList";
        public const string XmlSoundType = "sound";
        
        public const string Program = "program";
        public const string Header = "header";
        public const string FileName = "fileName";
        public const string ObjectList = "objectList";

        #endregion
        
        #region XmlObjects-Bricks-ControlFlow

        public const string XmlBroadcastBrickType = "BroadcastBrick";
        public const string BroadcastMessage = "broadcastMessage";

        public const string XmlBroadcastWaitBrickType = "BroadcastWaitBrick";
        public const string XmlForeverBrickType = "ForeverBrick";
        public const string XmlLoopEndlessBrickType = "LoopEndlessBrick";

        public const string XmlIfLogicBeginBrick = "IfLogicBeginBrick";
        public const string XmlIfLogicElseBrick = "IfLogicElseBrick";
        public const string XmlIfLogicEndBrick = "IfLogicEndBrick";

        public const string XmlIFCONDITION = "IF_CONDITION";

        //public const string XmlLoopBeginBrick = "ForeverBrick";
        

        public const string XmlRepeatBrickType = "RepeatBrick";
        public const string XmlRepeatLoopEndBrickType = "LoopEndBrick";
        public const string XmlWaitBrickType = "WaitBrick";

        #endregion

        #region XmlObjects-Bricks-Looks

        public const string XmlNextLookBrickType = "NextLookBrick";
        public const string XmlSetLookBrickType = "SetLookBrick";
        public const string Look = "look";

        #endregion

        #region XmlObjects-Bricks-Nxt

        public const string XmlNxtMotorActionBrickType = "legoNxtMotorActionBrick";
        public const string XmlNxtMotorStopBrickType = "legoNxtMotorStopBrick";
        public const string XmlNextMotorTurnAngleBrickType = "legoNxtMotorTurnAngleBrick";
        public const string XmlNxtPlayToneBrickType = "legoNxtPlayToneBrick";
        
        public const string Motor = "motor";
        public const string Speed = "speed";
        
        public const string Frequency = "frequency";

        #endregion

        #region XmlObjects-Bricks-Properties

        public const string XmlChangeBrightnessBrickType = "ChangeBrightnessByNBrick";
        public const string XmlChangeGhostEffectBrickType = "ChangeGhostEffectByNBrick";
        public const string XmlChangeSizeByNBrickType = "ChangeSizeByNBrick";
        public const string XmlChangeXByBrickType = "ChangeXByNBrick";
        public const string XmlChangeYByBrickType = "ChangeYByNBrick";
        public const string XmlClearGraphicEffectBrickType = "ClearGraphicEffectBrick";
        public const string XmlComeToFrontBrickType = "ComeToFrontBrick";
        public const string XmlGlideToBrickType = "GlideToBrick";
        public const string XmlGoNStepsBackBrickType = "GoNBricksBackBrick";
        public const string XmlHideBrickType = "HideBrick";
        public const string XmlIfOnEdgeBounceBrickType = "IfOnEdgeBounceBrick";
        public const string XmlMoveNStepsBrickType = "MoveNStepsBrick";
        public const string XmlPlaceAtBrickType = "PlaceAtBrick";
        public const string XmlPointInDirectionBrickType = "PointToDirectionBrick";
        public const string XmlPointToBrickType = "PointToBrick";
        public const string PointedObject = "pointedObject";
        public const string XmlSetBrightnessBrickType = "SetBrightnessBrick";
        public const string XmlSetGhostEffectBrickType = "SetGhostEffectBrick";
        public const string XmlSetSizeToBrickType = "SetSizeToBrick";
        public const string XmlSetXBrickType = "SetXBrick";
        public const string XmlSetYBrickType = "SetYBrick";
        public const string XmlShowBrickType = "ShowBrick";
        public const string XmlTurnLeftBrickType = "TurnLeftBrick";
        public const string XmlTurnRightBrickType = "TurnRightBrick";

        #endregion

        #region XmlObjects-Bricks-Sounds

        public const string XmlChangeVolumeByBricksType = "ChangeVolumeByNBrick";

        public const string XmlPlaySoundBrickType = "PlaySoundBrick";
        public const string Sound = "sound";

        public const string XmlSetVolumeToBrickType = "SetVolumeToBrick";

        public const string XmlSpeakBrickType = "SpeakBrick";
        public const string Text = "text";

        public const string XmlStopAllSoundsBrickType = "StopAllSoundsBrick";

        #endregion

        #region XmlObjects-Bricks-Variables

        public const string XmlChangeVariableBrickType = "ChangeVariableBrick";
        public const string UserVariable = "userVariable";

        public const string XmlSetVariableBrickType = "SetVariableBrick";

        #endregion

        #region XmlObjects-Scripts

        public const string XmlBroadcastScriptType = "BroadcastScript";
        public const string ReceivedMessage = "receivedMessage";
        public const string XmlStartScriptType = "StartScript";
        public const string Script = "script";
        public const string XmlWhenScriptType = "WhenScript";
        public const string Action = "action";
        public const string Tapped = "Tapped";

        #endregion

        #region XmlObjects-Variables

        public const string Object = "object";
        public const string List = "list";
        public const string Entry = "entry";
        public const string XmlObjectVariableListType = "objectVariableList";
        public const string XmlProgramVariableListType = "programVariableList";
        public const string Name = "name";
        public const string Variables = "variables"; //for <0.94
        public const string Data = "data"; //for >=0.94 (the android team promised)
        public const string Reference = "reference";

        #endregion
    }
}
