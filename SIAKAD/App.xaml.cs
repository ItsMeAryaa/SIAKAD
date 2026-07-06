using SIAKAD.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIAKAD
{
    public partial class App : Application
    {
        public static DatabaseHelper DBSiakad { get; private set; }
        public App()
        {
            InitializeComponent();

            DBSiakad = new DatabaseHelper();
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
