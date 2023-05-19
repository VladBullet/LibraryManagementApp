namespace LibraryManagementApi.Services
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class BackgroundWorker : BackgroundService
    {
        private readonly LibraryContext _dbContext;
        private readonly IRentService _rentService;
        private readonly IUserService _userService;

        public BackgroundWorker(LibraryContext dbContext, IRentService rentService, IUserService userService)
        {
            _dbContext = dbContext;
            _rentService = rentService;
            _userService = userService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.WriteLine("Background worker is running...");
                    var users = await _userService.GetAllUsers();
                    foreach (var user in users)
                    {
                        if (await _rentService.HasRentalOverdue(user.Id))
                        {
                            user.BookRentalOverdue = true;
                            await _userService.UpdateUser(user);
                        }
                    }

                    // Wait for a specific interval before executing the next iteration
                    await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
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
