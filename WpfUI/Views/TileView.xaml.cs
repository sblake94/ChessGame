using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfUI.ViewModels;

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for TileView.xaml
    /// </summary>
    public partial class TileView : UserControl
    {
        TileViewModel ViewModel { get; set; }

        public static readonly DependencyProperty BoardRefProperty = DependencyProperty.Register(
        "BoardRef", typeof(string), typeof(TileView));

        public string BoardRef
        {
            get { return (string)GetValue(BoardRefProperty); }
            set { SetValue(BoardRefProperty, value); }
        }

        public TileView()
        {
            InitializeComponent();

            DataContext = ViewModel = new TileViewModel();
            Loaded += TileView_Loaded;
        }

        private void TileView_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is TileViewModel)
            {
                ViewModel.SetIndex(BoardRef);
                ViewModel.Refresh();
            }
        }

        private void TileView_MouseEnter(object sender, MouseEventArgs e)
        {
            ViewModel.MouseEnter();
        }

        private void TileView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.MouseDown();
        }

        private void TileView_MouseLeave(object sender, MouseEventArgs e)
        {
            ViewModel.MouseLeave();
        }
    }
}
