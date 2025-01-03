using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing; // Tambahkan ini
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PamerYuk_Library;

namespace PamerYuk
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            // Initialization logic if needed
            try
            {
                // Ambil data kota dari database menggunakan metode BacaData
                List<Kota> listKota = Kota.BacaData("", "");

                // Tambahkan data kota ke ComboBox
                foreach (Kota kota in listKota)
                {
                    comboBoxKota.Items.Add(new KeyValuePair<int, string>(kota.Id, kota.Nama));
                }

                // Atur ComboBox untuk menampilkan nama kota
                comboBoxKota.DisplayMember = "Value"; // Nama kota
                comboBoxKota.ValueMember = "Key";    // ID kota
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat memuat data kota: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtUsername.Clear();
            textPwd.Clear();
            dateTimePickerDate.Value = DateTime.Now;
            txtNoKTP.Clear();
            txtFoto.Clear();
            pictureBoxFoto.Image = null;
            comboBoxKota.SelectedIndex = -1; // Reset ComboBox
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {
            // Handle group box enter event if needed
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Ambil data dari form
                string username = txtUsername.Text.Trim();
                string password = textPwd.Text.Trim();
                DateTime tglLahir = dateTimePickerDate.Value;
                string noKTP = txtNoKTP.Text.Trim();
                string foto = txtFoto.Text.Trim();

                // Validasi kota
                if (comboBoxKota.SelectedItem == null)
                {
                    MessageBox.Show("Harap pilih kota.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ambil id kota dari ComboBox
                int kotaId = ((KeyValuePair<int, string>)comboBoxKota.SelectedItem).Key;

                // Validasi field kosong
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(noKTP) || string.IsNullOrEmpty(foto))
                {
                    MessageBox.Show("Harap isi semua field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Simpan data ke database
                using (MySqlConnection koneksi = new MySqlConnection("Server=localhost;Database=erd_project;Uid=root;Pwd=;"))
                {
                    koneksi.Open();

                    string query = "INSERT INTO user (username, password, tglLahir, noKTP, foto, Kota_id) " +
                                   "VALUES (@username, @password, @tglLahir, @noKTP, @foto, @kotaId)";

                    using (MySqlCommand cmd = new MySqlCommand(query, koneksi))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password); // Encryption recommended
                        cmd.Parameters.AddWithValue("@tglLahir", tglLahir.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@noKTP", noKTP);
                        cmd.Parameters.AddWithValue("@foto", foto);
                        cmd.Parameters.AddWithValue("@kotaId", kotaId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registrasi berhasil!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Registrasi gagal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Handle panel paint event if needed
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            // Buka dialog File Explorer
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                openFileDialog.Title = "Pilih Foto";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Simpan path file ke TextBox
                    txtFoto.Text = openFileDialog.FileName;

                    // Tampilkan gambar di PictureBox
                    pictureBoxFoto.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }
    }
}
