using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaceDay.Domain.Interfaces;
using RaceDay.SqlLite.Commands;
using RaceDay.SqlLite.Queries;
using RaceDay.WpfUi.Services;
using RaceDay.WpfUi.ViewModels;

namespace RaceDay.WpfUi.StartUp;

public class Bootstrapper
{
    /// <summary>
    ///     Creates the host and configures the services.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public static IHost Bootstrap(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args).ConfigureServices((context, services) =>
        {
            services.AddSingleton<App>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();

            /* Services */
            services.AddSingleton<NavigationService>();
            services.AddSingleton<DialogService>();

            /* ViewModels */
            services.AddSingleton<HomeViewModel>(s => new HomeViewModel(s.GetRequiredService<DaySummaryViewModel>(), s.GetRequiredService<RacesSummaryViewModel>()));
            services.AddSingleton<DaySummaryViewModel>(s => new DaySummaryViewModel(s.GetRequiredService<DialogService>(), s.GetRequiredService<IDaySummaryQuery>()));

            services.AddSingleton<RacesSummaryViewModel>(s => new RacesSummaryViewModel(s.GetRequiredService<DialogService>(), s.GetRequiredService<NavigationService>(),
                                                                                        s.GetRequiredService<IRaceSummaryQuery>(),
                                                                                        s.GetRequiredService<RaceViewModel.CreateRaceViewModel>()));
            services.AddSingleton<NewRaceDayViewModel>();
            services.AddSingleton<NewRaceViewModel>();
            services.AddSingleton<AddRacerViewModel>();

            /* Factories */
            services.AddSingleton<RacerViewModel.CreateRacerViewModel>(s => (race, racer) =>
                                                                           new RacerViewModel(race, racer, s.GetRequiredService<IRacerLapQuery>(),
                                                                                              s.GetRequiredService<ICreateRacerLapCommand>(),
                                                                                              s.GetRequiredService<IDeleteRaceLapCommand>()));

            services.AddSingleton<RaceViewModel.CreateRaceViewModel>(s => r => new RaceViewModel(r, s.GetRequiredService<DialogService>(),
                                                                                                 s.GetRequiredService<NavigationService>(),
                                                                                                 s.GetRequiredService<RacerViewModel.CreateRacerViewModel>(),
                                                                                                 s.GetRequiredService<IRacersQuery>()));

            /* In SQLite Database */
            string filesRoot = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(filesRoot, "RaceDay.SqlLite.db");

            /* Queries */
            services.AddSingleton<IDaySummaryQuery, DaySummaryQuery>(s => new DaySummaryQuery(dbPath));
            services.AddSingleton<IRaceSummaryQuery, RaceSummaryQuery>(s => new RaceSummaryQuery(dbPath));
            services.AddSingleton<IRacersQuery, RacersQuery>(s => new RacersQuery(dbPath));
            services.AddSingleton<IRacerLapQuery, RacerLapQuery>(s => new RacerLapQuery(dbPath));

            /* Create Commands */
            services.AddSingleton<ICreateRaceDayCommand, CreateRaceDayCommand>(s => new CreateRaceDayCommand(dbPath));
            services.AddSingleton<ICreateRaceCommand, CreateRaceCommand>(s => new CreateRaceCommand(dbPath));
            services.AddSingleton<ICreateRacerCommand, CreateRacerCommand>(s => new CreateRacerCommand(dbPath));

            services.AddSingleton<ICreateRaceRacerCommand, CreateRaceRacerCommand>(s => new CreateRaceRacerCommand(dbPath));

            services.AddSingleton<ICreateRacerLapCommand, CreateRacerLapCommand>(s => new CreateRacerLapCommand(dbPath));

            /* Delete Commands */
            services.AddSingleton<IDeleteRaceLapCommand, DeleteRaceLapCommand>(s => new DeleteRaceLapCommand(dbPath));
            services.AddSingleton<IDeleteRaceRacerCommand, DeleteRaceRacerCommand>(s => new DeleteRaceRacerCommand(dbPath));
        }).Build();

        return host;
    }
}