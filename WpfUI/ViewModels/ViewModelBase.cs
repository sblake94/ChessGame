using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace WpfUI.ViewModels
{
    /// <summary>
    /// Implements default behaviour for all ViewModels
    /// </summary>
    /// <typeparam name="T">Must inherit from ViewModelBase</typeparam>
    public abstract class ViewModelBase<T> 
        : ObservableObject
        , INotifyPropertyChanged
        where T : ViewModelBase<T>
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
