using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Library.Services;
using System.ComponentModel;

namespace Presentation_WPF.ViewModels
{
    /// <summary>
    /// Implements default behaviour for all ViewModels
    /// </summary>
    /// <typeparam name="T">Must inherit from ViewModelBase</typeparam>
    public abstract class ViewModelBase<T> 
        : ObservableObject
        where T : ViewModelBase<T>
    {
        protected readonly IChessLogicFacadeService _chessLogicFacadeService;

        public ViewModelBase()
        {
            _chessLogicFacadeService = Ioc.Default.GetRequiredService<IChessLogicFacadeService>();
        }
    }
}
