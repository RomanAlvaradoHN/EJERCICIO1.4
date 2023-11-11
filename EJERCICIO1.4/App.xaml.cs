using EJERCICIO1._4.Controllers;
using EJERCICIO1._4.Views;

namespace EJERCICIO1._4 {
    public partial class App : Application {

        public static readonly DBController db = new DBController();
        public static readonly string photosDirectory = Path.Combine(FileSystem.AppDataDirectory, "Fotos");



        public App() {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new CapturaDatos());
        }
    }
}