using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Application.Interfaces.Services;

namespace TeamTaskManagement.Application.Services
{
    public class NotificationService : INotificationService
    {
        public Task SendNotificationAsync(
            int userId,
            string message)
        {
            Console.WriteLine(
                $"Notification for User {userId}: {message}");

            return Task.CompletedTask;
        }
    }
}
