using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Presentation_WPF.Common;
using Presentation_WPF.Views;

namespace Presentation_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider services;

        public App()
        {
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            services = ServiceConfigurator.Configure();

            MainWindow = new GameWindowView();
            MainWindow.Show();
        }
    }
}
