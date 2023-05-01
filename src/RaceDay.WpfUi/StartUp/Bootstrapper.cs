using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaceDay.MemoryDatabase;
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

                            /* ViewModels */
                            services.AddSingleton<HomeViewModel>(s => new HomeViewModel(s.GetRequiredService<RaceDaySummaryViewModel>(),
                                                                                        s.GetRequiredService<RaceDayRacesViewModel>(), s.GetRequiredService<NavigationService>()));
                            services.AddSingleton<RaceDaySummaryViewModel>();
                            services.AddSingleton<RaceDayRacesViewModel>();
                            services.AddSingleton<CreateRaceDayViewModel>();

                            /* In Memory Database */
                            string filesRoot = AppDomain.CurrentDomain.BaseDirectory;
                            services.AddSingleton<InMemoryDatabase>(s => new InMemoryDatabase(filesRoot));

                            /* Queries */
                            services.AddSingleton<RaceDaySummaryQuery>();
                        })
                       .Build();

        return host;
    }
}