using System;

namespace Catrobat.IDE.Core.Utilities
{
    public class ProgressEventArgs : EventArgs
    {
        private int _percent;
        private string _operationGuid;

        public int Percent { get { return _percent; } }
        public string OperationGuid { get { return _operationGuid; } }

        public ProgressEventArgs(int percent, string operationGuid)
        {
            _percent = percent;
            _operationGuid = operationGuid;
        }
    }
}