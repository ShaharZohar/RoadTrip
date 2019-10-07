using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RoadTrip
{
    public partial class addTrip : Form
    {
        public string picture_path, picture_name;
        public string video_path, video_name;

        public addTrip()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addTrip_Load(object sender, EventArgs e)
        {
            //making a form non-resizable
            this.FormBorderStyle = FormBorderStyle.FixedSingle; 
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Video Files (*.mp4)|*.mp4|Video Files (*.mpeg)|*.mpeg|Video Files (*.wmv)|*.wmv";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    txtVideo.Text = open.FileName;
                    video_name = open.SafeFileName;
                    video_path = open.FileName;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed loading video");
            }
        }

        private void bAddTrip_Click(object sender, EventArgs e)
        {
            if (check())
            {
                string destinationFile = Application.StartupPath + @"..\..\..\data\images\" + picture_name;
                string destinationFile2 = Application.StartupPath + @"..\..\..\data\videos\" + video_name;

                // To move a picture and a video to a new location:
                System.IO.File.Copy(picture_path, destinationFile);
                System.IO.File.Copy(video_path, destinationFile2);

                DataSet ds = new DataSet();
                ds = DBConnect.ExecuteDataSet("select * from Trips");
                DataRow dr = ds.Tables[0].NewRow();

                dr["tripName"] = txtTripName.Text;
                dr["date1"] = txtDate1.Text;
                dr["date2"] = txtDate2.Text;
                dr["date3"] = txtDate3.Text;
                dr["date4"] = txtDate4.Text;
                dr["video1"] = video_name;
                dr["cost1"] = Convert.ToSingle(txtCost.Text);
                dr["time1"] = Convert.ToInt64(txtTime.Text);
                dr["pic1"] = picture_name;

                ds.Tables[0].Rows.Add(dr);
                DBConnect.UpdateTableInDataBase("select * from Trips", ds);

                DialogResult ret = MessageBox.Show("נוסף טיול בהצלחה", "", MessageBoxButtons.OK, MessageBoxIcon.None);

                if (ret == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private bool check()
        {
            if (txtTripName.Text == "")
            {
                MessageBox.Show("עליך להכניס שם טיול", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (txtDate1.Text == "" && txtDate2.Text == "" && txtDate3.Text == "" && txtDate4.Text == "")
            {
                MessageBox.Show("עליך להכניס לפחות תאריך יציאה אחד", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (video_path == null)
            {
                MessageBox.Show("עליך לבחור סרטון וידיאו", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (txtCost.Text == "")
            {
                MessageBox.Show("עליך להכניס עלות טיול", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (txtTime.Text == "")
            {
                MessageBox.Show("עליך להכניס את משך הטיול", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (picture_path == null)
            {
                MessageBox.Show("עליך לבחור תמונה לטיול", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            return true;
        }
    }
}
