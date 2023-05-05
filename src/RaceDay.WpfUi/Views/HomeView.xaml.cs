using System;
using System.Windows.Controls;

namespace RaceDay.WpfUi.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        Initialized += HomeView_Initialized;
    }

    private void HomeView_Initialized(object? sender, EventArgs e)
    {
        var dc = DataContext as ViewModels.HomeViewModel;
        dc?.RaceDaySummaryViewModel.LoadData();
    }
}