using SIAKAD.Data;
using SIAKAD.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIAKAD
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TambahMatkulPage : ContentPage
	{
		Matkul matkulEdit;
		bool isEdit = false;

		public TambahMatkulPage ()
		{
			InitializeComponent ();
			isEdit = false;
		}

        public TambahMatkulPage(Matkul matkul)
        {
            InitializeComponent();
            
			matkulEdit = matkul;
			isEdit = true;

			Title = "Ubah Data Matkul";
			btnSimpan.Text = "Ubah";
			btnSimpan.BackgroundColor = Color.DarkCyan;
			btnSimpan.TextColor = Color.White;

			txtKode.Text = matkul.Kode;
			txtNama.Text = matkul.Nama;
			txtSks.Text = matkul.Sks;
        }

        private async void BtnSimpan(object sender, EventArgs e)
        {
			if (isEdit)
			{
				matkulEdit.Kode = txtKode.Text;
				matkulEdit.Nama = txtNama.Text;
				matkulEdit.Sks = txtSks.Text;

				await DatabaseHelper.updateMatkul(matkulEdit);
				await DisplayAlert("Sukses", "Data berhasil diubah", "Ok");
			}
			else
			{
				Matkul matkulBaru = new Matkul()
				{
					Kode = txtKode.Text,
					Nama = txtNama.Text,
					Sks = txtSks.Text
				};

				await DatabaseHelper.addMatkul(matkulBaru);
				await DisplayAlert("Sukses", "Data berhasil disimpan", "Ok");
			}

			await Navigation.PopAsync();
        }
    }
}