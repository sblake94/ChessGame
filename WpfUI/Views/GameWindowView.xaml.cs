﻿using System.Windows;
using Presentation_WPF.ViewModels;

namespace Presentation_WPF.Views
{
    /// <summary>
    /// Interaction logic for GameWindowView.xaml
    /// </summary>
    public partial class GameWindowView : Window
    {
        GameWindowViewModel ViewModel { get; set; }

        public GameWindowView()
        {
            InitializeComponent();

            DataContext = ViewModel = new GameWindowViewModel();
        }
    }
}
