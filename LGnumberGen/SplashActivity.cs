using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using System.Threading.Tasks;
using static Android.Bluetooth.BluetoothClass;
using LGnumberGen.Repository;

namespace LGnumberGen
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]

    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SplashLayout);

            // Create your application here
            // Simulate a long loading process on app

            NumberListXML numberListXML = new NumberListXML();
            NamedNumberListXML NamednumberListXML = new NamedNumberListXML();
            SettingsXML settingsXML = new SettingsXML();

            numberListXML.Create();
            NamednumberListXML.Create();
            settingsXML.Create();

            //Thread.Sleep(1500);

            LoadActivity();
        }
        private async void LoadActivity()
        {
            // Simulate a long pause
            //System.Threading.Thread.Sleep(1500);

            await Task.Delay(2000);
            StartActivity(typeof(MainActivity));

        }



        public override void OnBackPressed()
        {
        }


    }
}