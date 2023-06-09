﻿namespace LibraryManagementApi.Services
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class BackgroundWorkerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public BackgroundWorkerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var _rentService = scope.ServiceProvider.GetRequiredService<IRentService>();
                        var _userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                        Console.WriteLine("Background worker is STARTED work...");
                        var users = await _userService.GetAllUsers();
                        foreach (var user in users)
                        {
                            await _rentService.UpdateUserRentalOverdue(user.Id);
                            //if (await _rentService.HasRentalOverdue(user.Id))
                            //{
                            //    user.BookRentalOverdue = true;
                            //}
                            //else
                            //{
                            //    user.BookRentalOverdue = false;
                            //}
                            //await _userService.UpdateUser(user);
                        }
                        Console.WriteLine("Background worker is FINISHED work...");

                        // Wait for a specific interval before executing the next iteration
                        await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here (log, notify, etc.)
                Console.WriteLine($"An unhandled exception occurred: {ex.Message}");
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            // Perform any cleanup or graceful shutdown tasks here

            // Call the base StopAsync method to ensure a clean shutdown
            await base.StopAsync(cancellationToken);
        }
    }

}
