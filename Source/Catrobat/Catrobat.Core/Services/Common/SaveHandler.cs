using Catrobat.IDE.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Catrobat.Core.Services.Common
{
    public static class SaveHandler
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
        private static bool running = true;
        #endregion

        #region public Methods

        public static void addSaveJob(Program toSave)
        {
            SaveJob newJob = new SaveJob();
            newJob.ProgramToSave = toSave;

            lock (QueueLock)
            {
                _saveJobQueue.Enqueue(newJob);
            }

            // Wake up the Saving Task
            waitHandle.Set();
        }

        public static void StopSaveThread()
        {
            running = false;
        }

        #endregion

        #region private Methods
        public static async void Execute()
        {
            while (true)
            {
                while (_saveJobQueue != null && _saveJobQueue.Count > 0)
                {
                    SaveJob actualJob;

                    lock (QueueLock)
                    {
                        actualJob = _saveJobQueue.Dequeue();
                    }

                    try
                    {
                        await actualJob.ProgramToSave.Save();
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine("Could not Save, Exception: " + e.Message);
                        continue;
                    }
                }

                // To ensure that the thread will do all savejobs left before stopping
                if (!running)
                    break;

                waitHandle.WaitOne();
                waitHandle.Reset();
            }
        }

        public static Queue<SaveJob> GetQueue()
        {
            return _saveJobQueue;
        }
        #endregion
    }
}
