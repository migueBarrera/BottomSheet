using System;
using Xamarin.Forms;

namespace BottomSheet
{
    public abstract class BaseBottomSheet : Grid
    {
        public const uint ExpandAnimationSpeed = 350;

        public const uint CollapseAnimationSpeed = 250;

        private BoxView Fade;

        private double parentHeight;

        public EventHandler OpenEvent;

        public EventHandler CloseEvent;

        public static readonly BindableProperty ViewProperty = BindableProperty.Create(
            propertyName: nameof(View),
            returnType: typeof(ContentView),
            declaringType: typeof(BaseBottomSheet),
            defaultValue: null);

        public static readonly BindableProperty IsOpenProperty =
            BindableProperty.Create(
                nameof(IsOpen),
                typeof(bool),
                typeof(BaseBottomSheet),
                false,
                BindingMode.TwoWay,
                null,
                propertyChanged: TitleChanged);

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
            OpenEvent?.Invoke(this, null);
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
            CloseEvent?.Invoke(this, null);
        }

        private static void TitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as BaseBottomSheet;
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
