using System;
using MediatR;

namespace CustomerManagement.Domain.Notifications
{
    public class ClienteUpdatedNotification : INotification
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}