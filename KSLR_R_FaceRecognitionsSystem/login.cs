using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSLR_R_FaceRecognitionsSystem
{
    public partial class login : Form
    {
        
        public login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if ((guna2TextBox1.Text == "admin") && (guna2TextBox2.Text == "admin"))
            {
                MessageBox.Show("Log in succesful.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                landing a = new landing();
                this.Hide();
                a.Show();
            }
            else
            {
                MessageBox.Show("Log in failed.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuTextbox2_OnTextChange(object sender, EventArgs e)
        {
        }

        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {
            
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if ((guna2TextBox1.Text == "admin") && (guna2TextBox2.Text == "admin"))
            {
                MessageBox.Show("Log in succesful.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                landing a = new landing();
                this.Hide();
                a.Show();
            }
            else
            {
                MessageBox.Show("Log in failed.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            scan a = new scan();
            this.Hide();
            a.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
