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
    [Activity(Label = "GymAddress")]
    public class GymAddress : Activity
    {
        private ImageView backBtn;
        private ListView addresslistview;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_addressList);
            addresslistview = FindViewById<ListView>(Resource.Id.addListview);
            loadAddress();
            backBtn = FindViewById<ImageView>(Resource.Id.imageBackButton);
            backBtn.Click += BackBtn_Click;

        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        public void loadAddress()
        {
            MySqlConnection con = new MySqlConnection("Server=db4free.net;Port=3306;database=dbtipnessgym;User Id=tipnessfitness;Password=tipness2020;charset=utf8;old guids=true");
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT `Bname`, `Baddress` FROM `tblbranch` WHERE `isActive` = 1 ", con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {

                        List<Branch> myList = new List<Branch>();
                        while (reader.Read())
                        {
                            Branch obj = new Branch();
                            obj.branchname = reader["Bname"].ToString();
                            obj.branchaddress = reader["Baddress"].ToString();
                            myList.Add(obj);
                        }
                        addresslistview.Adapter = new BranchAdapter(this, myList);
                    }
                    else
                    {
                        List<Branch> myList = new List<Branch>();
                        addresslistview.Adapter = new BranchAdapter(this, myList);
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