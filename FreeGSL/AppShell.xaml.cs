namespace FreeGSL
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Settings), typeof(Settings));
        }
    }
}