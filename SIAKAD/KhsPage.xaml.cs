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
    }
}