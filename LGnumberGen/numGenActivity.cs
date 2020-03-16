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
using LGnumberGen.Classes;
using Plugin.Share;
using Plugin.Share.Abstractions;

namespace LGnumberGen
{
    [Activity(Theme = "@style/Theme.MyTheme")]
    public class numGenActivity : Activity
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

            SetContentView(Resource.Layout.numGen);

            //Check to see if there is data to Load and Populate Text Boxes

            string LoadDetails = Intent.GetStringExtra("Mode");

            if (LoadDetails != null)
            {
                var LoadNumList = NumberListService.LoadList(LoadDetails);
                FindViewById<EditText>(Resource.Id.MaxNumber).Text = LoadNumList.MaxNumber.ToString();
                FindViewById<EditText>(Resource.Id.MinNumber).Text = LoadNumList.MinNumber.ToString();
                FindViewById<EditText>(Resource.Id.NumberOfPicks).Text = LoadNumList.DefPickNo.ToString();
                FindViewById<TextView>(Resource.Id.generatedNumbers).Text = LoadNumList.GenNum;
                FindViewById<TextView>(Resource.Id.hiddenGuid3).Text = LoadDetails;
                FindViewById<Button>(Resource.Id.buttonShareN).Visibility = Android.Views.ViewStates.Visible;

            }

            

            // Set our view from the "main" layout resource

            Button button = FindViewById<Button>(Resource.Id.buttonGen);
            Button save = FindViewById<Button>(Resource.Id.buttonSave);
            Button share = FindViewById<Button>(Resource.Id.buttonShareN);

            // Toolbar buttons

            ImageButton buttonLoadImg = FindViewById<ImageButton>(Resource.Id.btnLoadImg);
            ImageButton buttonHomeImg = FindViewById<ImageButton>(Resource.Id.btnHome);
            ImageButton buttonSettingsImg = FindViewById<ImageButton>(Resource.Id.btnSettingsImg);
       

            button.Click += Button_Click1;
            save.Click += Save_Click1;
            share.Click += Share_Click1;

            // Toolbar Clicks

            buttonLoadImg.Click += Button_ClickLoad;
            buttonHomeImg.Click += Button_ClickHome;
            buttonSettingsImg.Click += Button_ClickSettings;
        }

        //Share button Clicked
        private async void Share_Click1(object sender, EventArgs e)
        {

            string numberList = FindViewById<TextView>(Resource.Id.generatedNumbers).Text;

            var title = new ShareOptions { ChooserTitle = "Share Generated List" };

            var message = new ShareMessage { Text = numberList };

            await CrossShare.Current.Share(message, title);
        }


        //Generate Random Number Button Clicked
        public void Button_Click1(object sender, System.EventArgs e)
        {

            EditText maxNum = FindViewById<EditText>(Resource.Id.MaxNumber);
            EditText minNum = FindViewById<EditText>(Resource.Id.MinNumber);
            EditText pickNum = FindViewById<EditText>(Resource.Id.NumberOfPicks);

            //Check Text Boxes have been populated

            if (string.IsNullOrEmpty(maxNum.Text) || string.IsNullOrEmpty(minNum.Text) || string.IsNullOrEmpty(pickNum.Text))
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Check Your Entries!");
                alert.SetMessage("Please Complete All Fields");
                alert.SetPositiveButton("OK", (EventHandler<DialogClickEventArgs>)null);

                Dialog dialog = alert.Create();


                dialog.Show();

            }
            else
            {
                int minNumint = int.Parse(minNum.Text.ToString());
                int maxNumint = int.Parse(maxNum.Text.ToString());
                int pickNumint = int.Parse(pickNum.Text.ToString());

                bool NumTest = true;
                bool PickTest = true;

                if(minNumint == 0)
                {
                    if(pickNumint > maxNumint - minNumint +1)
                    {
                        NumTest = false;
                        PickTest = false;
                    }
                    
                }
                else if (minNumint > 0)
                {
                    if (pickNumint > maxNumint - minNumint +1)
                    {
                        NumTest = false;
                        PickTest = false;
                    }
                }

                //If all text boxes populated then run random number generator

                if (maxNumint > minNumint && !string.IsNullOrEmpty(maxNum.Text) && !string.IsNullOrEmpty(minNum.Text) && !string.IsNullOrEmpty(pickNum.Text) && PickTest == true && maxNumint <= 100000000 && NumTest == true && pickNumint <= 10000)
                {


                    string ranNum = NumberListService.numGen(minNumint, maxNumint, pickNumint);

                    FindViewById<TextView>(Resource.Id.generatedNumbers).Text = ranNum;
                    FindViewById<Button>(Resource.Id.buttonSave).Visibility = Android.Views.ViewStates.Visible;
                    FindViewById<Button>(Resource.Id.buttonShareN).Visibility = Android.Views.ViewStates.Visible;

                    InputMethodManager inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    var currentFocus = Window.CurrentFocus;

                    if (currentFocus != null)
                    {
                        inputManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
                    }

                }

                //List of error messages if text boxes not filled

                else
                {
                    string errMsg = "";

                    if (string.IsNullOrEmpty(minNum.Text))
                    {
                        errMsg = "Please enter a minimum number";
                    }

                    else if (string.IsNullOrEmpty(pickNum.Text))
                    {
                        errMsg = "Please enter the number of picks required";
                    }

                    else if (string.IsNullOrEmpty(maxNum.Text))
                    {
                        errMsg = "Please enter a Maximum Number";
                    }

                    else if (maxNumint < minNumint)
                    {
                        errMsg = "Maximum number must be greater than minimum number";
                    }
                    else if (PickTest == false)
                    {
                        errMsg = "Number of picks must be less than or equal to numbers available";
                    }
                    else if (maxNumint > 100000000)
                    {
                        errMsg = "Maximum Number must be no more than 100000000";
                    }
                    else if (NumTest == false)
                    {
                        errMsg = "Please enter a valid number of picks";
                    }
                    else if (pickNumint > 10000)
                    {
                        errMsg = "Please enter a maximum of 10000 picks";
                    }

                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Check Your Entries!");
                    alert.SetMessage(errMsg);
                    alert.SetPositiveButton("OK", (EventHandler<DialogClickEventArgs>)null);

                    Dialog dialog = alert.Create();

                    dialog.Show();

                }

            }
        }

        //Save button Clicked

        private void Save_Click1(object sender, EventArgs e)
        {
            string existingList = FindViewById<TextView>(Resource.Id.hiddenGuid3).Text;

            if (existingList == "")
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);

                EditText saveName = new EditText(this);

                string genNum = FindViewById<TextView>(Resource.Id.generatedNumbers).Text;
                string maxNum = FindViewById<EditText>(Resource.Id.MaxNumber).Text;
                string minNum = FindViewById<EditText>(Resource.Id.MinNumber).Text;
                string pickNum = FindViewById<EditText>(Resource.Id.NumberOfPicks).Text;

                int minNumint = int.Parse(minNum.ToString());
                int maxNumint = int.Parse(maxNum.ToString());
                int pickNumint = int.Parse(pickNum.ToString());

                alert.SetTitle("Save As...");
                alert.SetMessage("Please provide a save name for your list: -");
                alert.SetView(saveName);
                alert.SetPositiveButton("Save", (senderAlert, args) =>
                {

                    string saveNameString = saveName.Text;

                    if (saveNameString != "")
                    {
                       Guid idUpdate = NumberListService.SaveNum(saveNameString, genNum, maxNumint, minNumint, pickNumint);

                        FindViewById<TextView>(Resource.Id.hiddenGuid3).Text = idUpdate.ToString();
                    }

                });
                alert.SetNegativeButton("Cancel", (EventHandler<DialogClickEventArgs>)null);

                saveName.RequestFocus();



                Dialog dialog = alert.Create();

                dialog.Show();
            }
            else
            {
                string genNum = FindViewById<TextView>(Resource.Id.generatedNumbers).Text;
                string maxNum = FindViewById<EditText>(Resource.Id.MaxNumber).Text;
                string minNum = FindViewById<EditText>(Resource.Id.MinNumber).Text;
                string pickNum = FindViewById<EditText>(Resource.Id.NumberOfPicks).Text;

                int minNumint = int.Parse(minNum.ToString());
                int maxNumint = int.Parse(maxNum.ToString());
                int pickNumint = int.Parse(pickNum.ToString());

                NumberListService.SaveExisting(existingList, genNum, maxNumint, minNumint, pickNumint);

                Toast.MakeText(this, "Changes Saved", ToastLength.Short).Show();
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
           

        //Open up Settings (Toolbar)
        private void Button_ClickSettings(object sender, EventArgs e)
        {
            StartActivity(typeof(settingsActivity));
        }



    }
}