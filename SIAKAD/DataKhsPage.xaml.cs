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
	public partial class DataKhsPage : ContentPage
	{
		public DataKhsPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ListKhs.ItemsSource = await DatabaseHelper.getKhsGroup();
        }

        private async void listKhs(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var khs = (Khs)e.SelectedItem;
            string action = await DisplayActionSheet(
                "Pilih aksi",
                "Batal",
                null,
                "Detail",
                "Hapus");

            if (action == "Detail")
            {
                //await Navigation.PushAsync(new DetailKhsPage());
            }
            else if (action == "Hapus")
            {
                bool jawab = await DisplayAlert(
                    "Konfirmasi Hapus",
                    $"Apakah anda yakin ingin menghapus data KHS\n{khs.Nama}",
                    "Ya",
                    "Tidak");

                if (jawab)
                {
                    await DatabaseHelper.deleteKhsByNim(khs.Nim);

                    ListKhs.ItemsSource = await DatabaseHelper.getKhsGroup();

                    await DisplayAlert(
                        "Berhasil",
                        "Data KHS berhasil dihapus!",
                        "OK");
                }
            }

            ListKhs.SelectedItem = null;
        }
    }
}