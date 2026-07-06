using SIAKAD.Data;
using SIAKAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIAKAD
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TambahMahasiswaPage : ContentPage
	{
		string fotoPath = null;

		Mahasiswa mahasiswaEdit;
		bool isEdit = false;

		public TambahMahasiswaPage ()
		{
			InitializeComponent ();
			isEdit = false;
		}

        public TambahMahasiswaPage(Mahasiswa mahasiswa)
        {
            InitializeComponent();

			mahasiswaEdit = mahasiswa;
			isEdit = true;

			Title = "Ubah Data Mahasiswa";
			BtnSimpan.Text = "Ubah";
			BtnSimpan.BackgroundColor = Color.DarkCyan;
			BtnSimpan.TextColor = Color.White;

			txtNim.Text = mahasiswa.Nim;
			txtNama.Text = mahasiswa.Nama;
			txtProdi.Text = mahasiswa.Prodi;
			txtTelp.Text = mahasiswa.NoTelp;
			txtAlamat.Text = mahasiswa.Alamat;
			fotoPath = mahasiswa.Foto;

			if (!string.IsNullOrEmpty(fotoPath) && System.IO.File.Exists(fotoPath))
			{
				imgMahasiswa.Source = ImageSource.FromFile(fotoPath);
			}
			else
			{
				imgMahasiswa.Source = "mahasiswa";
			}
        }

        private async Task<string> simpanFoto(FileResult photo)
		{
			string folderFoto = Path.Combine(FileSystem.AppDataDirectory, "Foto");

			if (!Directory.Exists(folderFoto))
			{
				Directory.CreateDirectory(folderFoto);
			}

			string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
			string newPath = Path.Combine(folderFoto, fileName);

			using (var sourceStream = await photo.OpenReadAsync())
			using (var destinationStream = File.Create(newPath))
			{
				await sourceStream.CopyToAsync(destinationStream);
			}

			return newPath;
		}

        private async void btnPilihFoto(object sender, EventArgs e)
        {
			try
			{
				var photo = await MediaPicker.PickPhotoAsync();

				if (photo != null)
				{
					fotoPath = await simpanFoto(photo);
					imgMahasiswa.Source = ImageSource.FromFile(fotoPath);
				}
			}

			catch (Exception ex) 
			{
				await DisplayAlert("Error", ex.Message, "Ok");
			}
        }

        private async void btnSimpan(object sender, EventArgs e)
        {
			if (isEdit)
			{
				mahasiswaEdit.Nim = txtNim.Text;
				mahasiswaEdit.Nama = txtNama.Text;
				mahasiswaEdit.Prodi = txtProdi.Text;
				mahasiswaEdit.NoTelp = txtTelp.Text;
				mahasiswaEdit.Alamat = txtAlamat.Text;
				mahasiswaEdit.Foto = fotoPath ?? mahasiswaEdit.Foto;

				await DatabaseHelper.updateMahasiswa(mahasiswaEdit);
				await DisplayAlert("Sukses", "Data berhasil diubah", "Ok");
			}
			else
			{
				Mahasiswa mahasiswaBaru = new Mahasiswa()
				{
					Nim = txtNim.Text,
					Nama = txtNama.Text,
					Prodi = txtProdi.Text,
					NoTelp = txtTelp.Text,
					Alamat = txtAlamat.Text,
					Foto = fotoPath
				};

				await DatabaseHelper.addMahasiswa(mahasiswaBaru);
				await DisplayAlert("Sukses", "Data berhasil disimpan", "Ok");
			}

			await Navigation.PopAsync();
        }
    }
}