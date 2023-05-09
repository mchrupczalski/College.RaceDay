using System;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaceDay.WpfUi.Services;
using RaceDay.WpfUi.StartUp;
using RaceDay.WpfUi.ViewModels;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace RaceDay.WpfUi;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    #region Static Fields and Const

    private static IHost _host = null!;

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
        mainWindow.DataContext = mainViewModel;
        mainWindow.Show();

        var navService = _host.Services.GetRequiredService<NavigationService>();
        navService.NavigateTo<HomeViewModel>();
    }

    #endregion

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
            MessageBox.Show(e.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _host.StopAsync();
        }
    }
}