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
    public class Branch
    {
        public string branchname;
        public string branchaddress;

        public Branch()
        {
            branchname = "";
            branchaddress = "";
        }
    }
}