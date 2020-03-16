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
using Android.Views.InputMethods;

namespace LGnumberGen
{
    [Activity(Theme = "@style/Theme.MyTheme")]
    public class namedNumGenActivity : Activity
    {
        public Button Button_ClickGenList { get; private set; }

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

            SetContentView(Resource.Layout.namedNumGen);

            int selectedNumber = 0;
            string LoadDetails = Intent.GetStringExtra("Mode");
            string DeleteDetails = Intent.GetStringExtra("Delete");



            if (LoadDetails != null || DeleteDetails !=null)
            {
                if(LoadDetails == null)
                {
                    LoadDetails = DeleteDetails;
                }


                var LoadNamedNumList = NamedNumberListService.LoadNamedList(LoadDetails);
                FindViewById<TextView>(Resource.Id.hiddenGuid).Text = LoadNamedNumList.ListId.ToString();
                List<NameListItem> ListContent = NamedNumberListService.listGen(LoadNamedNumList.ListId.ToString());

                ListView NamedNumberListView = FindViewById<ListView>(Resource.Id.NameNumList);

                NamedNumberListView.ChoiceMode = ChoiceMode.Multiple;

                NamedNumberListView.Adapter = new namedNumberAdapter(this, ListContent);

                int updatedSelectedNum = ListContent.Where(x => x.Active == true).Count();

                FindViewById<TextView>(Resource.Id.selectedNumber).Text = updatedSelectedNum.ToString();

            }
            else
            {

                string existingGuid = Intent.GetStringExtra("ExistingReturn");
                FindViewById<TextView>(Resource.Id.hiddenGuid).Text = existingGuid;



                if (existingGuid == null)
                {
                    NamedNumberList newList = NamedNumberListService.NewList();
                    FindViewById<TextView>(Resource.Id.hiddenGuid).Text = newList.ListId.ToString();
                    FindViewById<TextView>(Resource.Id.selectedNumber).Text = selectedNumber.ToString();
                }


                else
                {


                    List<NameListItem> ListContent = NamedNumberListService.listGen(existingGuid);

                    ListView NamedNumberListView = FindViewById<ListView>(Resource.Id.NameNumList);

                    NamedNumberListView.ChoiceMode = ChoiceMode.Multiple;

                    NamedNumberListView.Adapter = new namedNumberAdapter(this, ListContent);

                    int updatedSelectedNum = ListContent.Where(x => x.Active == true).Count();

                    FindViewById<TextView>(Resource.Id.selectedNumber).Text = updatedSelectedNum.ToString();
                }



            }

           
            Button buttonAddName = FindViewById<Button>(Resource.Id.buttonAddName);
            Button buttonsaveList = FindViewById<Button>(Resource.Id.buttonsaveNamedNumList);
            Button buttonGenNamedList = FindViewById<Button>(Resource.Id.btnGenerateList);



            // Toolbar buttons

            ImageButton buttonLoadImg = FindViewById<ImageButton>(Resource.Id.btnLoadImg);
            ImageButton buttonHomeImg = FindViewById<ImageButton>(Resource.Id.btnHome);
            ImageButton buttonSettingsImg = FindViewById<ImageButton>(Resource.Id.btnSettingsImg);

            // Toolbar Clicks

            buttonLoadImg.Click += Button_ClickLoad;
            buttonHomeImg.Click += Button_ClickHome;
            buttonSettingsImg.Click += Button_ClickSettings;

            //Button Clicks

            buttonAddName.Click += Button_ClickAddName;
            buttonsaveList.Click += Button_ClickSaveList;
            buttonGenNamedList.Click += Button_ClickGenListNow;
        }


        //Generate the List Method

        private void Button_ClickGenListNow(object sender, EventArgs e)
        {
            string ListCount = FindViewById<TextView>(Resource.Id.selectedNumber).Text;

            int ListCountint = int.Parse(ListCount.ToString());

            if(ListCountint < 1)
            {
                

                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Check Your Entries!");
                alert.SetMessage("Select at least one name");
                alert.SetPositiveButton("OK", (EventHandler<DialogClickEventArgs>)null);

                Dialog dialog = alert.Create();

                dialog.Show();
            }
            else
            {


                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                
                Spinner numSpin = new Spinner(this);

                int[] From = Enumerable.Range(1, ListCountint).ToArray();
                
                ArrayAdapter _adapterFrom = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, From);
                _adapterFrom.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                numSpin.Adapter = _adapterFrom;

                numSpin.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

                string existingListID = FindViewById<TextView>(Resource.Id.hiddenGuid).Text;

                alert.SetTitle("Number of Selections");
                alert.SetMessage("Please specify the number of selections you require: -");
                alert.SetView(numSpin);
                alert.SetPositiveButton("Generate", (senderAlert, args) =>
                {
                    Intent intent = new Intent(this, typeof(GeneratedListActivity));

                    intent.PutExtra("ListID", existingListID);

                    this.StartActivity(intent);
                });

                alert.SetNegativeButton("Cancel", (EventHandler<DialogClickEventArgs>)null);

                Dialog dialog = alert.Create();

                dialog.Show();
            }
        }

        //save the number of picks user wants returned

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            List<String> idList = new List<String>();

            var NamedNumberListView = FindViewById<ListView>(Resource.Id.NameNumList);

            for (int i = 0; i < NamedNumberListView.Count; i++)
            {
                
                

                View v = NamedNumberListView.GetChildAt(i + NamedNumberListView.FirstVisiblePosition);


                if (v != null)
                {
                    TextView tt = (TextView)v.FindViewById(Resource.Id.nameId);
                    CheckBox ck = (CheckBox)v.FindViewById(Resource.Id.chkActive);

                    if (ck.Checked == true)
                    {
                        idList.Add(tt.Text);
                    }
                }
            }



            string existingListID = FindViewById<TextView>(Resource.Id.hiddenGuid).Text;

            string numtoGen = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

            int numtoGenint = int.Parse(numtoGen.ToString());

            NamedNumberListService.PickNumbers(numtoGenint, existingListID, idList);
            
        }



        //Save the List Method

        private void Button_ClickSaveList(object sender, EventArgs e)
        {
            string existingListID = FindViewById<TextView>(Resource.Id.hiddenGuid).Text;

            bool alreadyExists = NamedNumberListService.ExistsCheck(existingListID);

            if (alreadyExists == false)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);

                EditText saveName = new EditText(this);

                alert.SetTitle("Save As...");
                alert.SetMessage("Please provide a save name for your list: -");
                alert.SetView(saveName);
                alert.SetPositiveButton("Save", (senderAlert, args) =>
                {

                    string saveNameString = saveName.Text;

                    if (saveNameString != "")
                    {
                        NamedNumberListService.SaveNum(existingListID, saveNameString);

                    }

                });
                alert.SetNegativeButton("Cancel", (EventHandler<DialogClickEventArgs>)null);

                saveName.RequestFocus();




                Dialog dialog = alert.Create();

                dialog.Show();
            }
            else
            {
                Toast.MakeText(this, "Changes Saved", ToastLength.Short).Show();
            }
       
        }

        //Adding a Name Method

        private void Button_ClickAddName(object sender, EventArgs e)
        {
            View view = (View)sender;

            TextView existing = FindViewById<TextView>(Resource.Id.hiddenGuid);

            string ExistingId = existing.Text;

            var intent = new Intent(this, typeof(addNameActivity));

            intent.PutExtra("Existing", ExistingId);

            StartActivity(intent);


        }

        public override void OnBackPressed()
        {
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
            StartActivity(typeof(settingsActivity));
        }
    }
}