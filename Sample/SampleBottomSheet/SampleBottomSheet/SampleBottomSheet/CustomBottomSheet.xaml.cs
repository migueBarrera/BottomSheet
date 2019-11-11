using Xamarin.Forms.Xaml;

namespace SampleBottomSheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomBottomSheet
    {
        public CustomBottomSheet()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            this.IsOpen = false;
        }
    }
}