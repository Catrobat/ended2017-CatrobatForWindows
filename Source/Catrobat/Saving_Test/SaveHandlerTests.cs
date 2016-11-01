using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Saving_Test
{
    [TestClass]
    public class SaveHandlerTests
    {
        [TestMethod]
        public void SaveHandlerQueueTest()
        {
            Catrobat.IDE.Core.Models.Program testprog = new Catrobat.IDE.Core.Models.Program();
            Catrobat.Core.Services.Common.SaveHandler.addSaveJob(testprog);
            Catrobat.Core.Services.Common.SaveHandler.addSaveJob(testprog);
            Queue<Catrobat.Core.Services.Common.SaveHandler.SaveJob> testqueue = Catrobat.Core.Services.Common.SaveHandler.GetQueue();

            Assert.IsTrue(testqueue.Count == 2);
        }

        [TestMethod]
        public void SaveHandlerSaveCollisionTest()
        {
            Catrobat.IDE.Core.App.InitializeSaveHandler();
            Catrobat.IDE.Core.Models.Program testprog = new Catrobat.IDE.Core.Models.Program();
            Catrobat.IDE.Core.Models.Program testprog_ref = new Catrobat.IDE.Core.Models.Program();

            for (int i = 0; i < 10; i++)
            {
                Catrobat.IDE.Core.Models.Sprite testsprite = new Catrobat.IDE.Core.Models.Sprite();
                testprog.Sprites.Add(testsprite);
                testprog_ref.Sprites.Add(testsprite);
            }

            // not properly working when both is used but working individually

            // let save thread save (async)
            Catrobat.Core.Services.Common.SaveHandler.addSaveJob(testprog);
            // let actual thread save
            testprog.Save();

            // problem => both are probably saving at the same time, disturbing each other


            for (int i = 0; i < 10; i++)
            {
                Assert.IsNotNull(testprog.Sprites[i]);
                Assert.IsNotNull(testprog_ref.Sprites[i]);
                Assert.AreEqual(testprog.Sprites[i], testprog_ref.Sprites[i]);
            }
        }
        [TestMethod]
        public void SaveHandlerStopTest()
        {
            var SaveHandlerTask = new Task(Catrobat.Core.Services.Common.SaveHandler.Execute);
            SaveHandlerTask.Start();

            // check if running

            Debug.WriteLine("Running?: " + SaveHandlerTask.Status);

            Assert.AreEqual(TaskStatus.Running, SaveHandlerTask.Status);

            // stop

            Catrobat.IDE.Core.App.StopSaveThread();

            // lazy polling but it works
            while (SaveHandlerTask.Status == TaskStatus.Running)
            {}

            Assert.AreNotEqual(TaskStatus.Running, SaveHandlerTask.Status);
        }
    }
}
