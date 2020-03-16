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

namespace LGnumberGen
{
    internal class LoadNamedAdapter : BaseAdapter<NamedNumberList>
    {

        List<NamedNumberList> items;
        Activity context;

        public LoadNamedAdapter(Activity context, List<NamedNumberList> items):base()
        {
            this.context = context;
            this.items = items;
        }


        public override NamedNumberList this[int position]
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


            view.Tag = item.ListId.ToString();

            view.Click += openLoadItem;
            view.LongClick += OpenDeleteItem;

            return view;
        }


        private void openLoadItem(object sender, EventArgs e)
        {
            View view = (View)sender;

            string LoadId = view.Tag.ToString();

            Intent intent = new Intent(context, typeof(namedNumGenActivity));

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