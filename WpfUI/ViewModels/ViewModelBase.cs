using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace WpfUI.ViewModels
{
    public class ViewModelBase<T> 
        : ObservableObject
        , INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
