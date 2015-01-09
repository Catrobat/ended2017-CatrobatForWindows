using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc;
using Catrobat.IDE.Core.Tests.SampleData.ProgramGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Tests.Extensions;

namespace Catrobat.IDE.Core.Tests.Tests.Data
{
    [TestClass]
    public class ProgramGeneratorWhackAMoleTests
    {
        [TestMethod, TestCategory("Data")]
        public async Task ProgramGeneratorWhackAMole_ReferenceTest()
        {
            var programGenerator = new ProgramGeneratorWhackAMole();

            var program = await programGenerator.GenerateProgram("TestProgram", false);

            Assert.AreEqual(5, program.Sprites.Count);

            for (var moleIndex = 1; moleIndex < 5; moleIndex ++)
            {
                var foreverBegin = program.Sprites[moleIndex].Scripts[0].Bricks.FirstOrDefault(
                    a => a is ForeverBrick) as ForeverBrick;
                var foreverEnd = program.Sprites[moleIndex].Scripts[0].Bricks.FirstOrDefault(
                    a => a is EndForeverBrick) as EndForeverBrick;

                Assert.IsNotNull(foreverBegin);
                Assert.IsNotNull(foreverEnd);

                Assert.AreEqual(foreverBegin.End, foreverEnd);
                Assert.AreEqual(foreverEnd.Begin, foreverBegin);
            }
        }

        [TestMethod, TestCategory("Data")]
        public async Task ProgramGeneratorWhackAMole_SimilarityTest()
        {
            var programGenerator = new ProgramGeneratorWhackAMole();

            var program = await programGenerator.GenerateProgram("TestProgram", false);

            Assert.AreEqual(5, program.Sprites.Count);

            for (var moleIndex = 2; moleIndex < 5; moleIndex++)
            {
                var mole1 = program.Sprites[1];

                for (var scriptIndex = 0; scriptIndex < mole1.Scripts.Count; scriptIndex++)
                {
                    var mole1Script = mole1.Scripts[scriptIndex];
                    for (var brickIndex = 0; brickIndex < mole1Script.Bricks.Count; brickIndex++)
                    {
                        var brick = program.Sprites[moleIndex].Scripts[scriptIndex].Bricks[brickIndex];

                        Assert.IsInstanceOfType(brick, mole1Script.Bricks[brickIndex].GetType());
                    }
                }
            }
        }
    }
}
