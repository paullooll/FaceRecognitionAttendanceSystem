using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Diagnostics;
using System.IO;
using System.Data.OleDb;

namespace KSLR_R_FaceRecognitionsSystem
{
    public partial class scan : Form
    {
        //Variables 
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.6d, 0.6d);

        //HaarCascade Library
        HaarCascade faceDetected;

        //For Camera as WebCams 
        Capture camera;

        //Images List if Stored
        Image<Bgr, Byte> Frame;

        Image<Gray, byte> result;
        Image<Gray, byte> TrainedFace = null;
        Image<Gray, byte> grayFace = null;

        //List 
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();

        List<string> labels = new List<string>();
        List<string> users = new List<string>();

        int Count, NumLables, t;
        string name, names = null;

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if ((lblName.Text == "") || (lblName.Text == "Nobody"))
            {
                MessageBox.Show("Could not time in", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Add Attendance?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OleDbConnection conn = new OleDbConnection();
                    conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\richi\Documents\fibasklsk\HEHEE\assets\face.mdb";

                    OleDbCommand cmd = new OleDbCommand("INSERT into attnd ([Name], [Time]) Values(@a, @b)");
                    cmd.Connection = conn;

                    conn.Open();

                    cmd.Parameters.AddWithValue("@a", lblName.Text);
                    cmd.Parameters.AddWithValue("@b", label2.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendance Added", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("MMMM dd, yyyy - h:mm:ss tt");
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if ((lblName.Text == "") || (lblName.Text == "Nobody"))
            {
                MessageBox.Show("Could not time in", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Add Attendance?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OleDbConnection conn = new OleDbConnection();
                    conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\richi\Documents\fibasklsk\HEHEE\assets\face.mdb";

                    OleDbCommand cmd = new OleDbCommand("INSERT into attnd ([Name], [Time]) Values(@a, @b)");
                    cmd.Connection = conn;

                    conn.Open();

                    cmd.Parameters.AddWithValue("@a", lblName.Text);
                    cmd.Parameters.AddWithValue("@b", label2.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendance Added", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {
            login a = new login();
            this.Hide();
            a.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            login a = new login();
            this.Hide();
            a.Show();
        }

        public scan()
        {
            InitializeComponent();
            faceDetected = new HaarCascade("haarcascade_frontalface_alt.xml");

            try
            {
                string Labelsinf = File.ReadAllText(Application.StartupPath + "/Faces/Faces.txt");
                string[] Labels = Labelsinf.Split(',');

                NumLables = Convert.ToInt16(Labels[0]);
                Count = NumLables;

                string FacesLoad;

                for (int i = 1; i < NumLables + 1; i++)
                {
                    FacesLoad = "face" + i + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/Faces/" + FacesLoad));
                    labels.Add(Labels[i]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please register a face!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrameProcedure(object sender, EventArgs e)
        {
            users.Add("");
            //lblCountAllFaces.Text = "0";

            Frame = camera.QueryFrame().Resize(426, 320, INTER.CV_INTER_CUBIC);
            grayFace = Frame.Convert<Gray, Byte>();

            MCvAvgComp[][] faceDetectedShow = grayFace.DetectHaarCascade(faceDetected, 1.2, 10,
                HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

            foreach (MCvAvgComp f in faceDetectedShow[0])
            {
                t += 1;

                result = Frame.Copy(f.rect).Convert<Gray, Byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                Frame.Draw(f.rect, new Bgr(Color.Green), 3);

                if (trainingImages.ToArray().Length != 0)
                {
                    MCvTermCriteria termCriterias = new MCvTermCriteria(Count, 0.001);
                    EigenObjectRecognizer recognizer =
                        new EigenObjectRecognizer(trainingImages.ToArray(),
                        labels.ToArray(), 3000,
                        ref termCriterias);

                    name = recognizer.Recognize(result);
                    Frame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.Red));

                }
                users[t - 1] = name;
                users.Add("");
                //Set the number of faces detected on the scene
                //lblCountAllFaces.Text = faceDetectedShow[0].Length.ToString();
                users.Add("");

            }

            t = 0;

            //Names concatenation of persons recognized
            for (int nnn = 0; nnn < faceDetectedShow[0].Length; nnn++)
            {
                names = names + users[nnn] + "";
            }

            //Show the faces procesed and recognized
            cameraBox.Image = Frame;
            lblName.Text = names;
            names = "";
            users.Clear();
        }



        private void scan_Load(object sender, EventArgs e)
        {
            camera = new Capture();
            camera.QueryFrame();

            Application.Idle += new EventHandler(FrameProcedure);

            guna2Button2.Enabled = true;
            //bunifuFlatButton2.Enabled = true;

            //bunifuMaterialTextbox1.Focus();
        }
    }
}
