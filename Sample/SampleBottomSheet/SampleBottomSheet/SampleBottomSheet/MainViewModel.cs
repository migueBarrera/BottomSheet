using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampleBottomSheet
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool isOpenSimple;
        public bool IsOpenSimple
        {
            get => isOpenSimple;
            set => SetAndRaisePropertyChanged(ref isOpenSimple, value);
        }
        
        private bool isOpenSnackBar;
        public bool IsOpenSimpleSnackBar
        {
            get => isOpenSnackBar;
            set => SetAndRaisePropertyChanged(ref isOpenSnackBar, value);
        }
        
        private bool isOpenCustom;
        public bool IsOpenCustom
        {
            get => isOpenCustom;
            set => SetAndRaisePropertyChanged(ref isOpenCustom, value);
        }

        public ICommand ClickCommand => new Command<string>(ClickCommandExecute);

        public event PropertyChangedEventHandler PropertyChanged;

        private void ClickCommandExecute(string parameter)
        {
            switch (parameter)
            {
                case "1":
                    IsOpenSimple = true;
                    break;
                case "2":
                    IsOpenSimpleSnackBar = true;
                    break;
                case "3":
                    IsOpenCustom = true;
                    break;

            }
        }

        protected void SetAndRaisePropertyChanged<TRef>(
     ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetAndRaisePropertyChangedIfDifferentValues<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
            where TRef : class
        {
            if (field == null || !field.Equals(value))
            {
                SetAndRaisePropertyChanged(ref field, value, propertyName);
            }
        }
    }
}
