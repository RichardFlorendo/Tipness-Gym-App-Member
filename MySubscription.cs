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
using Android.Support.Percent;
namespace TipnessMobile
{
    [Activity(Label = "MySubscription", Theme = "@style/AppTheme.NoActionBar")]
    
    public class MySubscription : Activity
    {
        ArrayAdapter adapter;
        ArrayList info;
        private EditText editextRid;
        private ImageView backBtn;
        private Button goBtn;
        private TextView textViewError, txtRidnumber, txtbranch, txtaccess, txtstart, txtend, txttraining, txttrainer;
        [Obsolete]
        private PercentRelativeLayout layoutInfo;

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_mysubscription);
            editextRid = FindViewById<EditText>(Resource.Id.editTextRid);

            //views
            txtRidnumber = FindViewById<TextView>(Resource.Id.txtRidnumber);
            txtbranch = FindViewById<TextView>(Resource.Id.txtbranch);
            txtaccess = FindViewById<TextView>(Resource.Id.txtaccess);
            txtstart = FindViewById<TextView>(Resource.Id.txtstart);
            txtend = FindViewById<TextView>(Resource.Id.txtend);
            txttraining = FindViewById<TextView>(Resource.Id.txttraining);
            txttrainer = FindViewById<TextView>(Resource.Id.txttrainer);



            backBtn = FindViewById<ImageView>(Resource.Id.imageBackButton);
            goBtn = FindViewById<Button>(Resource.Id.buttonGo);


            textViewError = FindViewById<TextView>(Resource.Id.txterror);
            layoutInfo = FindViewById<PercentRelativeLayout>(Resource.Id.infoContainer);
            goBtn.Click += GoBtn_Click;
            backBtn.Click += BackBtn_Click;

          
        }

        private void GoBtn_Click(object sender, EventArgs e)
        {
            if (editextRid.Text == "")
            {
                //visible the 
                layoutInfo.Visibility = Android.Views.ViewStates.Invisible;
                textViewError.Visibility = Android.Views.ViewStates.Visible;
            }
            else if (editextRid.Text != "")
            {
                info = new ArrayList();
                MySqlConnection con = new MySqlConnection("Server=db4free.net;Port=3306;database=dbtipnessgym;User Id=tipnessfitness;Password=tipness2020;charset=utf8;old guids=true");
                try
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        string RId = "";
                        string gAccessName = "";
                        string cNumber = "";
                        string sDate = "";
                        string eDate = "";
                        string aTraining = "";
                        string tName = "";
                        string bName = "";
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("SELECT `RId`,`gAccessName`, `cNumber`, `sDate`, `eDate`, `aTraining`, `tName`, `bName`, `isActive` FROM `tblmembers` WHERE `RId` = '" + editextRid.Text + "' and `isActive` = 1", con);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                RId = reader["RId"].ToString();
                                gAccessName = reader["gAccessName"].ToString();
                                cNumber = reader["cNumber"].ToString();
                                sDate = reader["sDate"].ToString();
                                eDate = reader["eDate"].ToString();
                                aTraining = reader["aTraining"].ToString();
                                tName = reader["tName"].ToString();
                                bName = reader["bName"].ToString();

                                //var toast = Toast.MakeText(Application.Context, RId, ToastLength.Short);
                                //toast.Show();

                                if (reader["RId"] != DBNull.Value || reader["gAccessName"] != DBNull.Value || reader["sDate"] != DBNull.Value || reader["eDate"] != DBNull.Value || reader["aTraining"] != DBNull.Value || reader["tName"] != DBNull.Value || reader["bName"] != DBNull.Value)
                                {

                                    txtRidnumber.Text = RId;
                                    txtbranch.Text = bName;
                                    txtaccess.Text = gAccessName;
                                    txtstart.Text = sDate;
                                    txtend.Text = eDate;
                                    txttraining.Text = aTraining;
                                    txttrainer.Text = tName;
                                }
                            }

                            EditText et = new EditText(this);

                            et.InputType = Android.Text.InputTypes.NumberVariationPassword |
                            Android.Text.InputTypes.ClassNumber;

                            AlertDialog.Builder ad = new AlertDialog.Builder(this);
                            ad.SetTitle("Account Verification");
                            ad.SetIcon(Resource.Drawable.User);
                            ad.SetMessage("Enter the last 4 digits of your Phone Number");
                            ad.SetView(et);
                            ad.SetNegativeButton("Cancel", delegate
                            {
                                ad.Dispose();
                            });
                            ad.SetPositiveButton("Ok", delegate {
                                if (string.Compare(et.Text, cNumber.Remove(0,7)) == 0)
                                {
                                    layoutInfo.Visibility = Android.Views.ViewStates.Visible;
                                    textViewError.Visibility = Android.Views.ViewStates.Invisible;
                                }
                                else {
                                    layoutInfo.Visibility = Android.Views.ViewStates.Invisible;
                                    textViewError.Visibility = Android.Views.ViewStates.Visible;
                                    textViewError.Text = "Account Verification Failed! ";
                                    editextRid.Text = "";
                                }
                                ad.Dispose();
                            });
                            ad.Show();

                           
                        }
                        else
                        {
                            Endloop();
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
            else
            {
                layoutInfo.Visibility = Android.Views.ViewStates.Invisible;
                textViewError.Visibility = Android.Views.ViewStates.Visible;
                textViewError.Text = "Invalid Input!";
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        public void Endloop()
        {
            layoutInfo.Visibility = Android.Views.ViewStates.Invisible;
            textViewError.Visibility = Android.Views.ViewStates.Visible;
            textViewError.Text = "Invalid Input!";
        }

    }
}