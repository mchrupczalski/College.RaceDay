using System;
using System.Windows.Controls;

namespace RaceDay.WpfUi.Views;

public partial class RaceDaySummaryView : UserControl
{
    public RaceDaySummaryView()
    {
        InitializeComponent();
        Initialized += RaceDaySummaryView_Initialized;
    }

    private void RaceDaySummaryView_Initialized(object? sender, EventArgs e)
    {
        var dc = DataContext as ViewModels.RaceDaySummaryViewModel;
        dc?.LoadData();
    }
}