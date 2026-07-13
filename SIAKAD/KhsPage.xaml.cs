using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SIAKAD.Data;
using SIAKAD.Models;

namespace SIAKAD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KhsPage : ContentPage
    {
        public KhsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var data = await DatabaseHelper.getMatkul();
            cmbMatkul.ItemsSource = data;
            cmbMatkul.ItemDisplayBinding = new Binding("Nama");
        }

        private async void Hitung(object sender, EventArgs e)
        {
            try
            {
                double kuis = Convert.ToDouble(txtKuis.Text);
                double uts = Convert.ToDouble(txtUts.Text);
                double uas = Convert.ToDouble(txtUas.Text);
                double tugas = Convert.ToDouble(txtTugas.Text);

                double rata = (kuis + uts + uas + tugas) / 4;

                txtRata.Text = rata.ToString("0.00");

                string grade;

                if (rata >= 85)
                    grade = "A";
                else if (rata >= 70)
                    grade = "B";
                else if (rata >= 60)
                    grade = "C";
                else if (rata >= 50)
                    grade = "D";
                else
                    grade = "E";

                txtGrade.Text = grade;
            }
            catch
            {
                await DisplayAlert("Peringatan", "Masukkan semua nilai dengan benar. ", "OK");
            }
        }

        private async void txtNimCompleted(object sender, EventArgs e)
        {
            var nim = txtNim.Text;
            var mahasiswa = await DatabaseHelper.getMahasiswaByNim(nim);
            if (mahasiswa != null)
            {
                txtNama.Text = mahasiswa.Nama;
            }
            else
            {
                await DisplayAlert("Peringatan", "Mahasiswa tidak ditemukan.", "OK");
                txtNim.Focus();
            }
        }

        private async void Simpan(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNim.Text) ||
                string.IsNullOrWhiteSpace(txtNama.Text) ||
                cmbMatkul.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtKuis.Text) ||
                string.IsNullOrWhiteSpace(txtTugas.Text) ||
                string.IsNullOrWhiteSpace(txtUts.Text) ||
                string.IsNullOrWhiteSpace(txtUas.Text))
            {
                await DisplayAlert(
                    "Peringatan",
                    "Semua data harus diisi!!!",
                    "OK");
                return;
            }

            Matkul matkul = (Matkul)cmbMatkul.SelectedItem;
            bool sudahAda = await DatabaseHelper.CekKhs(txtNim.Text, matkul.Nama);

            if (sudahAda)
            {
                await DisplayAlert(
                    "Peringatan",
                    "Mahasiswa sudah mengambil mata kuliah tersebut!!",
                    "OK");
                return;
            }

            Khs khs = new Khs
            {
                Nim = txtNim.Text,
                Nama = txtNama.Text,
                NamaMatkul = matkul.Nama,

                Kuis = txtKuis.Text,
                Tugas = txtTugas.Text,
                Uts = txtUts.Text,
                Uas = txtUas.Text,

                Rata = txtRata.Text,
                Grade = txtGrade.Text
            };

            await DatabaseHelper.addKhs(khs);

            await DisplayAlert(
                "Sukses",
                "Data KHS berhasil disimpan!",
                "OK");

            BersihForm();
        }

        private void BersihForm()
        {
            cmbMatkul.SelectedItem = null;

            txtKuis.Text = "";
            txtTugas.Text = "";
            txtUts.Text = "";
            txtUas.Text = "";

            txtRata.Text = "";
            txtGrade.Text = "";

            txtNim.Focus();
        }

        private async void DataKhs(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataKhsPage());
        }
    }
}