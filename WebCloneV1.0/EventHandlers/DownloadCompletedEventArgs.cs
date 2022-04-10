using System;
using System.Collections.Generic;
using System.Text;

namespace WebClone
{
    public class DownloadCompletedEventArgs : EventArgs
    {
        public DownloadCompletedEventArgs()
        { 
        }

        public DownloadCompletedEventArgs(object result, Exception error, bool cancelled)
        {
            this._result = result;
            this._error = error;
            this._cancelled = cancelled;
        }

        private object _result;

        public object Result
        {
            get { return _result; }
        }

        private bool _cancelled;

        public bool Cancelled
        {
            get { return _cancelled; }
        }

        private Exception _error;

        public Exception Error
        {
            get { return _error; }
        }
    }
}
