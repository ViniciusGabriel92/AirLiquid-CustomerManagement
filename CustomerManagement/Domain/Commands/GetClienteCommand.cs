using System.Collections.Generic;
using CustomerManagement.Domain.Entities;
using MediatR;

namespace CustomerManagement.Domain.Commands
{
    public class GetClienteCommand : IRequest<List<Cliente>>
    {
    }
}