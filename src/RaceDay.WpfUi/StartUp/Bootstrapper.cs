using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaceDay.MemoryDatabase;
using RaceDay.MemoryDatabase.Commands;
using RaceDay.MemoryDatabase.Queries;
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
                            services.AddSingleton<HomeViewModel>(s => new HomeViewModel(s.GetRequiredService<RaceDaySummaryViewModel>(),
                                                                                        s.GetRequiredService<RaceDayRacesViewModel>(), s.GetRequiredService<NavigationService>(), s.GetRequiredService<DialogService>()));
                            services.AddSingleton<RaceDaySummaryViewModel>(s => new RaceDaySummaryViewModel(s.GetRequiredService<RaceDaySummaryQuery>()));
                            services.AddSingleton<RaceDayRacesViewModel>(s => new RaceDayRacesViewModel(s.GetRequiredService<RaceDayRacesQuery>()));
                            services.AddSingleton<CreateRaceDayViewModel>();
                            services.AddSingleton<NewRaceViewModel>();

                            /* In Memory Database */
                            string filesRoot = AppDomain.CurrentDomain.BaseDirectory;
                            services.AddSingleton<InMemoryDatabase>(s =>
                            {
                                var database = new InMemoryDatabase(filesRoot);
                                database.Initialize();
                                return database;
                            });

                            /* Queries */
                            services.AddSingleton<RaceDaySummaryQuery>();
                            services.AddSingleton<RaceDayRacesQuery>();

                            /* Commands */
                            services.AddSingleton<CreateRaceDayCommand>();
                            services.AddSingleton<CreateRaceDayRaceCommand>();
                        })
                       .Build();

        return host;
    }
}