using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PamerYuk_Library;

namespace PamerYuk
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            User user = User.CekLogin(username, password);

            if (user != null)
            {
                FormHome homeForm = new FormHome();
                homeForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login gagal. Username atau password salah.");
            }

        }
        

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegister registerForm = new FormRegister();
            registerForm.Show();
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
