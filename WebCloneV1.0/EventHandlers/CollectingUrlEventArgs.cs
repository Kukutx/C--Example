using System;
using System.Collections.Generic;
using System.Text;
using WebClone.Models;

namespace WebClone
{
    public class CollectingUrlEventArgs : EventArgs
    {
        public CollectingUrlEventArgs()
        { 
        }

        public CollectingUrlEventArgs(UrlModel model)
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
