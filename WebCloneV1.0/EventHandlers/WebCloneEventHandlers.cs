using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WebClone
{
    public delegate void UrlChangedEventHandler(object sender, UrlChangedEventArgs e);
    public delegate void FileSavedSuccessEventHandler(object sender, FileSavedSuccessEventArgs e);
    public delegate void FileSavedFailEventHandler(object sender, FileSavedFailEventArgs e);
    public delegate void DownloadCompletedEventHandler(object sender, DownloadCompletedEventArgs e);
    public delegate void CollectingUrlEventHandler(object sender, CollectingUrlEventArgs e);
    public delegate void CollectedUrlEventHandler(object sender, CollectedUrlEventArgs e);
    public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);
}
