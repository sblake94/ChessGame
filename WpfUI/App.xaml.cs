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
    public partial class App : System.Windows.Application
    {
        private IServiceProvider services;

        public App()
        {
            
            var ConfigurationResult = ServiceConfigurator.Configure();
            services = ConfigurationResult.Value;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = new GameWindowView();
            MainWindow.Show();
        }
    }
}
