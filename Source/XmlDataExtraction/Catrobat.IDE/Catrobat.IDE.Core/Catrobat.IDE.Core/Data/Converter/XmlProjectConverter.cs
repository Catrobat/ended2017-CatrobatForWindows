using System.Collections.Generic;
using System.Collections.ObjectModel;
using Catrobat.Data.Xml.XmlObjects;
using Catrobat.Data.Xml.XmlObjects.Bricks;
using Catrobat.Data.Xml.XmlObjects.Scripts;
using Catrobat.Data.Xml.XmlObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;


namespace Catrobat.IDE.Core.Xml.Converter
{
    public class XmlProjectConverter
    {
        #region Covert

        public class ConvertContextBase : ContextBase<XmlProject, 
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

            public ConvertContextBase(XmlProject project, ReadOnlyDictionary<XmlUserVariable, GlobalVariable> globalVariables)
                : base(project, globalVariables)
            {
                _broadcastMessages = new Dictionary<string, BroadcastMessage>();
            }
        }

        public class ConvertContext : Context<ConvertContextBase, 
            XmlProject, 
            XmlUserVariable, Variable,
            XmlUserVariable, LocalVariable,
            XmlUserVariable, GlobalVariable,
            XmlCostume, Costume, 
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

            internal ConvertContext(
                ConvertContextBase contextBase,
                XmlSprite sprite,
                IReadOnlyDictionary<XmlCostume, Costume> costumes,
                IReadOnlyDictionary<XmlSound, Sound> sounds,
                IReadOnlyDictionary<XmlUserVariable, LocalVariable> localVariables)
                : base(contextBase, sprite, costumes, sounds, localVariables)
            {
                _formulaConverter = new XmlFormulaConverter(LocalVariables.Values, GlobalVariables.Values);
            }
        }

        public Project Convert(XmlProject project)
        {
            return project == null ? null : project.ToModel();
        }

        #endregion

        #region ConvertBack

        public class ConvertBackContextBase : ContextBase<
            Project, 
            GlobalVariable, XmlUserVariable,
            Sprite, XmlSprite,
            Script, XmlScript,
            Brick, XmlBrick>
        {
            public ConvertBackContextBase(Project project, ReadOnlyDictionary<GlobalVariable, XmlUserVariable> globalVariables)
                : base(project, globalVariables)
            {
            }
        }

        public class ConvertBackContext : Context<ConvertBackContextBase, 
            Project, 
            Variable, XmlUserVariable,
            LocalVariable, XmlUserVariable,
            GlobalVariable, XmlUserVariable,
            Costume, XmlCostume, 
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

            internal ConvertBackContext(
                ConvertBackContextBase contextBase,
                Sprite sprite,
                IReadOnlyDictionary<Costume, XmlCostume> costumes, 
                IReadOnlyDictionary<Sound, XmlSound> sounds, 
                IReadOnlyDictionary<LocalVariable, XmlUserVariable> localVariables)
                : base(contextBase, sprite, costumes, sounds, localVariables)
            {
                _formulaConverter = new XmlFormulaConverter();
            }
        }

        public XmlProject ConvertBack(Project project)
        {
            return project == null ? null : project.ToXmlObject();
        }

        #endregion

        #region Helpers

        public abstract class ContextBase<TSourceProject, 
            TSourceGlobalVariable, TTargetGlobalVariable,
            TSourceSprite, TTargetSprite,
            TSourceScript, TTargetScript,
            TSourceBrick, TTargetBrick>
            where TSourceSprite : class
            where TSourceScript : class
            where TSourceBrick : class
        {
            #region Properties

            private readonly TSourceProject _project;
            public TSourceProject Project
            {
                get { return _project; }
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

            protected ContextBase(TSourceProject project, ReadOnlyDictionary<TSourceGlobalVariable, TTargetGlobalVariable> globalVariables)
            {
                _project = project;
                _globalVariables = globalVariables;
                _sprites = new Dictionary<TSourceSprite, TTargetSprite>();
                _scripts = new Dictionary<TSourceScript, TTargetScript>();
                _bricks = new Dictionary<TSourceBrick, TTargetBrick>();
            }
        }

        public abstract class Context<TBaseContext, 
            TSourceProject, 
            TSourceVariable, TTargetVariable,
            TSourceLocalVariable, TTargetLocalVariable,
            TSourceGlobalVariable, TTargetGlobalVariable,
            TSourceCostume, TTargetCostume,
            TSourceSound, TTargetSound,
            TSourceSprite, TTargetSprite,
            TSourceScript, TTargetScript,
            TSourceBrick, TTargetBrick>
            where TBaseContext : ContextBase<TSourceProject, TSourceGlobalVariable, TTargetGlobalVariable, TSourceSprite, TTargetSprite, TSourceScript, TTargetScript, TSourceBrick, TTargetBrick>
            where TSourceLocalVariable : TSourceVariable
            where TSourceGlobalVariable : TSourceVariable
            where TTargetLocalVariable : TTargetVariable
            where TTargetGlobalVariable : TTargetVariable
            where TSourceSprite : class
            where TSourceScript : class
            where TSourceBrick : class
        {
            #region Properties

            protected readonly TBaseContext BaseContext;

            public TSourceProject Project
            {
                get { return BaseContext.Project; }
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

            private readonly IReadOnlyDictionary<TSourceCostume, TTargetCostume> _costumes;
            public IReadOnlyDictionary<TSourceCostume, TTargetCostume> Costumes
            {
                get { return _costumes; }
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

            internal Context(
                TBaseContext baseContext,
                TSourceSprite sprite,
                IReadOnlyDictionary<TSourceCostume, TTargetCostume> costumes,
                IReadOnlyDictionary<TSourceSound, TTargetSound> sounds,
                IReadOnlyDictionary<TSourceLocalVariable, TTargetLocalVariable> localVariables)
            {
                BaseContext = baseContext;
                _sprite = sprite;
                _costumes = costumes;
                _sounds = sounds;
                _localVariables = localVariables;
                var variables = new Dictionary<TSourceVariable, TTargetVariable>();
                foreach (var entry in GlobalVariables) variables[entry.Key] = entry.Value;
                foreach (var entry in LocalVariables) variables[entry.Key] = entry.Value;
                _variables = new ReadOnlyDictionary<TSourceVariable, TTargetVariable>(variables);
            }
        }

        #endregion
    }
}
