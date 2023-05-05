using System;
using System.Windows.Controls;

namespace RaceDay.WpfUi.Views;

public partial class DaySummaryView : UserControl
{
    public DaySummaryView()
    {
        InitializeComponent();
        Initialized += RaceDaySummaryView_Initialized;
    }

    private void RaceDaySummaryView_Initialized(object? sender, EventArgs e)
    {
        var dc = DataContext as ViewModels.DaySummaryViewModel;
        dc?.LoadData();
    }
}