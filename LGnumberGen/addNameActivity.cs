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
using Android.Views.InputMethods;

namespace LGnumberGen
{
    [Activity(Theme = "@style/Theme.MyTheme")]
    public class addNameActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

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

            string existingGuid = Intent.GetStringExtra("Existing");

            SetContentView(Resource.Layout.AddName);

            


            Button buttonSaveName = FindViewById<Button>(Resource.Id.saveName);
            Button buttonCancel = FindViewById<Button>(Resource.Id.buttonCancelAdd);
            buttonSaveName.Click += Button_ClickSaveName;
            buttonCancel.Click += Button_ClickCancel;


            // Toolbar buttons

            ImageButton buttonLoadImg = FindViewById<ImageButton>(Resource.Id.btnLoadImg);
            ImageButton buttonHomeImg = FindViewById<ImageButton>(Resource.Id.btnHome);
            ImageButton buttonSettingsImg = FindViewById<ImageButton>(Resource.Id.btnSettingsImg);

            // Toolbar Clicks

            buttonLoadImg.Click += Button_ClickLoad;
            buttonHomeImg.Click += Button_ClickHome;
           


        }

        private void Button_ClickCancel(object sender, EventArgs e)
        {



            string existingGuid = Intent.GetStringExtra("Existing");

            var intent = new Intent(this, typeof(namedNumGenActivity));

            intent.PutExtra("ExistingReturn", existingGuid);

            StartActivity(intent);
            
        }

        //Save Name Method

        private void Button_ClickSaveName(object sender, EventArgs e)
        {
            EditText NametoAdd = FindViewById<EditText>(Resource.Id.AddName);

            string NametoAddStr = NametoAdd.Text;

            if (NametoAddStr != "")
            {
                string existingGuid = Intent.GetStringExtra("Existing");

                NamedNumberListService.listAdd(NametoAddStr, existingGuid);

                var intent = new Intent(this, typeof(namedNumGenActivity));

                intent.PutExtra("ExistingReturn", existingGuid);

                StartActivity(intent);
            }
            else
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Enter a Name");
                alert.SetMessage("Please Enter a Name in the Field");
                alert.SetPositiveButton("OK", (EventHandler<DialogClickEventArgs>)null);

                Dialog dialog = alert.Create();


                dialog.Show();
            }
        }

        public override void OnBackPressed()
        {
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

        //Open Up Settings (Toolbar)

        private void Button_ClickSettings(object sender, EventArgs e)
        {
            StartActivity(typeof(settingsActivity));
        }

    }
}