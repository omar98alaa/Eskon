using Eskon.API.Hubs;
using Eskon.Domian.DTOs.Notification;
using Eskon.Domian.Entities;
using Eskon.Service.UnitOfWork;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Eskon.API.BackgroundJobs
{
    public class NotificationOutboxProcessor : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotificationOutboxProcessor> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationOutboxProcessor(IServiceProvider serviceProvider, ILogger<NotificationOutboxProcessor> logger, IHubContext<NotificationHub> hubContext)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var unitOfWork = scope.ServiceProvider.GetRequiredService<IServiceUnitOfWork>();
                        var pending = await unitOfWork.NotificationOutboxService.GetPendingAsync();
                        foreach (var msg in pending)
                        {
                            try
                            {
                                var payload = JsonSerializer.Deserialize<NotificationDto>(msg.Payload);
                               if(payload != null)
                                {
                                    // Send via SignalR
                                    await _hubContext.Clients.User(payload.ReceiverId.ToString())
                                        .SendAsync("ReceiveNotification", new
                                        {
                                            id = payload.Id,
                                            content = payload.Content,
                                            isRead = payload.IsRead,
                                            createdAt = payload.CreatedAt,
                                            notificationTypeName = payload.NotificationTypeName
                                        }, stoppingToken);
                                    await unitOfWork.NotificationOutboxService.UpdateStatusAsync(msg.Id, "Sent");
                                    await unitOfWork.SaveChangesAsync();
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "Failed to process outbox message {Id}", msg.Id);
                                await unitOfWork.NotificationOutboxService.UpdateStatusAsync(msg.Id, "Failed", ex.Message);
                                await unitOfWork.SaveChangesAsync();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in NotificationOutboxProcessor loop");
                }
                await Task.Delay(5000, stoppingToken); 
            }
        }
    }
} 