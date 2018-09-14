using System;
using Android.App;
using Android.Runtime;

namespace Demo
{
    [Application(LargeHeap = true, Debuggable = true)]
    [MetaData("com.google.android.gms.version", Value = "@integer/google_play_services_version")]
    public class App : Application
    {
        public App(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
    }
}