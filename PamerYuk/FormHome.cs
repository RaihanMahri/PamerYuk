using PamerYuk_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PamerYuk
{
    public partial class FormHome : Form
    {
        public FormHome()
        {
            InitializeComponent();
        }

        public User userLogin;

        private void FormHome_Load(object sender, EventArgs e)
        {
            FormLogin frm = new FormLogin();
            frm.Owner = this;
            this.Visible = false;
            frm.ShowDialog();

            if (userLogin != null)
            {
                this.Visible = true;
            }
            else
            {
                MessageBox.Show("Aplikasi akan ditutup karena login gagal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void buttonPencarian_Click(object sender, EventArgs e)
        {
            // Pastikan userLogin tidak null sebelum mengakses Username
            if (userLogin != null)
            {
                FormTeman frm = new FormTeman(userLogin.Username); // Kirimkan username pengguna yang sedang login
                frm.Owner = this;
                this.Visible = false;
                frm.ShowDialog();
                this.Visible = true; // Tampilkan kembali FormHome setelah FormTeman ditutup
            }
            else
            {
                MessageBox.Show("Pengguna belum login.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
