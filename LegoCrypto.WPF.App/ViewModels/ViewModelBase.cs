using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LegoCrypto.WPF.App
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        {
            if (Equals(member, val)) return;

            member = val;
            NotifyPropertyChanged(propertyName);
        }
    }
}
