using BottomSheet;
using Xamarin.Forms;

namespace BottomSheetXF
{
    public class SimpleBottomSheet : BaseBottomSheet
    {
        private Label TitleLabel;

        private Label BodyLabel;

        private StackLayout RootLayout;

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(
                nameof(Title),
                typeof(string),
                typeof(SimpleBottomSheet),
                string.Empty,
                BindingMode.OneWay,
                null,
                propertyChanged: TitleChanged);
        
        public static readonly BindableProperty BodyProperty =
            BindableProperty.Create(
                nameof(Body),
                typeof(string),
                typeof(SimpleBottomSheet),
                string.Empty,
                BindingMode.OneWay,
                null,
                propertyChanged: BodyChanged);
        
        public static readonly BindableProperty MainColorProperty =
            BindableProperty.Create(
                nameof(MainColor),
                typeof(Color),
                typeof(SimpleBottomSheet),
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

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        
        public string Body
        {
            get => (string)GetValue(BodyProperty);
            set => SetValue(BodyProperty, value);
        }
        
        public Color MainColor
        {
            get => (Color)GetValue(MainColorProperty);
            set => SetValue(MainColorProperty, value);
        }
        
        public Thickness MainPadding
        {
            get => (Thickness)GetValue(MainPaddingProperty);
            set => SetValue(MainPaddingProperty, value);
        }

        public SimpleBottomSheet()
        {
            this.TitleLabel = new Label();
            this.BodyLabel = new Label();

            RootLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = MainColor,
                Padding = MainPadding,
            };

            RootLayout.Children.Add(TitleLabel);
            RootLayout.Children.Add(BodyLabel);

            this.View = new ContentView()
            {
                Content = RootLayout,
            };
        }

        private static void TitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SimpleBottomSheet)bindable;
            control.TitleLabel.Text = newValue as string;
        }

        private static void BodyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SimpleBottomSheet)bindable;
            control.BodyLabel.Text = newValue as string;
        }

        private static void MainColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SimpleBottomSheet)bindable;
            control.RootLayout.BackgroundColor = (Color)newValue;
        }

        private static void MainPaddingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SimpleBottomSheet)bindable;
            control.RootLayout.Padding = (Thickness)newValue;
        }
    }
}
