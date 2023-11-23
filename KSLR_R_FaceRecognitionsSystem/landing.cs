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

namespace KSLR_R_FaceRecognitionsSystem
{
    public partial class landing : Form
    {
        public landing()
        {
            InitializeComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2TextBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            guna2TextBox2.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            guna2ComboBox1.SelectedItem = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            //guna2TextBox4.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            guna2TextBox5.Text = guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            guna2TextBox6.Text = guna2DataGridView1.SelectedRows[0].Cells[6].Value.ToString();

            bunifuMaterialTextbox7.Text = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Visible = true;
            tbs();
            guna2DataGridView2.Visible = false;
            label2.Visible = true;

            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            guna2TextBox1.Visible = true;
            guna2TextBox2.Visible = true;
            guna2ComboBox1.Visible = true;
            guna2DateTimePicker1.Visible = true;
            guna2TextBox5.Visible = true;
            guna2TextBox6.Visible = true;

            bunifuFlatButton6.Visible = true;
            bunifuFlatButton7.Visible = true;

            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            guna2DataGridView2.Visible = true;
            tba();
            guna2DataGridView1.Visible = false;

            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
        }

        void tbs()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\richi\Documents\fibasklsk\HEHEE\assets\face.mdb";
            conn.Open();
            OleDbDataAdapter fl = new OleDbDataAdapter("select * from infu order by ID", conn);
            DataTable dt = new DataTable();
            fl.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            conn.Close();
        }

        void tba()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\richi\Documents\fibasklsk\HEHEE\assets\face.mdb";
            conn.Open();
            OleDbDataAdapter fl = new OleDbDataAdapter("select * from attnd order by ID", conn);
            DataTable dt = new DataTable();
            fl.Fill(dt);
            guna2DataGridView2.DataSource = dt;
            conn.Close();
        }

        private void landing_Load(object sender, EventArgs e)
        {
            tbs();
            guna2DataGridView2.Visible = false;

            guna2TextBox1.Visible = false;
            guna2TextBox2.Visible = false;
            guna2ComboBox1.Visible = false;
            guna2DateTimePicker1.Visible = false;
            guna2TextBox5.Visible = false;
            guna2TextBox6.Visible = false;
            label2.Visible = false;
            bunifuMaterialTextbox7.Visible = false;

            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;

            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bunifuMaterialTextbox7.Text))
            {
                MessageBox.Show("Please select a Student.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) || string.IsNullOrWhiteSpace(guna2TextBox2.Text) || (guna2ComboBox1.SelectedIndex == -1) || string.IsNullOrWhiteSpace(guna2TextBox5.Text) || string.IsNullOrWhiteSpace(guna2TextBox6.Text))
                {
                    MessageBox.Show("Fields should not be empty!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (MessageBox.Show("Update Student #" + bunifuMaterialTextbox7.Text + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        OleDbConnection conn = new OleDbConnection();
                        conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\richi\Documents\fibasklsk\HEHEE\assets\face.mdb";

                        OleDbCommand cmd = new OleDbCommand("UPDATE infu SET [Name] = @a, Address = @b, Gender = @c, Birthday = @d, [Section] = @e, StudentID = @f WHERE ID = @id");
                        cmd.Connection = conn;
                        conn.Open();

                        cmd.Parameters.AddWithValue("@a", guna2TextBox1.Text);
                        cmd.Parameters.AddWithValue("@b", guna2TextBox2.Text);
                        cmd.Parameters.AddWithValue("@c", guna2ComboBox1.SelectedItem);
                        cmd.Parameters.AddWithValue("@d", guna2DateTimePicker1.Value.ToShortDateString());
                        cmd.Parameters.AddWithValue("@e", guna2TextBox5.Text);
                        cmd.Parameters.AddWithValue("@f", guna2TextBox6.Text);
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(bunifuMaterialTextbox7.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Student Info Updated!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        tbs();

                        guna2TextBox1.Text = "";
                        guna2TextBox2.Text = "";
                        guna2ComboBox1.SelectedIndex = 1;
                        //guna2TextBox4.Text = "";
                        guna2TextBox5.Text = "";
                        guna2TextBox6.Text = "";
                        bunifuMaterialTextbox7.Text = "";
                    }
                }
            }
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bunifuMaterialTextbox7.Text))
            {
                MessageBox.Show("Please select a student.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Delete Student #" + bunifuMaterialTextbox7.Text + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OleDbConnection conn = new OleDbConnection();
                    conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\richi\Documents\fibasklsk\HEHEE\assets\face.mdb";

                    OleDbCommand cmd = new OleDbCommand("DELETE FROM infu WHERE ID = @id");
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(bunifuMaterialTextbox7.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Info Deleted!", "Alert ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    tbs();

                    guna2TextBox1.Text = "";
                    guna2TextBox2.Text = "";
                    guna2ComboBox1.SelectedIndex = 1;
                    //guna2TextBox4.Text = "";
                    guna2TextBox5.Text = "";
                    guna2TextBox6.Text = "";
                    bunifuMaterialTextbox7.Text = "";
                }
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                addst a = new addst();
                this.Hide();
                a.Show();
            }
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bunifuMaterialTextbox7.Text))
            {
                MessageBox.Show("Please select a Student.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) || string.IsNullOrWhiteSpace(guna2TextBox2.Text) || (guna2ComboBox1.SelectedIndex == -1) || string.IsNullOrWhiteSpace(guna2TextBox5.Text) || string.IsNullOrWhiteSpace(guna2TextBox6.Text))
                {
                    MessageBox.Show("Fields should not be empty!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (MessageBox.Show("Update Student #" + bunifuMaterialTextbox7.Text + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        OleDbConnection conn = new OleDbConnection();
                        conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\richi\Documents\fibasklsk\HEHEE\assets\face.mdb";

                        OleDbCommand cmd = new OleDbCommand("UPDATE infu SET [Name] = @a, Address = @b, Gender = @c, Birthday = @d, [Section] = @e, StudentID = @f WHERE ID = @id");
                        cmd.Connection = conn;
                        conn.Open();

                        cmd.Parameters.AddWithValue("@a", guna2TextBox1.Text);
                        cmd.Parameters.AddWithValue("@b", guna2TextBox2.Text);
                        cmd.Parameters.AddWithValue("@c", guna2ComboBox1.SelectedItem);
                        cmd.Parameters.AddWithValue("@d", guna2DateTimePicker1.Value.ToShortDateString());
                        cmd.Parameters.AddWithValue("@e", guna2TextBox5.Text);
                        cmd.Parameters.AddWithValue("@f", guna2TextBox6.Text);
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(bunifuMaterialTextbox7.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Student Info Updated!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        tbs();

                        guna2TextBox1.Text = "";
                        guna2TextBox2.Text = "";
                        guna2ComboBox1.SelectedIndex = 1;
                        //guna2TextBox4.Text = "";
                        guna2TextBox5.Text = "";
                        guna2TextBox6.Text = "";
                        bunifuMaterialTextbox7.Text = "";
                    }
                }
            }
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bunifuMaterialTextbox7.Text))
            {
                MessageBox.Show("Please select a student.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Delete Student #" + bunifuMaterialTextbox7.Text + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OleDbConnection conn = new OleDbConnection();
                    conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\richi\Documents\fibasklsk\HEHEE\assets\face.mdb";

                    OleDbCommand cmd = new OleDbCommand("DELETE FROM infu WHERE ID = @id");
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(bunifuMaterialTextbox7.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Info Deleted!", "Alert ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    tbs();

                    guna2TextBox1.Text = "";
                    guna2TextBox2.Text = "";
                    guna2ComboBox1.SelectedIndex = 1;
                    //guna2TextBox4.Text = "";
                    guna2TextBox5.Text = "";
                    guna2TextBox6.Text = "";
                    bunifuMaterialTextbox7.Text = "";
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2ComboBox1.SelectedIndex = 1;
            //guna2TextBox4.Text = "";
            guna2TextBox5.Text = "";
            guna2TextBox6.Text = "";
            bunifuMaterialTextbox7.Text = "";
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {
            login a = new login();
            this.Hide();
            a.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            login a = new login();
            this.Hide();
            a.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2ComboBox1.SelectedIndex = 1;
            //guna2TextBox4.Text = "";
            guna2TextBox5.Text = "";
            guna2TextBox6.Text = "";
            bunifuMaterialTextbox7.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {
            tbs();
            guna2DataGridView2.Visible = false;
            label2.Visible = false;
            guna2TextBox1.Visible = false;
            guna2TextBox2.Visible = false;
            guna2ComboBox1.Visible = false;
            guna2DateTimePicker1.Visible = false;
            guna2TextBox5.Visible = false;
            guna2TextBox6.Visible = false;

            bunifuMaterialTextbox7.Visible = false;

            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;

            bunifuFlatButton1.Visible = true;
            bunifuFlatButton2.Visible = true;
            bunifuFlatButton3.Visible = true;
            guna2DataGridView1.Visible = true;

            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
        }
    }
}
