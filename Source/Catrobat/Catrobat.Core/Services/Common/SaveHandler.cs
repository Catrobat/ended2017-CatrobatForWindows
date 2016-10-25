using Catrobat.IDE.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Catrobat.Core.Services.Common
{
    class SaveHandler
    {
        #region public Members
        public struct SaveJob
        {
            public Program ProgramToSave;
        }

        public static Object QueueLock = new Object();
        #endregion

        #region private Members

        private static Queue<SaveJob> _saveJobQueue = new Queue<SaveJob>();
        private static AutoResetEvent waitHandle = new AutoResetEvent(false);
        #endregion

        #region public Methods

        public static void addSaveJob(Program toSave)
        {
            SaveJob newJob = new SaveJob();
            newJob.ProgramToSave = toSave;

            lock(QueueLock)
            {
                _saveJobQueue.Enqueue(newJob);
            }

            // Wake up the Saving Task
            waitHandle.Set();
        }

        #endregion

        #region private Methods
        public static async void Execute()
        {
            while(true)
            {
                // be productive
                Debug.WriteLine("WORKING ON SAVE JOBS");

                while(_saveJobQueue != null && _saveJobQueue.Count > 0)
                {
                    SaveJob actualJob;

                    lock (QueueLock)
                    {
                        actualJob = _saveJobQueue.Dequeue();
                    }
                    
                    await actualJob.ProgramToSave.Save();
                }

                // sleep
                Debug.WriteLine("SLEEPING");
                waitHandle.WaitOne();
                waitHandle.Reset();
                Debug.WriteLine("GOING TO WORK AGAIN");
            }
        }
        #endregion
    }
}
