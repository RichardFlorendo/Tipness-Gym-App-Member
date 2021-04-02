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
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;

namespace TipnessMobile
{
    [Activity(Label = "ClientHomePage", Theme = "@style/AppTheme.NoActionBar")]
    public class ClientHomePage : Activity
    {
        private TextView txtMysub;
        private Spinner spnrList;
        private Button btnBranch;
        private ListView promolistview;
        private ImageView btnBranches;

        ArrayAdapter adapter;
        ArrayList branches;

        ArrayList branch;
        ArrayList price;
        ArrayList name;
        ArrayList duration;

        int check = 0;

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_clienthomepage);

            txtMysub = FindViewById<TextView>(Resource.Id.txtmySubscription);
            txtMysub.Click += TxtMysub_Click;

            btnBranches = FindViewById<ImageView>(Resource.Id.imageBranchButton);
            btnBranches.Click += BtnBranches_Click;

            // Set our view from the "main" layout resource
            promolistview = FindViewById<ListView>(Resource.Id.listPromos);
           
            //spinner data
            GetBranch();
            spnrList = FindViewById<Spinner>(Resource.Id.spinnerBranch);
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, branches);
            spnrList.Adapter = adapter;

            //button for branches
            btnBranch = FindViewById<Button>(Resource.Id.buttonBranch);
            btnBranch.Click += BtnBranch_Click;

            //spinner select
            spnrList.ItemSelected += SpnrList_ItemSelected;
        }

        private void BtnBranches_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(GymAddress));
        }

        private JniHandleOwnership GetListValues()
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        private void SpnrList_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (++check > 1)
            {

                MySqlConnection con = new MySqlConnection("Server=db4free.net;Port=3306;database=dbtipnessgym;User Id=tipnessfitness;Password=tipness2020;charset=utf8;old guids=true");
                try
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("SELECT `bName`, `pName`, `pDesc`, `pDuration` FROM `tblannouncement` WHERE `isActive` = 1 and `bname`='"+ branches[e.Position].ToString() + "'", con);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {

                            List<Data> myList = new List<Data>();
                            while (reader.Read())
                            {
                                Data obj = new Data();
                                obj.branchname = reader["bName"].ToString();
                                obj.pname = reader["pName"].ToString();
                                obj.pprice = reader["pDesc"].ToString();
                                obj.pduration = reader["pDuration"].ToString();
                                myList.Add(obj);
                            }
                            promolistview.Adapter = new DataAdapter(this, myList);
                        }
                        else {
                            List<Data> myList = new List<Data>();
                            promolistview.Adapter = new DataAdapter(this, myList);
                        }
                    }

                }
                catch (MySqlException ex)
                {
                    //txtSysLog.Text = ex.ToString();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void TxtMysub_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MySubscription));
        }

        private void BtnBranch_Click(object sender, EventArgs e)
        {
            spnrList.PerformClick();
        }

        private void GetBranch()
        {
            branches = new ArrayList();
            MySqlConnection con = new MySqlConnection("Server=db4free.net;Port=3306;database=dbtipnessgym;User Id=tipnessfitness;Password=tipness2020;charset=utf8;old guids=true");
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    string branchname = "";
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Bname FROM tblbranch WHERE isActive = 1", con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        branchname = reader["Bname"].ToString();
                        branches.Add(branchname);
                    }

                }
            }
            catch (MySqlException ex)
            {
                //txtSysLog.Text = ex.ToString();
            }
            finally
            {
                con.Close();
            }
        }
    }


}