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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnMasuk(object sender, EventArgs e)
        {
            string user = txtUsername.Text?.Trim();
            string pass = txtPassword.Text?.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                await DisplayAlert(
                    "Peringatan", 
                    "Username dan Password tidak boleh kosong", 
                    "OK");
                return;
            }

            if (user == "admin" && pass == "123")
            {
                Application.Current.MainPage = new MenuPage();
            }
            else
            {
                await DisplayAlert(
                    "Peringatan", 
                    "Username atau Password salah", 
                    "OK");
            }
        }
    }
}