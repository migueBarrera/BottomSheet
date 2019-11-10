using System;
using System.Threading.Tasks;
using BottomSheet;
using Xamarin.Forms;

namespace BottomSheetXF
{
    public class ToastBottomSheet : BaseBottomSheet
    {
        private Label TitleLabel;
        private Image Image;

        private StackLayout RootLayout;

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(
                nameof(Title),
                typeof(string),
                typeof(ToastBottomSheet),
                string.Empty,
                BindingMode.OneWay,
                null,
                propertyChanged: TitleChanged);

        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(
                nameof(Icon),
                typeof(ImageSource),
                typeof(ToastBottomSheet),
                null,
                BindingMode.OneWay,
                null,
                propertyChanged: IconChanged);

        public static readonly BindableProperty MainColorProperty =
            BindableProperty.Create(
                nameof(MainColor),
                typeof(Color),
                typeof(ToastBottomSheet),
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
                typeof(ToastBottomSheet),
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

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
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

        public ToastBottomSheet()
        {
            this.TitleLabel = new Label();

            RootLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = MainColor,
                Padding = MainPadding,
            };

            Image = new Image()
            {
                Source = Icon,
            };
            RootLayout.Children.Add(Image);

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
            var control = (ToastBottomSheet)bindable;
            control.TitleLabel.Text = newValue as string;
        }

        private static void MainColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ToastBottomSheet)bindable;
            control.RootLayout.BackgroundColor = (Color)newValue;
        }

        private static void MainPaddingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ToastBottomSheet)bindable;
            control.RootLayout.Padding = (Thickness)newValue;
        }


        private static void IconChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ToastBottomSheet)bindable;
            Device.BeginInvokeOnMainThread(
               ()
               =>
               {
                   control.Image.Source = (ImageSource)newValue;
               });

        }
    }
}
