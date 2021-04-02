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
    public class BranchAdapter : BaseAdapter<Branch>
    {
        List<Branch> items;

        Activity context;
        public BranchAdapter(Activity context, List<Branch> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Branch this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.list_itemGymAddress, null);

            view.FindViewById<TextView>(Resource.Id.txtBranchName).Text = item.branchname;
            view.FindViewById<TextView>(Resource.Id.txtBranchAddress).Text = item.branchaddress;
            return view;
        }

    }
}