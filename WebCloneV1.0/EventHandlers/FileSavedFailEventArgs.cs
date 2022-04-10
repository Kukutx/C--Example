using System;
using System.Collections.Generic;
using System.Text;

namespace WebClone
{
    public class FileSavedFailEventArgs : EventArgs
    {
        public FileSavedFailEventArgs()
        { 
        }

        public FileSavedFailEventArgs(Exception ex)
        {
            this._error = ex;
        }

        private Exception _error;

        public Exception Error
        {
            get { return _error; }
        }
    }
}
