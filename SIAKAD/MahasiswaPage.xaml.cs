using SIAKAD.Data;
using SIAKAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIAKAD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MahasiswaPage : ContentPage
    {
        public MahasiswaPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ListMahasiswa.ItemsSource = await DatabaseHelper.getMahasiswa();
        }

        async void tambahData(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TambahMahasiswaPage());
        }

        private async void listMahasiswa(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) 
                return;

            var mahasiswa = (Mahasiswa)e.SelectedItem;
            string action = await DisplayActionSheet(
                "Pilih aksi",
                "Batal",
                null,
                "Detail",
                "Ubah",
                "Hapus");

            if (action == "Detail") {
                await Navigation.PushAsync(new DetailMahasiswaPage(mahasiswa));
            } else if (action == "Ubah") {
                await Navigation.PushAsync(new TambahMahasiswaPage(mahasiswa));
            } else if (action == "Hapus") {
                bool jawab = await DisplayAlert(
                    "Notifikasi Hapus",
                    $"Apakah anda yakin ingin menghapus data mahasiswa\n{mahasiswa.Nama}",
                    "Ya",
                    "Tidak");

                if (jawab){
                    if (!string.IsNullOrEmpty(mahasiswa.Foto) && System.IO.File.Exists(mahasiswa.Foto)) { 
                        System.IO.File.Delete(mahasiswa.Foto);
                    }

                    await DatabaseHelper.deleteMahasiswa(mahasiswa);
                    ListMahasiswa.ItemsSource = await DatabaseHelper.getMahasiswa();

                    await DisplayAlert(
                        "Berhasil",
                        "Data mahasiswa berhasil dihapus",
                        "Ok");
                }
            }

            ListMahasiswa.SelectedItem = null;
        }
    }
}