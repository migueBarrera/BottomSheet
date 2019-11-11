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
            if (SimpleBottomSheet.ParentHeight == 0)
            {
                SimpleBottomSheet.ParentHeight = height;
            }
            
            if (CustomBottomSheet.ParentHeight == 0)
            {
                CustomBottomSheet.ParentHeight = height;
            }
            
            if (SnackBarBottomSheet.ParentHeight == 0)
            {
                SnackBarBottomSheet.ParentHeight = height;
            }
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            ViewModel.IsOpenCustom = false;
            ViewModel.IsOpenSimpleSnackBar = false;
            ViewModel.IsOpenSimple = false;
        }
    }
}
