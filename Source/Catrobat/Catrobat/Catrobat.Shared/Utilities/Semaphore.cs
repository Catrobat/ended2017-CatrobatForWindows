using System;
using System.Diagnostics;
using System.Threading;

namespace Catrobat.IDE.WindowsShared.Utilities
{
    public abstract class BWaitHandle
    {
        protected abstract void OnSuccessfullWait();
        public abstract bool WaitOne();
        public abstract bool WaitOne(TimeSpan timeout);
        public abstract bool WaitOne(int millisecondsTimeout);

        internal abstract WaitHandle WaitHandle { get; }

        static WaitHandle[] ToWaitHandle(BWaitHandle[] waitHandles)
        {
            int n = waitHandles.Length;
            WaitHandle[] wh = new WaitHandle[n];

            for (int i = 0; i < n; ++i)
                wh[i] = waitHandles[i].WaitHandle;

            return wh;
        }

        public static int WaitAny(BWaitHandle[] waitHandles)
        {
            WaitHandle[] wh = ToWaitHandle(waitHandles);
            var res = WaitHandle.WaitAny(wh);
            if (res >= 0)
                waitHandles[res].OnSuccessfullWait();
            return res;
        }

        public static int WaitAny(BWaitHandle[] waitHandles, int millisecondsTimeout)
        {
            WaitHandle[] wh = ToWaitHandle(waitHandles);
            var res = WaitHandle.WaitAny(wh, millisecondsTimeout);
            if (res >= 0)
                waitHandles[res].OnSuccessfullWait();
            return res;
        }

        public static int WaitAny(BWaitHandle[] waitHandles, TimeSpan timeout)
        {
            WaitHandle[] wh = ToWaitHandle(waitHandles);
            var res = WaitHandle.WaitAny(wh, timeout);
            if (res >= 0)
                waitHandles[res].OnSuccessfullWait();
            return res;
        }

        public static int WaitAll(BWaitHandle[] waitHandles)
        {
            throw new NotImplementedException();
        }

        public static int WaitAll(BWaitHandle[] waitHandles, int millisecondsTimeout)
        {
            throw new NotImplementedException();
        }

        public static int WaitAll(BWaitHandle[] waitHandles, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }
    }


    public class BManualResetEvent : BWaitHandle, IDisposable
    {
        ManualResetEvent _mre;

        public BManualResetEvent(bool initialState)
        {
            _mre = new ManualResetEvent(initialState);
        }

        // Summary:
        //     Sets the state of the event to non-signaled, which causes threads to block.
        //
        // Returns:
        //     true if the operation succeeds; otherwise, false.
        public bool Reset()
        {
            return _mre.Reset();
        }
        //
        // Summary:
        //     Sets the state of the event to signaled, which allows one or more waiting
        //     threads to proceed.
        //
        // Returns:
        //     true if the operation succeeds; otherwise, false.
        public bool Set()
        {
            return _mre.Set();
        }

        protected override void OnSuccessfullWait()
        {
            // nothing special needed
        }

        public override bool WaitOne()
        {
            return _mre.WaitOne();
        }

        public override bool WaitOne(TimeSpan timeout)
        {
            return _mre.WaitOne(timeout);
        }

        public override bool WaitOne(int millisecondsTimeout)
        {
            return _mre.WaitOne(millisecondsTimeout);
        }

        internal override WaitHandle WaitHandle
        {
            get { return _mre; }
        }

        public void Dispose()
        {
            if (_mre != null)
            {
                _mre.Dispose();
                _mre = null;
            }
        }
    }


    public class BAutoResetEvent : BWaitHandle, IDisposable
    {
        AutoResetEvent _are;

        public BAutoResetEvent(bool initialState)
        {
            _are = new AutoResetEvent(initialState);
        }

        // Summary:
        //     Sets the state of the event to non-signaled, which causes threads to block.
        //
        // Returns:
        //     true if the operation succeeds; otherwise, false.
        public bool Reset()
        {
            return _are.Reset();
        }
        //
        // Summary:
        //     Sets the state of the event to signaled, which allows one or more waiting
        //     threads to proceed.
        //
        // Returns:
        //     true if the operation succeeds; otherwise, false.
        public bool Set()
        {
            return _are.Set();
        }

        protected override void OnSuccessfullWait()
        {
            // nothing special needed
        }

        public override bool WaitOne()
        {
            throw new NotImplementedException();
        }

        public override bool WaitOne(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public override bool WaitOne(int millisecondsTimeout)
        {
            throw new NotImplementedException();
        }

        internal override WaitHandle WaitHandle
        {
            get { return _are; }
        }

        public void Dispose()
        {
            if (_are != null)
            {
                _are.Dispose();
                _are = null;
            }
        }
    }

    ////////////////////////////////////////////////////////////////
    // Updated semaphore
    ////////////////////////////////////////////////////////////////
    public class BSemaphore : BWaitHandle, IDisposable
    {
        int _count = 0;
        int _maxCount = int.MaxValue;
        EventWaitHandle _ewh;

        public BSemaphore()
        {
            _ewh = new AutoResetEvent(false);
        }

        public BSemaphore(int initialCount, int maxCount)
        {
            if (initialCount < 0)
                throw new ArgumentException("Semaphore value should be >= 0.");
            if (initialCount >= maxCount)
                throw new ArgumentException();

            _count = initialCount;
            _maxCount = maxCount;
            _ewh = new AutoResetEvent(_count > 0);
        }

        protected override void OnSuccessfullWait()
        {
            var res = Interlocked.Decrement(ref _count);
            Debug.Assert(res >= 0, "The decremented value should be always >= 0.");
            if (res > 0)
                _ewh.Set();
        }

        public override bool WaitOne()
        {
            _ewh.WaitOne();
            OnSuccessfullWait();
            return true;
        }

        public override bool WaitOne(TimeSpan timeout)
        {
            if (_ewh.WaitOne(timeout))
            {
                OnSuccessfullWait();
                return true;
            }
            else
                return false;
        }

        public override bool WaitOne(int millisecondsTimeout)
        {
            if (_ewh.WaitOne(millisecondsTimeout))
            {
                OnSuccessfullWait();
                return true;
            }
            else
                return false;
        }

        public void Release()
        {
            var res = Interlocked.Increment(ref _count);
            if (res > _maxCount)
                throw new ArgumentException("The value of Semaphore is bigger than predefined maxValue.");

            if (res == 1)
                _ewh.Set();
        }

        public void Dispose()
        {
            if (_ewh != null)
            {
                _ewh.Dispose();
                _ewh = null;
            }
        }

        internal override WaitHandle WaitHandle
        {
            get { return _ewh; }
        }
    }
}
