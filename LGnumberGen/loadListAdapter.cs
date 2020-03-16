using System.Collections.Generic;
using Android.Widget;
using LGnumberGen.Classes;
using Android.App;
using Android.Views;
using Android.Content;
using System;

namespace LGnumberGen
{
    internal class loadListAdapter : BaseAdapter<NumberList>
    {
        List<NumberList> items;
        Activity context;

        public loadListAdapter(Activity context, List<NumberList> items):base()
        {
            this.context = context;
            this.items = items;
        }

        public override NumberList this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.loadListSelection, null);
            }


            view.FindViewById<TextView>(Resource.Id.loadName).Text = item.ListName.ToString();

            view.Click -= openLoadItem;
            view.LongClick -= OpenDeleteItem;


            view.Tag = item.Id.ToString();

            view.Click += openLoadItem;
            view.LongClick += OpenDeleteItem;
            
 
            return view;
        }

        private void openLoadItem(object sender, EventArgs e)
        {
            View view = (View)sender;

            string LoadId = view.Tag.ToString();

            Intent intent = new Intent(context, typeof(numGenActivity));

            intent.PutExtra("Mode", LoadId);

            context.StartActivity(intent);
        }

        private void OpenDeleteItem(object sender, View.LongClickEventArgs e)
        {
            View view = (View)sender;

            string LoadId = view.Tag.ToString();

            string LoadName = view.FindViewById<TextView>(Resource.Id.loadName).Text;

            Intent intent = new Intent(context, typeof(loadActivity));

            intent.PutExtra("Delete", LoadId);

            Toast.MakeText(this.context, "Deleted " + LoadName, ToastLength.Long).Show();

            context.StartActivity(intent);
        }

        

    }
}