using Android.App;
using Android.Widget;
using Android.OS;
using static Android.Locations.GpsStatus;
using LGnumberGen.ServiceLayer;
using Android.Content;
using Android.Views.InputMethods;
using System;
using LGnumberGen.Repository;

namespace LGnumberGen
{
    [Activity(Theme = "@style/Theme.MyTheme")]
    public class MainActivity : Activity
    {
        NumberListXML numberListXML = new NumberListXML();

        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            numberListXML.Create();
            // Set our view from the "main" layout resource

            // Set the Layout Colour

            string Theme = SettingsService.loadColour();

            if (Theme == "")
            {
                SetTheme(Resource.Style.Theme_MyTheme);
            }
            else if (Theme == "Theme.PurpleTheme")
            {
                SetTheme(Resource.Style.Theme_PurpleTheme);
            }
            else if (Theme == "Theme.BlueTheme")
            {
                SetTheme(Resource.Style.Theme_BlueTheme);
            }
            else if (Theme == "Theme.GreenTheme")
            {
                SetTheme(Resource.Style.Theme_GreenTheme);
            }
            else if (Theme == "Theme.RedTheme")
            {
                SetTheme(Resource.Style.Theme_RedTheme);
            }

            SetContentView(Resource.Layout.Main);

            

            Button button = FindViewById<Button>(Resource.Id.numGen);
            Button buttonLoad = FindViewById<Button>(Resource.Id.Load);
            Button buttonNamedNum = FindViewById<Button>(Resource.Id.namedNumGen);
            Button buttonSettingsMain = FindViewById<Button>(Resource.Id.buttonSettingMain);

            // Toolbar Buttons

            ImageButton buttonLoadImg = FindViewById<ImageButton>(Resource.Id.btnLoadImg);
            ImageButton buttonSettings = FindViewById<ImageButton>(Resource.Id.btnSettingsImg);


            button.Click += Button_ClickNumberGen;
            buttonLoad.Click += Button_ClickLoad;
            buttonNamedNum.Click += Button_ClickNamedNum;
            buttonSettingsMain.Click += Button_ClickSettings;

            // Toolbar Clicks

            buttonLoadImg.Click += Button_ClickLoad;
            buttonSettings.Click += Button_ClickSettings;
           
        }

        //Open up Settings

        private void Button_ClickSettings(object sender, System.EventArgs e)
        {
            StartActivity(typeof(settingsActivity));
        }

        //Open up Named Number Generator

        private void Button_ClickNamedNum(object sender, System.EventArgs e)
        {
            StartActivity(typeof(namedNumGenActivity));
        }

        //Open up Number Generator

        public void Button_ClickNumberGen(object sender, System.EventArgs e)
        {
            StartActivity(typeof(numGenActivity));
        }

        //********************TOOLBAR CONTROLS*************************************

        //Open up Load Lists (Toolbar)

        private void Button_ClickLoad(object sender, System.EventArgs e)
        {
            StartActivity(typeof(loadActivity));

        }
    }
}

