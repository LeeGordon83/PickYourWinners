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
    internal class namedNumberAdapter : BaseAdapter<NameListItem>
    {
        List<NameListItem> items;
        Activity context;

        public namedNumberAdapter(Activity context, List<NameListItem> items):base()
        {
            this.context = context;
            this.items = items;
        }

        public override NameListItem this[int position]
        {
            get
            {
                return items[position];
            }
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.NamedNumSelection, null);
            }

            
          
            view.FindViewById<TextView>(Resource.Id.listNameEntry).Text = item.Name.ToString();
            view.FindViewById<TextView>(Resource.Id.nameId).Text = item.Id.ToString();
            


            CheckBox chkBox = view.FindViewById<CheckBox>(Resource.Id.chkActive);

            chkBox.Tag = position;

            if (item.Active == false)
            {
                chkBox.Checked = false;
            }
            else
            {
                chkBox.Checked = true;
            }

            chkBox.Click -= UpdateCheckStatus;
            view.LongClick -= OpenDeleteItem;

            view.Tag = item.Id.ToString();

            chkBox.Click += UpdateCheckStatus;
            view.LongClick += OpenDeleteItem;

            return view;
        }

        //method to delete a name

        private void OpenDeleteItem(object sender, View.LongClickEventArgs e)
        {
            View view = (View)sender;

            string NameId = view.Tag.ToString();

            string ListID = context.FindViewById<TextView>(Resource.Id.hiddenGuid).Text;

            string Name = view.FindViewById<TextView>(Resource.Id.listNameEntry).Text;

            NamedNumberListService.DeleteName(NameId, ListID);

            Intent intent = new Intent(context, typeof(namedNumGenActivity));

            intent.PutExtra("Delete", ListID);

            Toast.MakeText(this.context, "Deleted " + Name, ToastLength.Long).Show();

            context.StartActivity(intent);
        }

        //method to update check status

        internal void UpdateCheckStatus(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;

            View view = (View)sender;

            int position = Convert.ToInt32(view.Tag.ToString());

            var item = items[position];

            string nameId = context.FindViewById<TextView>(Resource.Id.nameId).Text = item.Id.ToString();

            string ListID = context.FindViewById<TextView>(Resource.Id.hiddenGuid).Text;

            int selected = Convert.ToInt32(context.FindViewById<TextView>(Resource.Id.selectedNumber).Text);

            if (chkBox.Checked)
            {

                item.Active = true;

                NamedNumberListService.UpdateActive(nameId, ListID, true);

                selected++;

            }
            else
            {
                item.Active = false;

                NamedNumberListService.UpdateActive(nameId, ListID, false);

                selected--;
            }


            
            

            context.FindViewById<TextView>(Resource.Id.selectedNumber).Text = selected.ToString();

        }
    }
}