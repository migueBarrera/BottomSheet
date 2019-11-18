using System.ComponentModel;
using Xamarin.Forms;

namespace SampleBottomSheet
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private MainViewModel ViewModel;
        public MainPage()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();
            BindingContext = ViewModel;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            
            if (!SnackBarBottomSheet.IsInitiated)
            {
                SnackBarBottomSheet.Init(height, width);
            }
            
            if (!CustomBottomSheet.IsInitiated)
            {
                CustomBottomSheet.Init(height, width);
            }
            
            if (!SimpleBottomSheet.IsInitiated)
            {
                SimpleBottomSheet.Init(height, width);
            }
            
            if (!LeftCustomBottomSheet.IsInitiated)
            {
                LeftCustomBottomSheet.Init(height, width);
            }
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            ViewModel.IsOpenCustom = false;
            ViewModel.IsOpenSimpleSnackBar = false;
            ViewModel.IsOpenSimple = false;
            ViewModel.IsOpenLeftCustom = false;
        }
    }
}
