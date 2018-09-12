using Android.App;
using Android.Widget;
using Android.OS;

namespace Demo
{
    [Activity(Label = "Demo", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { button.Text = $"clicks!"; };
        }
    }
}

