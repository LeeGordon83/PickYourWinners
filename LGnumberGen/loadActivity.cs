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
using LGnumberGen.Classes;
using LGnumberGen.ServiceLayer;

namespace LGnumberGen
{
    [Activity(Theme = "@style/Theme.MyTheme")]
    public class loadActivity : Activity
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

            SetContentView(Resource.Layout.loadList);


            //Other Buttons

            Button buttonNamedNumLoad = FindViewById<Button>(Resource.Id.buttonNamedNumListToggle);
            Button buttonListLoad = FindViewById<Button>(Resource.Id.buttonNumberListToggle);

            //Button Clicks

            buttonNamedNumLoad.Click += Button_ClickNamedNumLoad;
            buttonListLoad.Click += Button_ClickListLoad;

            // Toolbar buttons

            ImageButton buttonLoadImg = FindViewById<ImageButton>(Resource.Id.btnLoadImg);
            ImageButton buttonHomeImg = FindViewById<ImageButton>(Resource.Id.btnHome);
            ImageButton buttonSettingsImg = FindViewById<ImageButton>(Resource.Id.btnSettingsImg);

            // Toolbar Clicks

            buttonLoadImg.Click += Button_ClickLoad;
            buttonHomeImg.Click += Button_ClickHome;
            buttonSettingsImg.Click += Button_ClickSettings;

            //Check to see if request to Delete has been Made

            string DeleteItem = Intent.GetStringExtra("Delete");

            if (DeleteItem != null)
            {
                
                NumberListService.DeleteList(DeleteItem);
                NamedNumberListService.DeleteList(DeleteItem);
            }

            //Get All Saved Number Lists and Populate to View

            List<NumberList> LoadList = NumberListService.GetAllSaved();

            ListView loadListView = FindViewById<ListView>(Resource.Id.listLoads);

            loadListView.Adapter = new loadListAdapter(this, LoadList);

        }

        private void Button_ClickListLoad(object sender, EventArgs e)
        {
            List<NumberList> LoadList = NumberListService.GetAllSaved();

            ListView loadListView = FindViewById<ListView>(Resource.Id.listLoads);

            loadListView.Adapter = new loadListAdapter(this, LoadList);

            FindViewById<TextView>(Resource.Id.textView1).Text = "Press to Load Number List:  ";
        }

        private void Button_ClickNamedNumLoad(object sender, EventArgs e)
        {
            List<NamedNumberList> LoadNamedNumberList = NamedNumberListService.GetAllSaved();

            ListView loadNamedListView = FindViewById<ListView>(Resource.Id.listLoads);

            loadNamedListView.Adapter = new LoadNamedAdapter(this, LoadNamedNumberList);

            FindViewById<TextView>(Resource.Id.textView1).Text = "Press to Load Named List:  ";
        }   



        //********************TOOLBAR CONTROLS*************************************

        //Open up Load Lists (Toolbar)

        private void Button_ClickLoad(object sender, System.EventArgs e)
        {
        
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