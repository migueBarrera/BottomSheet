using System;
using System.Threading.Tasks;
using BottomSheet;
using Xamarin.Forms;

namespace BottomSheetXF
{
    public class SnackBarBottomSheet : BaseBottomSheet
    {
        private Label TitleLabel;

        private StackLayout RootLayout;

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(
                nameof(Title),
                typeof(string),
                typeof(SnackBarBottomSheet),
                string.Empty,
                BindingMode.OneWay,
                null,
                propertyChanged: TitleChanged);

        public static readonly BindableProperty MainColorProperty =
            BindableProperty.Create(
                nameof(MainColor),
                typeof(Color),
                typeof(SnackBarBottomSheet),
                Color.White,
                BindingMode.OneWay,
                null,
                propertyChanged: MainColorChanged);

        public static readonly BindableProperty MainPaddingProperty =
        BindableProperty.Create(
            nameof(MainPadding),
            typeof(Thickness),
            typeof(SimpleBottomSheet),
            new Thickness(),
            BindingMode.OneWay,
            null,
            propertyChanged: MainPaddingChanged);


        public static readonly BindableProperty DurationProperty =
            BindableProperty.Create(
                nameof(Duration),
                typeof(int),
                typeof(SnackBarBottomSheet),
                1000,
                BindingMode.OneWay,
                null);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public int Duration
        {
            get => (int)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public Thickness MainPadding
        {
            get => (Thickness)GetValue(MainPaddingProperty);
            set => SetValue(MainPaddingProperty, value);
        }

        public Color MainColor
        {
            get => (Color)GetValue(MainColorProperty);
            set => SetValue(MainColorProperty, value);
        }

        public SnackBarBottomSheet()
        {
            FadeBackgroundEnabled = false;
            this.TitleLabel = new Label();

            RootLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = MainColor,
                Padding = MainPadding,
            };

            RootLayout.Children.Add(TitleLabel);

            this.View = new ContentView()
            {
                Content = RootLayout,
            };

            this.OpenEvent += OnOpenEvent;
        }

        private async void OnOpenEvent(object sender, EventArgs e)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(Duration));
            IsOpen = false;
        }

        private static void TitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SnackBarBottomSheet)bindable;
            control.TitleLabel.Text = newValue as string;
        }

        private static void MainColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SnackBarBottomSheet)bindable;
            control.RootLayout.BackgroundColor = (Color)newValue;
        }

        private static void MainPaddingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SnackBarBottomSheet)bindable;
            control.RootLayout.Padding = (Thickness)newValue;
        }
    }
}
