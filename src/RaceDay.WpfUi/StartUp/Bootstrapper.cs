using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaceDay.SqlLite;
using RaceDay.SqlLite.Commands;
using RaceDay.SqlLite.Queries;
using RaceDay.WpfUi.Services;
using RaceDay.WpfUi.ViewModels;
using SQLite;

namespace RaceDay.WpfUi.StartUp;

public class Bootstrapper
{
    public static IHost Bootstrap(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
                       .ConfigureServices((context, services) =>
                        {
                            services.AddSingleton<App>();
                            services.AddSingleton<MainWindow>();
                            services.AddSingleton<MainViewModel>();

                            /* Services */
                            services.AddSingleton<NavigationService>();
                            services.AddSingleton<DialogService>();

                            /* ViewModels */
                            services.AddSingleton<HomeViewModel>(s => new HomeViewModel(s.GetRequiredService<RaceDaySummaryViewModel>(),
                                                                                        s.GetRequiredService<RaceDayRacesViewModel>(), s.GetRequiredService<NavigationService>(), s.GetRequiredService<DialogService>()));
                            services.AddSingleton<RaceDaySummaryViewModel>(s => new RaceDaySummaryViewModel(s.GetRequiredService<DaySummaryQuery>()));
                            services.AddSingleton<RaceDayRacesViewModel>(s => new RaceDayRacesViewModel(s.GetRequiredService<RaceSummaryQuery>()));
                            services.AddSingleton<NewRaceDayViewModel>();
                            services.AddSingleton<NewRaceViewModel>();

                            /* In SQLite Database */
                            string filesRoot = AppDomain.CurrentDomain.BaseDirectory;
                            string dbPath = System.IO.Path.Combine(filesRoot, "RaceDay.SqlLite.db");

                            /* Queries */
                            services.AddSingleton<DaySummaryQuery>(s => new DaySummaryQuery(dbPath));
                            services.AddSingleton<RaceSummaryQuery>(s => new RaceSummaryQuery(dbPath));

                            /* Commands */
                            services.AddSingleton<CreateDayCommand>(s => new CreateDayCommand(dbPath));
                            services.AddSingleton<CreateRaceCommand>(s => new CreateRaceCommand(dbPath));
                        })
                       .Build();

        return host;
    }
}