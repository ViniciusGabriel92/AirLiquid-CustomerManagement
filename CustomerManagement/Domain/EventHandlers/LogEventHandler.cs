using System;
using System.Threading;
using System.Threading.Tasks;
using CustomerManagement.Domain.Notifications;
using MediatR;

namespace CustomerManagement.Domain.EventHandlers
{
    public class LogEventHandler : INotificationHandler<ClienteCreatedNotification>, INotificationHandler<ClienteUpdatedNotification>, INotificationHandler<ClienteDeletedNotification>
    {
        public Task Handle(ClienteCreatedNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => { Console.WriteLine($"CRIACAO: 'Id: {notification.Id} - Nome: {notification.Nome} - Idade: {notification.Idade}"); });
        }

        public Task Handle(ClienteUpdatedNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => { Console.WriteLine($"ATUALIZACAO: 'Id: {notification.Id} - Nome: {notification.Nome} - Idade: {notification.Idade}"); });
        }

        public Task Handle(ClienteDeletedNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => { Console.WriteLine($"REMOCAO: 'Id: {notification.Id} - Nome: {notification.Nome} - Idade: {notification.Idade}"); });
        }
    }
}