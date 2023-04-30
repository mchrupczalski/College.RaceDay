using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaceDay.Core.Entities;
using RaceDay.Core.Interfaces;
using RaceDay.Core.Repositories;
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
                                                                                        s.GetRequiredService<RaceDayRacesViewModel>()));
                            services.AddSingleton<RaceDaySummaryViewModel>();
                            services.AddSingleton<RaceDayRacesViewModel>();

                            /* Repositories */
                            string filesRoot = AppDomain.CurrentDomain.BaseDirectory;
                            services.AddSingleton<IRepository<LapEntity>, FileRepository<LapEntity>>(s => new FileRepository<LapEntity>(Path.Combine(filesRoot, "Laps.json")));
                            services.AddSingleton<IRepository<RaceDayEntity>, FileRepository<RaceDayEntity>>(
                                s => new FileRepository<RaceDayEntity>(Path.Combine(filesRoot, "RaceDays.json")));
                            services.AddSingleton<IRepository<RaceEntity>, FileRepository<RaceEntity>>(s => new FileRepository<RaceEntity>(Path.Combine(filesRoot, "Race.json")));
                            services.AddSingleton<IRepository<RaceLapEntity>, FileRepository<RaceLapEntity>>(
                                s => new FileRepository<RaceLapEntity>(Path.Combine(filesRoot, "RaceLaps.json")));
                            services.AddSingleton<IRepository<RacerEntity>, FileRepository<RacerEntity>>(
                                s => new FileRepository<RacerEntity>(Path.Combine(filesRoot, "Racers.json")));
                        })
                       .Build();

        return host;
    }
}