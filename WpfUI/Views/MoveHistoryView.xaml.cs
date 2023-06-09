﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Presentation_WPF.ViewModels;

namespace Presentation_WPF.Views
{
    /// <summary>
    /// Interaction logic for MoveHistoryView.xaml
    /// </summary>
    public partial class MoveHistoryView : UserControl
    {
        MoveHistoryViewModel ViewModel { get; set; }

        public MoveHistoryView()
        {
            InitializeComponent();

            DataContext = ViewModel = new MoveHistoryViewModel();
        }
    }
}
