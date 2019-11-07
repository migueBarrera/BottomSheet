using Xamarin.Forms;

namespace BottomSheet
{
    public class BottomSheetDialog : Grid
    {
        public const uint ExpandAnimationSpeed = 350;

        public const uint CollapseAnimationSpeed = 250;

        private BoxView Fade;

        private double parentHeight;

        public static readonly BindableProperty ViewProperty = BindableProperty.Create(
            propertyName: nameof(View),
            returnType: typeof(ContentView),
            declaringType: typeof(BottomSheetDialog),
            defaultValue: null);

        public static readonly BindableProperty IsOpenProperty =
            BindableProperty.Create(
                nameof(IsOpen),
                typeof(bool),
                typeof(BottomSheetDialog),
                false,
                BindingMode.OneWay,
                null,
                propertyChanged: IsOpenChanged);

        public ContentView View
        {
            get => (ContentView)GetValue(ViewProperty);
            set => SetValue(ViewProperty, value);
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public double ParentHeight
        {
            get
            {
                return parentHeight;
            }

            set
            {
                parentHeight = value;
                this.TranslationY = parentHeight;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

            Fade = new BoxView()
            {
                BackgroundColor = Color.FromHex("#AA000000"),
                Opacity = 0,
            };

            this.RowSpacing = 0;
            this.Children.Add(Fade, 0, 0);
            Grid.SetRowSpan(Fade, 2);
            this.Children.Add(this.View, 0, 1);
        }

        public void Open()
        {
            Device.BeginInvokeOnMainThread(
               async ()
               =>
               {
                   await this.TranslateTo(0, 0, ExpandAnimationSpeed, Easing.SinInOut);
                   await Fade.FadeTo(1, ExpandAnimationSpeed * 2, Easing.SinInOut);
               });
        }

        public void Close(double height)
        {
            Device.BeginInvokeOnMainThread(
                async ()
                =>
                {
                    await Fade.FadeTo(0, CollapseAnimationSpeed / 2, Easing.SinInOut);
                    await this.TranslateTo(0, height, CollapseAnimationSpeed, Easing.SinInOut);
                });
        }

        private static void IsOpenChanged(Xamarin.Forms.BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as BottomSheetDialog;
            if (newValue is true)
            {
                control.Open();
            }
            else
            {
                control.Close(control.ParentHeight);
            }
        }
    }
}
