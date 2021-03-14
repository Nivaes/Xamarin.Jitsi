using System;
using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using Android.Widget;
using Java.Lang;
using Java.Net;
//using Org.Jitsi.Meet.Sdk;

namespace JitsiMeet.Droid.Sample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity
        : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            base.SetContentView(Resource.Layout.activity_main);

            URL serverUrl;
            try
            {
                serverUrl = new URL("https://meet.jit.si");
            }
            catch (MalformedURLException e)
            {
                e.PrintStackTrace();
                throw new RuntimeException("Invalid server URL!");
            }

            //JitsiMeetConferenceOptions defaultOptions
            //    = new JitsiMeetConferenceOptions.Builder()
            //        .SetServerURL(serverUrl)
            //        .SetWelcomePageEnabled(false)
            //        .Build();

            //Org.Jitsi.Meet.Sdk.JitsiMeet.DefaultConferenceOptions = defaultOptions;

            var butJoint = base.FindViewById<Button>(Resource.Id.butJoin);
            butJoint.Click += ButJointClick;
        }

        private void ButJointClick(object sender, EventArgs e)
        {
            var editText = base.FindViewById<EditText>(Resource.Id.conferenceName);
            var text = editText.Text;

            if (text.Length > 0)
            {
                // Build options object for joining the conference. The SDK will merge the default
                // one we set earlier and this one when joining.
                //JitsiMeetConferenceOptions options
                //    = new JitsiMeetConferenceOptions.Builder()
                //        .SetRoom(text)
                //        .Build();

                // Launch the new activity with the given options. The launch() method takes care
                // of creating the required Intent and passing the options.
                //_ = Org.Jitsi.Meet.Sdk.JitsiMeetActivity.Launch(this, options);
            }
        }
    }
}
