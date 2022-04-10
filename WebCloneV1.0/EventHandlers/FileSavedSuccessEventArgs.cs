using System;
using System.Collections.Generic;
using System.Text;

namespace WebClone
{
    public class FileSavedSuccessEventArgs : EventArgs
    {
        public FileSavedSuccessEventArgs()
        { 
        }

        public FileSavedSuccessEventArgs(object userState)
        {
            this._userState = userState;
        }

        private object _userState;

        public object UserState
        {
            get { return _userState; }
        }
    }
}
