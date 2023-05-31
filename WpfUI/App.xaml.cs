using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WpfUI.Common;
using WpfUI.Views;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider services;

        public App()
        {
            services = ServiceConfigurator.Configure();
            MainWindow = services.GetRequiredService<GameWindowView>();
            MainWindow.Show();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = services.GetRequiredService<GameWindowView>();
            MainWindow.Show();
        }
    }
}
