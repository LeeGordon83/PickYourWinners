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
using LGnumberGen.ServiceLayer;

namespace LGnumberGen
{
    [Activity(Theme = "@style/Theme.MyTheme")]
    public class settingsActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

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

            SetContentView(Resource.Layout.Settings);

            //check the radio button of currently selected background.

            if (Theme == "Theme.PurpleTheme")
            {
                FindViewById<RadioButton>(Resource.Id.radioPurple).Checked = true;
            }
            else if (Theme == "Theme.BlueTheme")
            {
                FindViewById<RadioButton>(Resource.Id.radioBlue).Checked = true;
            }
            else if (Theme == "Theme.GreenTheme")
            {
                FindViewById<RadioButton>(Resource.Id.radioGreen).Checked = true;
            }
            else if (Theme == "Theme.RedTheme")
            {
                FindViewById<RadioButton>(Resource.Id.radioRed).Checked = true;
            }

            // Buttons

            Button buttonSaveSettings = FindViewById<Button>(Resource.Id.btnSaveSettings);

            //Button Clicks

            buttonSaveSettings.Click += Button_SaveSet;

            // Toolbar buttons

            ImageButton buttonLoadImg = FindViewById<ImageButton>(Resource.Id.btnLoadImg);
            ImageButton buttonHomeImg = FindViewById<ImageButton>(Resource.Id.btnHome);
            ImageButton buttonSettingsImg = FindViewById<ImageButton>(Resource.Id.btnSettingsImg);

            // Toolbar Clicks

            buttonLoadImg.Click += Button_ClickLoad;
            buttonHomeImg.Click += Button_ClickHome;

            // Create your application here
        }

        private void Button_SaveSet(object sender, EventArgs e)
        {
            string colour = "";

            if(FindViewById<RadioButton>(Resource.Id.radioDefault).Checked == true)
            {
                colour = "";
            }
            else if (FindViewById<RadioButton>(Resource.Id.radioPurple).Checked == true)
            {
                colour = "Theme.PurpleTheme";
            }
            else if (FindViewById<RadioButton>(Resource.Id.radioBlue).Checked == true)
            {
                colour = "Theme.BlueTheme";
            }
            else if (FindViewById<RadioButton>(Resource.Id.radioGreen).Checked == true)
            {
                colour = "Theme.GreenTheme";
            }
            else if (FindViewById<RadioButton>(Resource.Id.radioRed).Checked == true)
            {
                colour = "Theme.RedTheme";
            }

            SettingsService.saveColour(colour);

            StartActivity(typeof(MainActivity));
        }

        //********************TOOLBAR CONTROLS*************************************

        //Open up Load Lists (Toolbar)

        private void Button_ClickLoad(object sender, System.EventArgs e)
        {
            StartActivity(typeof(loadActivity));
        }

        //Open up Home (Toolbar)

        private void Button_ClickHome(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}