using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuReserva : TabbedPage
    {
        public MenuReserva ()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}