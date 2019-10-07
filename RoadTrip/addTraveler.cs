using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RoadTrip
{
    public partial class addTraveler : Form
    {
        public string picture_path, picture_name; //for picture path and name
        DataSet ds; //for database duplication
        AllTrips Trips; //for all trips info from database
        int i, num_rows; //i for counter and num_rows for number of rows in database

        public addTraveler()
        {
            InitializeComponent();
            Read_Trips_List(); ////for showing the trips list in the combo box
        }

        private void addTraveler_Load(object sender, EventArgs e)
        {
            //making a form non-resizable
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Read_Trips_List()
        {
            string StrSql1 = "select * from Trips";
            ds = DBConnect.ExecuteDataSet(StrSql1);
            num_rows = ds.Tables[0].Rows.Count;

            Trips = new AllTrips(num_rows);
            Trip trip = new Trip();
            for (i = 0; i < num_rows; i++)
            {
                trip = new Trip(Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                                    ds.Tables[0].Rows[i]["tripName"].ToString(),
                                    ds.Tables[0].Rows[i]["pic1"].ToString(),
                                    ds.Tables[0].Rows[i]["video1"].ToString(),
                                    Convert.ToSingle(ds.Tables[0].Rows[i]["cost1"]),
                                    ds.Tables[0].Rows[i]["date1"].ToString(),
                                    ds.Tables[0].Rows[i]["date2"].ToString(),
                                    ds.Tables[0].Rows[i]["date3"].ToString(),
                                    ds.Tables[0].Rows[i]["date4"].ToString(),
                                    Convert.ToInt32(ds.Tables[0].Rows[i]["time1"]));

                Trips.Add_Trips(trip);

                cbTrips.Items.Add(trip.get_tripName());
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bPic_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(open.FileName);
                    picture_name = open.SafeFileName;
                    picture_path = open.FileName;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed loading image");
            }
        }

        private void bAddTrip_Click(object sender, EventArgs e)
        {
            if (check())
            {
                string destinationFile = Application.StartupPath + @"..\..\..\data\images\" + picture_name;

                // To move a picture to a new location:
                System.IO.File.Copy(picture_path, destinationFile);

                DataSet ds = new DataSet();
                ds = DBConnect.ExecuteDataSet("select * from Travelers");
                DataRow dr = ds.Tables[0].NewRow();

                dr["travelerName"] = txtTravelerName.Text;
                dr["travelerID"] = txtID.Text;
                dr["address"] = txtAddress.Text;
                dr["phoneNumber"] = txtPhoneNumber.Text;
                dr["tripName"] = cbTrips.Text;
                dr["depDate"] = txtDepDate.Text;
                dr["pic1"] = picture_name;

                ds.Tables[0].Rows.Add(dr);
                DBConnect.UpdateTableInDataBase("select * from Travelers", ds);

                DialogResult ret =
                    MessageBox.Show("נוסף מטייל בהצלחה", "", MessageBoxButtons.OK, MessageBoxIcon.None);

                if (ret == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private bool check()
        {
            if (txtTravelerName.Text == "")
            {
                MessageBox.Show("עליך להכניס שם מטייל", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (txtID.Text == "")
            {
                MessageBox.Show("עליך להכניס מספר תעודת זהות", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (txtAddress.Text == "")
            {
                MessageBox.Show("עליך להכניס כתובת מגורים", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (txtPhoneNumber.Text == "")
            {
                MessageBox.Show("עליך להכניס מספר טלפון", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (cbTrips.Text == "")
            {
                MessageBox.Show("עליך לבחור טיול מרשימת הטיולים הקיימים במערכת", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (txtDepDate.Text == "")
            {
                MessageBox.Show("עליך להכניס תאריך יציאה מבוקש", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (picture_path == null)
            {
                MessageBox.Show("עליך לבחור תמונה למטייל", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            return true;
        }
    }
}
