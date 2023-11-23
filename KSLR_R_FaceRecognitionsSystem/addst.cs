using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
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



namespace KSLR_R_FaceRecognitionsSystem
{
    public partial class addst : Form
    {
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        DataTable dt;

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
        public addst()
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
        void user()
        {
            con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\richi\Documents\fibasklsk\HEHEE\assets\face.mdb");
            dt = new DataTable();
            //adapter = new OleDbDataAdapter("SELECT * FROM Employee", con);
            con.Open();
            adapter.Fill(dt);
            //dataGridView1.DataSource = dt;
            con.Close();

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) || string.IsNullOrWhiteSpace(guna2TextBox2.Text) || (guna2ComboBox1.SelectedIndex == -1) || string.IsNullOrWhiteSpace(guna2TextBox5.Text) || string.IsNullOrWhiteSpace(guna2TextBox6.Text))
            {
                MessageBox.Show("Fields should not be empty!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Add Student?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OleDbConnection conn = new OleDbConnection();
                    conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\richi\Documents\fibasklsk\HEHEE\assets\face.mdb";

                    OleDbCommand cmd = new OleDbCommand("INSERT into infu ([Name], Address, Gender, Birthday, [Section], StudentID) Values(@a, @b, @c, @d, @e, @f)");
                    cmd.Connection = conn;

                    conn.Open();

                    cmd.Parameters.AddWithValue("@a", guna2TextBox1.Text);
                    cmd.Parameters.AddWithValue("@b", guna2TextBox2.Text);
                    cmd.Parameters.AddWithValue("@c", guna2ComboBox1.SelectedItem);
                    cmd.Parameters.AddWithValue("@d", guna2DateTimePicker1.Value.ToShortDateString());
                    cmd.Parameters.AddWithValue("@e", guna2TextBox5.Text);
                    cmd.Parameters.AddWithValue("@f", guna2TextBox6.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Added to List!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();

                    //camera
                    Count += 1;
                    grayFace = camera.QueryGrayFrame().Resize(320, 240, INTER.CV_INTER_CUBIC);
                    MCvAvgComp[][] DetectedFace = grayFace.DetectHaarCascade(faceDetected, 1.2, 10,
                        HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

                    foreach (MCvAvgComp f in DetectedFace[0])
                    {

                        TrainedFace = Frame.Copy(f.rect).Convert<Gray, Byte>();
                        break;
                    }

                    TrainedFace = result.Resize(100, 100, INTER.CV_INTER_CUBIC);

                    trainingImages.Add(TrainedFace);
                    IBOutput.Image = TrainedFace;

                    labels.Add(guna2TextBox1.Text);

                    File.WriteAllText(Application.StartupPath + "/Faces/Faces.txt", trainingImages.ToArray().Length.ToString() + ",");

                    for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                    {
                        trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/Faces/face" + i + ".bmp");
                        File.AppendAllText(Application.StartupPath + "/Faces/Faces.txt", labels.ToArray()[i - 1] + ",");
                    }

                    MessageBox.Show("Face Stored.");





                    guna2TextBox1.Focus();
                    //txName.Clear();
                    guna2TextBox1.Text = "";
                    guna2TextBox2.Text = "";
                    guna2ComboBox1.SelectedIndex = -1;
                    //guna2TextBox4.Text = "";
                    guna2TextBox5.Text = "";
                    guna2TextBox6.Text = "";

                }
            }
        }

        private void bunifuTextbox1_MouseClick(object sender, MouseEventArgs e)
        {
           // bunifuTextbox1.text = "";    
        }

        private void bunifuTextbox2_MouseClick(object sender, MouseEventArgs e)
        {
            //bunifuTextbox2.text = "";
        }

        private void bunifuTextbox3_MouseClick(object sender, MouseEventArgs e)
        {
           // bunifuTextbox3.text = "";
        }

        private void bunifuTextbox4_MouseClick(object sender, MouseEventArgs e)
        {
           // bunifuTextbox4.text = "";
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            camera = new Capture();
            camera.QueryFrame();

            Application.Idle += new EventHandler(FrameProcedure);

            guna2Button1.Enabled = true;
            guna2Button2.Enabled = true;

            guna2TextBox1.Focus();
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
                names = names + users[nnn] + ", ";
            }

            //Show the faces procesed and recognized
            cameraBox.Image = Frame;
            //txName.Text = names;
            names = "";
            users.Clear();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            camera = new Capture();
            camera.QueryFrame();

            Application.Idle += new EventHandler(FrameProcedure);

            guna2Button1.Enabled = true;
            guna2Button2.Enabled = true;

            guna2TextBox1.Focus();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) || string.IsNullOrWhiteSpace(guna2TextBox2.Text) || (guna2ComboBox1.SelectedIndex == -1)  || string.IsNullOrWhiteSpace(guna2TextBox5.Text) || string.IsNullOrWhiteSpace(guna2TextBox6.Text))
            {
                MessageBox.Show("Fields should not be empty!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Add Student?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OleDbConnection conn = new OleDbConnection();
                    conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\richi\Documents\fibasklsk\HEHEE\assets\face.mdb";

                    OleDbCommand cmd = new OleDbCommand("INSERT into infu ([Name], Address, Gender, Birthday, [Section], StudentID) Values(@a, @b, @c, @d, @e, @f)");
                    cmd.Connection = conn;

                    conn.Open();

                    cmd.Parameters.AddWithValue("@a", guna2TextBox1.Text);
                    cmd.Parameters.AddWithValue("@b", guna2TextBox2.Text);
                    cmd.Parameters.AddWithValue("@c", guna2ComboBox1.SelectedItem);
                    cmd.Parameters.AddWithValue("@d", guna2DateTimePicker1.Value.ToShortDateString());
                    cmd.Parameters.AddWithValue("@e", guna2TextBox5.Text);
                    cmd.Parameters.AddWithValue("@f", guna2TextBox6.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Added to List!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();

                    //camera
                    Count += 1;
                    grayFace = camera.QueryGrayFrame().Resize(320, 240, INTER.CV_INTER_CUBIC);
                    MCvAvgComp[][] DetectedFace = grayFace.DetectHaarCascade(faceDetected, 1.2, 10,
                        HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

                    foreach (MCvAvgComp f in DetectedFace[0])
                    {

                        TrainedFace = Frame.Copy(f.rect).Convert<Gray, Byte>();
                        break;
                    }

                    TrainedFace = result.Resize(100, 100, INTER.CV_INTER_CUBIC);

                    trainingImages.Add(TrainedFace);
                    IBOutput.Image = TrainedFace;

                    labels.Add(guna2TextBox1.Text);

                    File.WriteAllText(Application.StartupPath + "/Faces/Faces.txt", trainingImages.ToArray().Length.ToString() + ",");

                    for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                    {
                        trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/Faces/face" + i + ".bmp");
                        File.AppendAllText(Application.StartupPath + "/Faces/Faces.txt", labels.ToArray()[i - 1] + ",");
                    }

                    MessageBox.Show("Face Stored!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    guna2TextBox1.Focus();
                    //txName.Clear();
                    guna2TextBox1.Text = "";
                    guna2TextBox2.Text = "";
                    guna2ComboBox1.SelectedIndex = -1;
                   // guna2TextBox4.Text = "";
                    guna2TextBox5.Text = "";
                    guna2TextBox6.Text = "";

                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            landing a = new landing();
            this.Hide();
            a.Show();
        }

        private void bunifuTextbox5_MouseClick(object sender, MouseEventArgs e)
        {
           // bunifuTextbox5.text = "";
        }

        private void bunifuTextbox6_MouseClick(object sender, MouseEventArgs e)
        {
            //bunifuTextbox6.text = "";
        }

        private void addst_Load(object sender, EventArgs e)
        {

        }
    }
}
