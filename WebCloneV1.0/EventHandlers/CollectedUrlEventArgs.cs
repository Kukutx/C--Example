using System;
using System.Collections.Generic;
using System.Text;
using WebClone.Models;

namespace WebClone
{
    public class CollectedUrlEventArgs : EventArgs
    {
        public CollectedUrlEventArgs()
        { 
        }

        public CollectedUrlEventArgs(UrlModel model)
        {
            this._model = model;
        }

        private UrlModel _model;

        public UrlModel Model
        {
            get { return _model; }
        }
    }
}
