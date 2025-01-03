using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PamerYuk_Library;

namespace PamerYuk
{
    public partial class FormTeman : Form
    {
        private string usernameAktif;

        public FormTeman(string username)
        {
            InitializeComponent();
            usernameAktif = username;
        }

        private void FormTeman_Load(object sender, EventArgs e)
        {
            try
            {
                // Ambil daftar organisasi menggunakan class Organisasi
                List<Organisasi> daftarOrganisasi = Organisasi.BacaData("", "");

                // Tambahkan ke ComboBox
                foreach (Organisasi org in daftarOrganisasi)
                {
                    comboBoxOrganisasi.Items.Add(new KeyValuePair<int, string>(org.Id, org.Nama));
                }

                // Atur properti untuk menampilkan nama dan menyimpan ID
                comboBoxOrganisasi.DisplayMember = "Value"; // Nama organisasi
                comboBoxOrganisasi.ValueMember = "Key";    // ID organisasi
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar organisasi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxOrganisasi.SelectedItem == null)
                {
                    MessageBox.Show("Pilih organisasi terlebih dahulu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ambil ID organisasi yang dipilih
                int organisasiId = ((KeyValuePair<int, string>)comboBoxOrganisasi.SelectedItem).Key;

                // Cari teman berdasarkan ID organisasi
                List<User> listTeman = User.CariTeman(organisasiId.ToString(), usernameAktif);

                // Tampilkan hasil pencarian di ListView
                listViewTeman.Items.Clear();
                foreach (User teman in listTeman)
                {
                    ListViewItem item = new ListViewItem(teman.Username);
                    item.SubItems.Add(teman.Foto); // Foto sebagai sub-item
                    listViewTeman.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat mencari teman: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUndang_Click(object sender, EventArgs e)
        {
            if (listViewTeman.SelectedItems.Count > 0)
            {
                // Ambil username teman yang dipilih
                string temanDipilih = listViewTeman.SelectedItems[0].Text;

                // Tampilkan pesan undangan berhasil dikirim
                MessageBox.Show($"Undangan dikirim ke {temanDipilih}.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Pilih teman yang ingin diundang.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormTeman_Load_1(object sender, EventArgs e)
        {

        }
    }
}
