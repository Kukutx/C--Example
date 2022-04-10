using System;
using System.Collections.Generic;
using System.Text;

namespace WebClone
{
    interface IWebCloneWorker
    {
        void Start();
        void Cancel();
    }
}
