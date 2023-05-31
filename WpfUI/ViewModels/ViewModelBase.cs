using System.ComponentModel;

namespace WpfUI.ViewModels
{
    public class ViewModelBase<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
