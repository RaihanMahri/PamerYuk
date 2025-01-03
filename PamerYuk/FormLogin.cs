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
            FormHome frm = (FormHome)this.Owner;
            string uid = textBoxUsername.Text;
            string pwd = textBoxPassword.Text;
            frm.userLogin = User.CekLogin(uid, pwd);

            if (frm.userLogin is null)
            {
                MessageBox.Show("Login gagal. Username atau password salah.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show($"Selamat datang, {frm.userLogin.Username}!", "Login Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frm.Visible = true; // Pastikan FormHome terlihat
                this.Close(); // Tutup FormLogin
            }

        }
        

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegister registerForm = new FormRegister();
            registerForm.Show();
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
    }
}
