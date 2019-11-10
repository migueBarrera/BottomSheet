using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            
            if (ToastBottomSheet.ParentHeight == 0)
            {
                ToastBottomSheet.ParentHeight = height;
            }
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            ViewModel.IsOpen = false;
        }
    }
}
