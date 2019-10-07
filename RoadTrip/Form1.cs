// programmed and designed by Shahar Zohar
// all rights reserved (c)
// this application programmed as a final project in C#
// 6.2013

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RoadTrip
{
    public partial class Form1 : Form
    {
        string companyName = "RoadTrip"; //for company name
        int companyLicense = 9; //for company license

        DataSet ds; //for database duplication
        AllTrips Trips; //for all trips info from database
        AllTravelers Travelers; //for all trips info from database
        string StrSql; //for sql string

        int i, num_rows, num_rows2; //i for counter, num_rows and num_rows2 for number of rows in database
        bool dataShow = false; //if groupBox3(database box) is showing or not
        bool searchTrip = false; //if groupBox1(searching box) is showing or not
        bool searchTrav = false; //if groupBox2(searching box) is showing or not
        string TripOrTravel = "Trip"; //if groupBox5 showing now trip info or travel info

        public Form1()
        {
            InitializeComponent();
            ShowTripsInDGV(); //for showing the trips info in the DGV
            Read_Trips_List(); //for showing the trips list in the combo boxes
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //making a form non-resizable
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }

        private void ShowTripsInDGV() //for showing the trips info in the DGV
        {
            TripOrTravel = "Trip";

            StrSql = "select * from Trips order by ID";
            ds = DBConnect.ExecuteDataSet(StrSql);

            dataGridView1.DataSource = ds.Tables[0];
        }

        private void Read_Trips_List() //for showing the trips list in the combo boxes
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

                //add trip name as an item in the comboboxes
                comboTrip2.Items.Add(trip.get_tripName());
                comboTrip1.Items.Add(trip.get_tripName());
            }
        }

        private void Read_Travelers_tbl(int id) //reading travelers data from Travelers table in the database
        {
            if (groupBox4.Visible == true)
                groupBox4.Visible = false;
            groupBox5.Visible = true;

            string StrSql1 = "select * from Travelers where ID=" + id;
            ds = DBConnect.ExecuteDataSet(StrSql1);
            num_rows2 = ds.Tables[0].Rows.Count;

            Travelers = new AllTravelers(num_rows2);
            Traveler traveler = new Traveler();
            for (i = 0; i < num_rows2; i++)
            {
                traveler = new Traveler(    Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                                            ds.Tables[0].Rows[i]["travelerName"].ToString(),
                                            ds.Tables[0].Rows[i]["travelerID"].ToString(),
                                            ds.Tables[0].Rows[i]["address"].ToString(),
                                            ds.Tables[0].Rows[i]["phoneNumber"].ToString(),
                                            ds.Tables[0].Rows[i]["tripName"].ToString(),
                                            ds.Tables[0].Rows[i]["depDate"].ToString(),
                                            ds.Tables[0].Rows[i]["pic1"].ToString()         );

                Travelers.Add_Travelers(traveler);
            }
            Show_Traveler(0);
        }

        private void Read_Trips_tbl(int id) //reading trips data from Trips table in the database
        {
            if (groupBox5.Visible == true)
                groupBox5.Visible = false;
            groupBox4.Visible = true;

            string StrSql1 = "select * from Trips where ID=" + id;
            ds = DBConnect.ExecuteDataSet(StrSql1);
            num_rows = ds.Tables[0].Rows.Count;

            Trips = new AllTrips(num_rows);
            Trip trip = new Trip();
            for (i = 0; i < num_rows; i++)
            {
                 trip = new Trip(    Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]), 
                                     ds.Tables[0].Rows[i]["tripName"].ToString(),
                                     ds.Tables[0].Rows[i]["pic1"].ToString(),
                                     ds.Tables[0].Rows[i]["video1"].ToString(),
                                     Convert.ToSingle(ds.Tables[0].Rows[i]["cost1"]),
                                     ds.Tables[0].Rows[i]["date1"].ToString(),
                                     ds.Tables[0].Rows[i]["date2"].ToString(),
                                     ds.Tables[0].Rows[i]["date3"].ToString(),
                                     ds.Tables[0].Rows[i]["date4"].ToString(),
                                     Convert.ToInt32(ds.Tables[0].Rows[i]["time1"])     );

                Trips.Add_Trips(trip);
            }
            Show_Trip(0);
        }

        private void Show_Trip(int ind) //showing a trip info
        {
          
            Trip trip = new Trip(Trips.Get_Trip_By_index(ind));
            
            lbl_ID.Text = trip.get_id().ToString();
            lbl_tripName.Text = trip.get_tripName();
            lbl_cost1.Text = trip.get_cost().ToString();
            lbl_departureDate.Text = trip.get_departureDate(0).ToString() + "   " + trip.get_departureDate(1).ToString() + "   " + trip.get_departureDate(2).ToString() + "   " + trip.get_departureDate(3).ToString();
            lbl_time1.Text = trip.get_time().ToString();
            pbTrip.Image = Image.FromFile(Application.StartupPath + @"..\..\..\data\images\" + trip.get_pic().ToString());

            //get video path to axWindowsMediaPlayer1(player)
            string path = Application.StartupPath;
            path = path.Replace(@"bin\Debug", @"data\videos\");
            axWindowsMediaPlayer1.URL = path + trip.get_video().ToString();  
        }

        private void Show_Traveler(int ind) //showing traveler info
        {

            Traveler traveler = new Traveler(Travelers.Get_Trav_By_index(ind));

            lbl_Trav_ID.Text = traveler.get_ID().ToString();
            lbl_travelerName.Text = traveler.get_travelerName().ToString();
            lbl_travelerID.Text = traveler.get_travelerID().ToString();
            lbl_Trav_address.Text = traveler.get_address().ToString();
            lbl_Trav_phoneNumber.Text = traveler.get_phoneNumber().ToString();
            lbl_Trav_tripName.Text = traveler.get_tripName().ToString();
            lbl_Trav_depDate.Text = traveler.get_depDate().ToString();
            pbTraveler.Image = Image.FromFile(Application.StartupPath + @"..\..\..\data\images\" + traveler.get_pic().ToString());
        }

        private void bShowData_Click(object sender, EventArgs e)
        {
            if (dataShow) //check if groupBox3(data box) is open or not
            {
                groupBox3.Visible = false;
                bShowData.Text = "הצג נתונים";
                dataShow = false;
            }
            else
            {
                groupBox3.Visible = true;
                bShowData.Text = "{ הסתר נתונים }";
                dataShow = true;
            }

            if (groupBox4.Visible == true)
                    groupBox4.Visible = false;
            else if(groupBox5.Visible == true)
                      groupBox5.Visible = false;
        }

        private void bSearchTrips_Click(object sender, EventArgs e)
        {
            if (searchTrip) //check if groupBox1(searching box) is open or not
            {
                groupBox1.Visible = false;
                bSearchTrips.Text = "חפש טיולים";
                searchTrip = false;
            }
            else
            {
                groupBox1.Visible = true;
                bSearchTrips.Text = "{ חפש טיולים }";
                searchTrip = true;
            }
        }

        private void bSearchTravelers_Click(object sender, EventArgs e)
        {
            if (searchTrav) //check if groupBox2(searching box) is open or not
            {
                groupBox2.Visible = false;
                bSearchTravelers.Text = "חפש מטיילים";
                searchTrav = false;
            }
            else
            {
                groupBox2.Visible = true;
                bSearchTravelers.Text = "{ חפש מטיילים }";
                searchTrav = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); //close the application
        }

        private void bAddTrip_Click(object sender, EventArgs e)
        {
            //open addTrip form
            addTrip f = new addTrip();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //open addTraveler form
            addTraveler f = new addTraveler();
            f.Show();
        }

        private void bSearchTrav_Click(object sender, EventArgs e)
        {
            TripOrTravel = "Traveler";

            //check if there is parameters for searching travelers
            if (txtID.Text == "" && comboTrip1.Text == "" && txtDate1.Text == "")
            {
                MessageBox.Show("עליך להכניס לפחות פרמטר אחד לחיפוש", "", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                if (txtID.Text != "")
                    StrSql = "select * from Travelers where travelerID = '" + txtID.Text + "' order by ID";

                else if (comboTrip1.Text != "")
                    StrSql = "select * from Travelers where tripName = '" + comboTrip1.Text + "' order by ID";

                else if (txtID.Text != "" && comboTrip1.Text != "")
                    StrSql = "select * from Travelers where travelerID = '" + txtID.Text + "' and tripName = '" + comboTrip1.Text + "' order by ID";

                else if (txtDate1.Text != "")
                    StrSql = "select * from Travelers where depDate = '" + txtDate1.Text + "' order by ID";

                else if (txtID.Text != "" && comboTrip1.Text != "" && txtDate1.Text != "")
                    StrSql = "select * from Travelers where travelerID = '" + txtID.Text + "' and tripName = '" + comboTrip1.Text + "' and depDate = '" + txtDate1.Text + "' order by ID";

                groupBox3.Text = "מטיילים  ";
                ds = DBConnect.ExecuteDataSet(StrSql);

                dataGridView1.DataSource = ds.Tables[0];

                if (!dataShow)
                {
                    groupBox3.Visible = true;
                    bShowData.Text = "{ הסתר נתונים }";
                    dataShow = true;
                }
            }
        }

        private void bSearchTrip_Click_1(object sender, EventArgs e)
        {
            TripOrTravel = "Trip";

            //check if there is parameters for searching trips
            if (comboTrip2.Text == "" && txtDate2.Text == "" && txtDays.Text == "")
            {
                MessageBox.Show("עליך להכניס לפחות פרמטר אחד לחיפוש", "", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                if (comboTrip2.Text != "")
                    StrSql = "select * from Trips where tripName = '" + comboTrip2.Text + "' order by ID";

                else if (comboTrip2.Text != "" && txtDate2.Text != "")
                    StrSql = "select * from Trips where tripName = '" + comboTrip2.Text + "' and date1 = '" + txtDate2.Text + "' or date2 = '" + txtDate2.Text + "' or date3 = '" + txtDate2.Text + "' or date4 = '" + txtDate2.Text + "' order by ID";

                else if (comboTrip2.Text != "" && txtDate2.Text != "" && txtDays.Text != "")
                    StrSql = "select * from Trips where tripName = '" + comboTrip2.Text + "' and date1 = '" + txtDate2.Text + "' or date2 = '" + txtDate2.Text + "' or date3 = '" + txtDate2.Text + "' or date4 = '" + txtDate2.Text + "' and time1 <= " + Convert.ToInt64(txtDays.Text) + " order by ID";

                else if (txtDate2.Text != "")
                    StrSql = "select * from Trips where date1 = '" + txtDate2.Text + "' or date2 = '" + txtDate2.Text + "' or date3 = '" + txtDate2.Text + "' or date4 = '" + txtDate2.Text + "' order by ID";

                else if (txtDays.Text != "")
                    StrSql = "select * from Trips where time1 <= " + Convert.ToInt64(txtDays.Text) + " order by ID";

                groupBox3.Text = "טיולים";
                ds = DBConnect.ExecuteDataSet(StrSql);

                dataGridView1.DataSource = ds.Tables[0];

                if (!dataShow)
                {
                    groupBox3.Visible = true;
                    bShowData.Text = "{ הסתר נתונים }";
                    dataShow = true;
                }
            }
        }

        private void bShowTrips_Click(object sender, EventArgs e)
        {
            TripOrTravel = "Trip";
            
            groupBox3.Text = "טיולים";
            StrSql = "select * from Trips order by ID";
            ds = DBConnect.ExecuteDataSet(StrSql);

            dataGridView1.DataSource = ds.Tables[0];
        }

        private void bShowTravelers_Click(object sender, EventArgs e)
        {
            TripOrTravel = "Traveler";
            
            groupBox3.Text = "מטיילים";
            StrSql = "select * from Travelers order by ID";
            ds = DBConnect.ExecuteDataSet(StrSql);

            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            bSearchTrips.Text = "חפש טיולים";
            searchTrip = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            bSearchTravelers.Text = "חפש מטיילים";
            searchTrav = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            bShowData.Text = "הצג נתונים";
            dataShow = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows[e.RowIndex].Cells.Count; i++)
            {
                dataGridView1[i, e.RowIndex].Style.BackColor = Color.GreenYellow;
            }

            if (TripOrTravel == "Trip") //check if groupBox5 showing now trip info or travel info
            {
                if (groupBox5.Visible == true)
                    groupBox5.Visible = false;
                groupBox4.Visible = true;
                Read_Trips_tbl(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value)); //get row index for get trip info
            }
            else
            {
                if (groupBox4.Visible == true)
                    groupBox4.Visible = false;
                groupBox5.Visible = true; 
                Read_Travelers_tbl(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value)); //get row index for get trip info
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox5.Visible = false;
        }

        private void bPlayVideo_Click(object sender, EventArgs e)
        {
            //play video
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void bStopVideo_Click(object sender, EventArgs e)
        {
            //pause video
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void pbTraveler_Click(object sender, EventArgs e)
        {

        }

    }
}
