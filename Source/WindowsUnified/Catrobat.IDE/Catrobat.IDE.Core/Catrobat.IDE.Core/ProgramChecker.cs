using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;

namespace Catrobat.IDE.Core
{
    public static class ProgramChecker
    {
        public static void CheckProgram(Program program)
        {
            const int moleIndex = 2;


                var foreverBegin = program.Sprites[moleIndex].Scripts[0].Bricks.FirstOrDefault(
                    a => a is ForeverBrick) as ForeverBrick;
                var foreverEnd = program.Sprites[moleIndex].Scripts[0].Bricks.FirstOrDefault(
                    a => a is EndForeverBrick) as EndForeverBrick;

                if (foreverBegin == null)
                    Debugger.Break();

                if (foreverEnd == null)
                    Debugger.Break();

                if (foreverBegin.End != foreverEnd)
                    Debugger.Break();

                if (foreverEnd.Begin != foreverBegin)
                    Debugger.Break();
        }

        public static void CheckProgram(XmlProgram program)
        {
            const int moleIndex = 2;


            var foreverBegin = program.SpriteList.Sprites[moleIndex].Scripts.Scripts[0].Bricks.Bricks.FirstOrDefault(
                a => a is XmlForeverBrick) as XmlForeverBrick;
            var foreverEnd = program.SpriteList.Sprites[moleIndex].Scripts.Scripts[0].Bricks.Bricks.FirstOrDefault(
                a => a is XmlForeverLoopEndBrick) as XmlForeverLoopEndBrick;

            if (foreverBegin == null)
                Debugger.Break();

            if (foreverEnd == null)
                Debugger.Break();

            if (foreverBegin.LoopEndBrick != foreverEnd)
                Debugger.Break();

            if (foreverEnd.LoopBeginBrick != foreverBegin)
                Debugger.Break();
        }
    }
}
