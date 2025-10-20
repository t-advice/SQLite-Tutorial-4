using SQLiteTutorial4.Views;    

namespace SQLiteTutorial4
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ImageDetailPage), typeof(ImageDetailPage));
        }
    }
}
