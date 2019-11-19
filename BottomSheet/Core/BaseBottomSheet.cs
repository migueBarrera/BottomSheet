using BottomSheetXF.Core;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BottomSheet.Core
{
    public abstract class BaseBottomSheet : Grid
    {
        public const uint ExpandAnimationSpeed = 350;

        public const uint CollapseAnimationSpeed = 250;

        private BoxView Fade;

        public EventHandler OpenEvent;

        public EventHandler CloseEvent;

        public bool FadeBackgroundEnabled { get; set; } = true;

        public Movements Movement { get; set; } = Movements.BottomUp;

        public Positions ContentPosition { get; set; } = Positions.Bottom;

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

        public static readonly BindableProperty FadeColorProperty =
            BindableProperty.Create(
                nameof(FadeColor),
                typeof(Color),
                typeof(BaseBottomSheet),
                Color.FromHex("#AA000000"),
                BindingMode.OneWay,
                null,
                propertyChanged: FadeColorChanged);

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

        public Color FadeColor
        {
            get { return (Color)GetValue(FadeColorProperty); }
            set { SetValue(FadeColorProperty, value); }
        }

        public double ParentHeight { get; set; }

        public double ParentWidh { get; set; }

        public bool IsInitiated => ParentHeight > 0 && ParentWidh > 0;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            switch (ContentPosition)
            {
                case Positions.Bottom:
                    this.RowSpacing = 0;
                    RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    break;
                case Positions.Top:
                    this.RowSpacing = 0;
                    RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    break;
                case Positions.Left:
                    this.ColumnSpacing = 0;
                    ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    break;
                case Positions.Right:
                    this.ColumnSpacing = 0;
                    ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    break;
                default:
                    throw new SystemException($"{nameof(ContentPosition)} can not be null");
            }


            if (FadeBackgroundEnabled)
            {
                Fade = new BoxView()
                {
                    BackgroundColor = FadeColor,
                    Opacity = 0,
                };
                this.Children.Add(Fade, 0, 0);
                if (ContentPosition == Positions.Bottom || ContentPosition == Positions.Top)
                {
                    Grid.SetRowSpan(Fade, 2);
                }
                else
                {
                    Grid.SetColumnSpan(Fade, 2);
                }
            }

            switch (ContentPosition)
            {
                case Positions.Bottom:
                    this.Children.Add(this.View, 0, 1);
                    break;
                case Positions.Top:
                    this.Children.Add(this.View, 0, 0);
                    break;
                case Positions.Left:
                    this.Children.Add(this.View, 0, 0);
                    break;
                case Positions.Right:
                    this.Children.Add(this.View, 1, 0);
                    break;
                default:
                    throw new SystemException($"{nameof(ContentPosition)} can not be null");
            }
        }

        public void Open()
        {
            Device.BeginInvokeOnMainThread(
               async ()
               =>
               {
                   await TranslateToOpen();
                   if (FadeBackgroundEnabled)
                   {
                       await Fade.FadeTo(1, ExpandAnimationSpeed * 2, Easing.SinInOut);
                   }
               });
            OpenEvent?.Invoke(this, null);
        }

        public void Close()
        {
            Device.BeginInvokeOnMainThread(
                async ()
                =>
                {
                    if (FadeBackgroundEnabled)
                    {
                        await Fade.FadeTo(0, CollapseAnimationSpeed / 2, Easing.SinInOut);
                    }
                    await TranslateToClose();
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
                control.Close();
            }
        }

        private static void FadeColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as BaseBottomSheet;
            control.Fade.BackgroundColor = (Color)newValue;
        }

        public void Init(double parentHeight, double parentWidh)
        {
            ParentHeight = parentHeight;
            ParentWidh = parentWidh;

            if ((Movement == Movements.BottomUp || Movement == Movements.TopBottom) && ParentHeight <= 0)
            {
                throw new SystemException($"{nameof(ParentHeight)} is required if {nameof(Movement)} is TOP or BOTTOM");
            }

            if ((Movement == Movements.LeftRight || Movement == Movements.RightLeft) && ParentWidh <= 0)
            {
                throw new SystemException($"{nameof(ParentWidh)} is required if {nameof(Movement)} is LEFT or RIGHT");
            }

            switch (Movement)
            {
                case Movements.BottomUp:
                    this.TranslationY = ParentHeight;
                    break;
                case Movements.TopBottom:
                    this.TranslationY = -ParentHeight;
                    break;
                case Movements.LeftRight:
                    this.TranslationX = -ParentWidh;
                    break;
                case Movements.RightLeft:
                    this.TranslationX = ParentWidh;
                    break;
            }
        }

        internal Task TranslateToClose()
        {
            switch (Movement)
            {
                case Movements.BottomUp:
                    return this.TranslateTo(0, ParentHeight, CollapseAnimationSpeed, Easing.SinInOut);
                case Movements.TopBottom:
                    return this.TranslateTo(0, -ParentHeight, CollapseAnimationSpeed, Easing.SinInOut);
                case Movements.LeftRight:
                    return this.TranslateTo(-ParentWidh, 0, CollapseAnimationSpeed, Easing.SinInOut);
                case Movements.RightLeft:
                    return this.TranslateTo(ParentWidh, 0, CollapseAnimationSpeed, Easing.SinInOut);
                default:
                    throw new SystemException($"{nameof(Movement)} can not be null");
            }
        }

        internal Task TranslateToOpen()
        {
            switch (Movement)
            {
                case Movements.BottomUp:
                    return this.TranslateTo(0, 0, ExpandAnimationSpeed, Easing.SinInOut);
                case Movements.TopBottom:
                    return this.TranslateTo(0, 0, ExpandAnimationSpeed, Easing.SinInOut);
                case Movements.LeftRight:
                    return this.TranslateTo(0, 0, ExpandAnimationSpeed, Easing.SinInOut);
                case Movements.RightLeft:
                    return this.TranslateTo(0, 0, ExpandAnimationSpeed, Easing.SinInOut);
                default:
                    throw new SystemException($"{nameof(Movement)} can not be null");
            }
        }
    }
}
