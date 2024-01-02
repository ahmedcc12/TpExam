using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TpExam.Models;
using System.IO;
using System.Collections.Generic;

[assembly: ExportFont("Montserrat-Bold.ttf",Alias="Montserrat-Bold")]
     [assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat-Medium")]
     [assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat-Regular")]
     [assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat-SemiBold")]
     [assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
namespace TpExam
{
    public partial class App : Application
    {
        public static string ImageServerPath { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public static Commande CurrentOrder { get; set; }

        static BoutiqueDataBase database;
        public static BoutiqueDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Boutique.db3"));
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();

            // Log the database path
            Console.WriteLine($"Database path: {Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Boutique.db3")}");
            CurrentOrder = new Commande
            {
                NomClient = "ahmed",
                LignesCommande = new List<LigneCommande>()
            };

            MainPage = new MainPage();
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
