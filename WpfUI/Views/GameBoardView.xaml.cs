﻿using System.Configuration;
using System.Windows.Controls;

using CommunityToolkit.Mvvm.DependencyInjection;
using WpfUI.ViewModels;

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for GameBoardView.xaml
    /// </summary>
    public partial class GameBoardView : UserControl
    {
        public GameBoardViewModel viewModel;

        public GameBoardView()
        {
            viewModel = Ioc.Default.GetRequiredService<GameBoardViewModel>();
            DataContext = viewModel;

            InitializeComponent();
        }        
    }
}
