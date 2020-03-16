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
using LGnumberGen.Classes;
using Plugin.Share;
using Plugin.Share.Abstractions;

namespace LGnumberGen
{
    [Activity(Theme = "@style/Theme.MyTheme")]
    public class GeneratedListActivity : Activity
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

            SetContentView(Resource.Layout.GeneratedList);

            string ListDetailsId = Intent.GetStringExtra("ListID");

            FindViewById<TextView>(Resource.Id.hiddenGuidNew).Text = ListDetailsId;

           

            
            List<NameListItem> ListContent = NamedNumberListService.GenerateNamedNumList(ListDetailsId);

            ListView NamedNumberListView = FindViewById<ListView>(Resource.Id.GeneratedList);

            NamedNumberListView.ChoiceMode = ChoiceMode.Multiple;

            NamedNumberListView.Adapter = new GeneratedListAdapter(this, ListContent);

            //Other Buttons

            Button buttonReGen = FindViewById<Button>(Resource.Id.btnReGenerateList);
            Button buttonBack = FindViewById<Button>(Resource.Id.buttonBack);
            Button buttonShare = FindViewById<Button>(Resource.Id.buttonShare);

            //Button CLicks

            buttonReGen.Click += Button_ClickReGen;
            buttonBack.Click += Button_ClickBack;
            buttonShare.Click += Button_ClickShare;

            // Toolbar buttons

            ImageButton buttonLoadImg = FindViewById<ImageButton>(Resource.Id.btnLoadImg);
            ImageButton buttonHomeImg = FindViewById<ImageButton>(Resource.Id.btnHome);
            ImageButton buttonSettingsImg = FindViewById<ImageButton>(Resource.Id.btnSettingsImg);

            // Toolbar Clicks

            buttonLoadImg.Click += Button_ClickLoad;
            buttonHomeImg.Click += Button_ClickHome;
            buttonSettingsImg.Click += Button_ClickSettings;
        }

        // Share Button Method

        private async void Button_ClickShare(object sender, EventArgs e)
        {

            var NamedNumberListView = FindViewById<ListView>(Resource.Id.GeneratedList);
            var nameList = new StringBuilder();
            nameList.Append("");

            for (int i = 0; i < NamedNumberListView.Count; i++)
            {
                View v = NamedNumberListView.GetChildAt(i - NamedNumberListView.FirstVisiblePosition);

                if (v != null)
                {
                    TextView tt = (TextView)v.FindViewById(Resource.Id.GenerateListNameEntry);

                    int j = i + 1;

                    if (nameList.ToString() == "")
                    {
                        nameList.Append(j+". " + tt.Text);
                    }
                    else
                    {
                        nameList.Append("\n" + j + ". " + tt.Text);
                    }
                }
            }

            var title = new ShareOptions { ChooserTitle = "Share Generated List" };

            var message = new ShareMessage { Text = nameList.ToString() };

            await CrossShare.Current.Share(message, title);
        }

        //Back Button Method

        private void Button_ClickBack(object sender, EventArgs e)
        {
            string existingListID = FindViewById<TextView>(Resource.Id.hiddenGuidNew).Text;

            Intent intent = new Intent(this, typeof(namedNumGenActivity));

            intent.PutExtra("Mode", existingListID);

            this.StartActivity(intent);
        }

        private void Button_ClickReGen(object sender, EventArgs e)
        {
            string existingListID = FindViewById<TextView>(Resource.Id.hiddenGuidNew).Text;

            List<NameListItem> ListContent = NamedNumberListService.GenerateNamedNumList(existingListID);

            ListView NamedNumberListView = FindViewById<ListView>(Resource.Id.GeneratedList);

            NamedNumberListView.ChoiceMode = ChoiceMode.Multiple;

            NamedNumberListView.Adapter = new GeneratedListAdapter(this, ListContent);
        }


        //********************TOOLBAR CONTROLS*************************************

        //Open up Load Lists (Toolbar)

        private void Button_ClickLoad(object sender, System.EventArgs e)
        {
            StartActivity(typeof(loadActivity));
            Finish();
        }

        //Open up Home (Toolbar)

        private void Button_ClickHome(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }

        //Open Up Settings (Toolbar)

        private void Button_ClickSettings(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}