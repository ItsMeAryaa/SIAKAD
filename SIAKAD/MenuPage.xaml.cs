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
    public partial class MenuPage : Shell
    {
        public MenuPage()
        {
            InitializeComponent();

            Routing.RegisterRoute("beranda", typeof(BerandaPage));
            Routing.RegisterRoute("mahasiswa", typeof(MahasiswaPage));
            Routing.RegisterRoute("matkul", typeof(MaktulPage));
            Routing.RegisterRoute("khs", typeof(KhsPage));
            Routing.RegisterRoute("info", typeof(InfoPage));
            Routing.RegisterRoute("tentang", typeof(TentangPage));
            Routing.RegisterRoute("logout", typeof(LoginPage));
        }

        private async void logout(object sender, EventArgs e)
        {
            bool jawab = await DisplayAlert(
                "Logout",
                "Apakah anda yakin ingin keluar dari aplikasi?",
                "Ya",
                "Tidak");

            if (jawab)
            {
                Application.Current.MainPage = new LoginPage();
            }
        }
    }
}