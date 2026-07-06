using SIAKAD.Data;
using SIAKAD.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIAKAD
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailMahasiswaPage : ContentPage
	{
		public DetailMahasiswaPage (Mahasiswa mahasiswa)
		{
			InitializeComponent ();
			lbNim.Text = mahasiswa.Nim;
			lbNama.Text = mahasiswa.Nama;
			lbProdi.Text = mahasiswa.Prodi;
			lbTelp.Text = mahasiswa.NoTelp;
			lbAlamat.Text = mahasiswa.Alamat;

			if (!string.IsNullOrEmpty(mahasiswa.Foto) && File.Exists(mahasiswa.Foto)) { 
				imgMahasiswa.Source = ImageSource.FromFile (mahasiswa.Foto);
			} else { 
				imgMahasiswa.Source= "mahasiswa";
			}
		}
	}
}