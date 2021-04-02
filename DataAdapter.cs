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

namespace TipnessMobile
{
    public class DataAdapter : BaseAdapter<Data>
    {
        List<Data> items;

        Activity context;
        public DataAdapter(Activity context, List<Data> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Data this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.list_itemPromos, null);

            view.FindViewById<TextView>(Resource.Id.txtbranch).Text = item.branchname;
            view.FindViewById<TextView>(Resource.Id.txtprice).Text = item.pprice;
            view.FindViewById<TextView>(Resource.Id.txtPromoname).Text = item.pname;
            view.FindViewById<TextView>(Resource.Id.txtduration).Text = item.pduration;
            return view;
        }

    }
}