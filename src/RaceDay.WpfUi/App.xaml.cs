using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaceDay.WpfUi.StartUp;
using RaceDay.WpfUi.ViewModels;

namespace RaceDay.WpfUi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _host = null!;
        
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                _host = Bootstrapper.Bootstrap(args);
                _host.Start();
                
                var app = _host.Services.GetRequiredService<App>();
                app.InitializeComponent();
                app.Run();        
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                _host.StopAsync();
            }
        }

        #region Overrides of Application

        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();
        }

        #endregion
    }
}