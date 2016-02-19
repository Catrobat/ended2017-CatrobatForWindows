using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Core.XmlModelConvertion
{
    #region Covert

    public class XmlModelConvertContextBase : XmlModelConvertContextBase<XmlProgram,
        XmlUserVariable, GlobalVariable,
        XmlSprite, Sprite,
        XmlScript, Script,
        XmlBrick, Brick>
    {
        #region Properties

        private readonly IDictionary<string, BroadcastMessage> _broadcastMessages;

        public IDictionary<string, BroadcastMessage> BroadcastMessages
        {
            get { return _broadcastMessages; }
        }

        #endregion

        public XmlModelConvertContextBase(XmlProgram program,
            ReadOnlyDictionary<XmlUserVariable, GlobalVariable> globalVariables)
            : base(program, globalVariables)
        {
            _broadcastMessages = new Dictionary<string, BroadcastMessage>();
        }
    }

    public class XmlModelConvertContext : XmlModelConvertContext<XmlModelConvertContextBase,
        XmlProgram,
        XmlUserVariable, Variable,
        XmlUserVariable, LocalVariable,
        XmlUserVariable, GlobalVariable,
        XmlLook, Look,
        XmlSound, Sound,
        XmlSprite, Sprite,
        XmlScript, Script,
        XmlBrick, Brick>
    {
        #region Properties

        public IDictionary<string, BroadcastMessage> BroadcastMessages
        {
            get { return BaseContext.BroadcastMessages; }
        }

        private readonly XmlFormulaConverter _formulaConverter;

        public XmlFormulaConverter FormulaConverter
        {
            get { return _formulaConverter; }
        }

        #endregion

        internal XmlModelConvertContext(
            XmlModelConvertContextBase contextBase,
            XmlSprite sprite,
            IReadOnlyDictionary<XmlLook, Look> looks,
            IReadOnlyDictionary<XmlSound, Sound> sounds,
            IReadOnlyDictionary<XmlUserVariable, LocalVariable> localVariables)
            : base(contextBase, sprite, looks, sounds, localVariables)
        {
            _formulaConverter = new XmlFormulaConverter(LocalVariables.Values, GlobalVariables.Values);
        }
    }

    #endregion

    #region ConvertBack

    public class XmlModelConvertBackContextBase : XmlModelConvertContextBase<
        Program,
        GlobalVariable, XmlUserVariable,
        Sprite, XmlSprite,
        Script, XmlScript,
        Brick, XmlBrick>
    {
        public XmlModelConvertBackContextBase(Program program,
            ReadOnlyDictionary<GlobalVariable, XmlUserVariable> globalVariables)
            : base(program, globalVariables)
        {
        }
    }

    public class XmlModelConvertBackContext : XmlModelConvertContext<XmlModelConvertBackContextBase,
        Program,
        Variable, XmlUserVariable,
        LocalVariable, XmlUserVariable,
        GlobalVariable, XmlUserVariable,
        Look, XmlLook,
        Sound, XmlSound,
        Sprite, XmlSprite,
        Script, XmlScript,
        Brick, XmlBrick>
    {

        #region Properties

        private readonly XmlFormulaConverter _formulaConverter;

        public XmlFormulaConverter FormulaConverter
        {
            get { return _formulaConverter; }
        }

        #endregion

        internal XmlModelConvertBackContext(
            XmlModelConvertBackContextBase contextBase,
            Sprite sprite,
            IReadOnlyDictionary<Look, XmlLook> looks,
            IReadOnlyDictionary<Sound, XmlSound> sounds,
            IReadOnlyDictionary<LocalVariable, XmlUserVariable> localVariables)
            : base(contextBase, sprite, looks, sounds, localVariables)
        {
            _formulaConverter = new XmlFormulaConverter();
        }
    }

    #endregion

    #region Helpers

    public abstract class XmlModelConvertContextBase<TSourceProgram,
        TSourceGlobalVariable, TTargetGlobalVariable,
        TSourceSprite, TTargetSprite,
        TSourceScript, TTargetScript,
        TSourceBrick, TTargetBrick>
        where TSourceSprite : class
        where TSourceScript : class
        where TSourceBrick : class
    {
        #region Properties

        private readonly TSourceProgram _program;

        public TSourceProgram Program
        {
            get { return _program; }
        }

        private readonly ReadOnlyDictionary<TSourceGlobalVariable, TTargetGlobalVariable> _globalVariables;

        public ReadOnlyDictionary<TSourceGlobalVariable, TTargetGlobalVariable> GlobalVariables
        {
            get { return _globalVariables; }
        }

        private readonly Dictionary<TSourceSprite, TTargetSprite> _sprites;

        public Dictionary<TSourceSprite, TTargetSprite> Sprites
        {
            get { return _sprites; }
        }

        private readonly Dictionary<TSourceScript, TTargetScript> _scripts;

        public Dictionary<TSourceScript, TTargetScript> Scripts
        {
            get { return _scripts; }
        }

        private readonly Dictionary<TSourceBrick, TTargetBrick> _bricks;

        public Dictionary<TSourceBrick, TTargetBrick> Bricks
        {
            get { return _bricks; }
        }

        #endregion

        protected XmlModelConvertContextBase(TSourceProgram program,
            ReadOnlyDictionary<TSourceGlobalVariable, TTargetGlobalVariable> globalVariables)
        {
            _program = program;
            _globalVariables = globalVariables;
            _sprites = new Dictionary<TSourceSprite, TTargetSprite>();
            _scripts = new Dictionary<TSourceScript, TTargetScript>();
            _bricks = new Dictionary<TSourceBrick, TTargetBrick>();
        }
    }

    public abstract class XmlModelConvertContext<TBaseContext,
        TSourceProgram,
        TSourceVariable, TTargetVariable,
        TSourceLocalVariable, TTargetLocalVariable,
        TSourceGlobalVariable, TTargetGlobalVariable,
        TSourceLook, TTargetLook,
        TSourceSound, TTargetSound,
        TSourceSprite, TTargetSprite,
        TSourceScript, TTargetScript,
        TSourceBrick, TTargetBrick>
        where TBaseContext :
            XmlModelConvertContextBase
                <TSourceProgram, TSourceGlobalVariable, TTargetGlobalVariable, TSourceSprite, TTargetSprite,
                    TSourceScript, TTargetScript, TSourceBrick, TTargetBrick>
        where TSourceLocalVariable : TSourceVariable
        where TSourceGlobalVariable : TSourceVariable
        where TTargetLocalVariable : TTargetVariable
        where TTargetGlobalVariable : TTargetVariable
        where TSourceSprite : class
        where TSourceScript : class
        where TSourceBrick : class
    {
        #region Properties

        //TODO: part of a dirty hack
        public Dictionary<TSourceVariable, TTargetVariable> variables { get; set; }

        protected readonly TBaseContext BaseContext;

        public TSourceProgram Program
        {
            get { return BaseContext.Program; }
        }

        private readonly TSourceSprite _sprite;

        public TSourceSprite Sprite
        {
            get { return _sprite; }
        }

        private readonly IReadOnlyDictionary<TSourceLocalVariable, TTargetLocalVariable> _localVariables;

        public IReadOnlyDictionary<TSourceLocalVariable, TTargetLocalVariable> LocalVariables
        {
            get { return _localVariables; }
        }

        public IReadOnlyDictionary<TSourceGlobalVariable, TTargetGlobalVariable> GlobalVariables
        {
            get { return BaseContext.GlobalVariables; }
        }

        private readonly IReadOnlyDictionary<TSourceVariable, TTargetVariable> _variables;

        public IReadOnlyDictionary<TSourceVariable, TTargetVariable> Variables
        {
            get { return _variables; }
        }

        private readonly IReadOnlyDictionary<TSourceLook, TTargetLook> _looks;

        public IReadOnlyDictionary<TSourceLook, TTargetLook> Looks
        {
            get { return _looks; }
        }

        private readonly IReadOnlyDictionary<TSourceSound, TTargetSound> _sounds;

        public IReadOnlyDictionary<TSourceSound, TTargetSound> Sounds
        {
            get { return _sounds; }
        }

        public IDictionary<TSourceSprite, TTargetSprite> Sprites
        {
            get { return BaseContext.Sprites; }
        }

        public IDictionary<TSourceScript, TTargetScript> Scripts
        {
            get { return BaseContext.Scripts; }
        }

        public IDictionary<TSourceBrick, TTargetBrick> Bricks
        {
            get { return BaseContext.Bricks; }
        }

        #endregion

        protected XmlModelConvertContext(
            TBaseContext baseContext,
            TSourceSprite sprite,
            IReadOnlyDictionary<TSourceLook, TTargetLook> looks,
            IReadOnlyDictionary<TSourceSound, TTargetSound> sounds,
            IReadOnlyDictionary<TSourceLocalVariable, TTargetLocalVariable> localVariables)
        {
            BaseContext = baseContext;
            _sprite = sprite;
            _looks = looks;
            _sounds = sounds;
            _localVariables = localVariables;
            variables = new Dictionary<TSourceVariable, TTargetVariable>();
            foreach (var entry in GlobalVariables) variables[entry.Key] = entry.Value;
            foreach (var entry in LocalVariables) variables[entry.Key] = entry.Value;
            _variables = new ReadOnlyDictionary<TSourceVariable, TTargetVariable>(variables);
        }
    }

    #endregion

}
