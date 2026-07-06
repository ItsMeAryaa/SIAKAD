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
    public partial class MaktulPage : ContentPage
    {
        public MaktulPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ListMK.ItemsSource = await DatabaseHelper.getMatkul();
        }

        async void tambahMatkul(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TambahMatkulPage());
        }

        async void listMatkul(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem == null)
                return;

            var matkul = (Matkul)e.SelectedItem;
            string action = await DisplayActionSheet(
                "Pilih Aksi", 
                "Batal", 
                null, 
                "Edit", 
                "Hapus"
            );

            if (action == "Edit")
            {
                await Navigation.PushAsync(new TambahMatkulPage(matkul));
            }
            else if (action == "Hapus")
            {
                bool confirm = await DisplayAlert(
                    "Konfirmasi Hapus", 
                    $"Apakah Anda yakin ingin menghapus matkul {matkul.Nama}?", 
                    "Ya", 
                    "Tidak"
                );
                if (confirm)
                {
                    await DatabaseHelper.deleteMatkul(matkul);
                    ListMK.ItemsSource = await DatabaseHelper.getMatkul();

                    await DisplayAlert(
                        "Berhasil", 
                        $"Matkul {matkul.Nama} berhasil dihapus.", 
                        "OK"
                    );
                }
            }

            ListMK.SelectedItem = null;
        }
    }
}