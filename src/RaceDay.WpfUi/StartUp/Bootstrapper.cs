using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaceDay.SqlLite.Commands;
using RaceDay.SqlLite.Queries;
using RaceDay.WpfUi.Services;
using RaceDay.WpfUi.ViewModels;

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
                            services.AddSingleton<HomeViewModel>(s => new HomeViewModel(s.GetRequiredService<DaySummaryViewModel>(),
                                                                                        s.GetRequiredService<RacesSummaryViewModel>()));
                            services.AddSingleton<DaySummaryViewModel>(
                                s => new DaySummaryViewModel(s.GetRequiredService<DialogService>(), s.GetRequiredService<DaySummaryQuery>()));
                            services.AddSingleton<RacesSummaryViewModel>(s => new RacesSummaryViewModel(s.GetRequiredService<DialogService>(),
                                                                                                        s.GetRequiredService<NavigationService>(),
                                                                                                        s.GetRequiredService<RaceSummaryQuery>(),
                                                                                                        s.GetRequiredService<RaceViewModel.CreateRaceViewModel>()));
                            services.AddSingleton<NewRaceDayViewModel>();
                            services.AddSingleton<NewRaceViewModel>();
                            services.AddSingleton<AddRacerViewModel>();

                            /* Factories */
                            services.AddSingleton<RacerViewModel.CreateRacerViewModel>(s => (race, racer) =>
                                                                                           new RacerViewModel(
                                                                                               race, racer, s.GetRequiredService<RacerLapQuery>(),
                                                                                               s.GetRequiredService<CreateRacerLapCommand>(),
                                                                                               s.GetRequiredService<DeleteRaceLapCommand>()));

                            services.AddSingleton<RaceViewModel.CreateRaceViewModel>(s => r => new RaceViewModel(r, s.GetRequiredService<DialogService>(),
                                                                                                                 s.GetRequiredService<NavigationService>(),
                                                                                                                 s.GetRequiredService<RacerViewModel.CreateRacerViewModel>(),
                                                                                                                 s.GetRequiredService<RacersQuery>()));

                            /* In SQLite Database */
                            string filesRoot = AppDomain.CurrentDomain.BaseDirectory;
                            string dbPath = Path.Combine(filesRoot, "RaceDay.SqlLite.db");

                            /* Queries */
                            services.AddSingleton<DaySummaryQuery>(s => new DaySummaryQuery(dbPath));
                            services.AddSingleton<RaceSummaryQuery>(s => new RaceSummaryQuery(dbPath));
                            services.AddSingleton<RacersQuery>(s => new RacersQuery(dbPath));
                            services.AddSingleton<RacerLapQuery>(s => new RacerLapQuery(dbPath));

                            /* Create Commands */
                            services.AddSingleton<CreateRaceDayCommand>(s => new CreateRaceDayCommand(dbPath));
                            services.AddSingleton<CreateRaceCommand>(s => new CreateRaceCommand(dbPath));
                            services.AddSingleton<CreateRacerCommand>(s => new CreateRacerCommand(dbPath));
                            services.AddSingleton<CreateRaceRacerCommand>(s => new CreateRaceRacerCommand(dbPath));
                            services.AddSingleton<CreateRacerLapCommand>(s => new CreateRacerLapCommand(dbPath));

                            /* Delete Commands */
                            services.AddSingleton<DeleteRaceLapCommand>(s => new DeleteRaceLapCommand(dbPath));
                        })
                       .Build();

        return host;
    }
}