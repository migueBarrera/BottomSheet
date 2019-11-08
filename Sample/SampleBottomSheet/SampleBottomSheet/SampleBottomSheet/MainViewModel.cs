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
        private bool isOpen;
        public bool IsOpen
        {
            get => isOpen;
            set => SetAndRaisePropertyChanged(ref isOpen, value);
            }

        public ICommand ClickCommand => new Command(ClickCommandExecute);

        public event PropertyChangedEventHandler PropertyChanged;

        private void ClickCommandExecute()
        {
            IsOpen = true;
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
