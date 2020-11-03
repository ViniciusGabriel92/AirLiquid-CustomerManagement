using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CustomerManagement.Domain.Commands;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Interfaces.Repositories;
using CustomerManagement.Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace CustomerManagement.Domain.Handlers
{
    public class InsertClienteHandler : IRequestHandler<InsertClienteCommand, bool>
    {
        const string key = "Clientes";
        private readonly IMediator _mediator;
        private readonly IRepository<Cliente> _repository;
        private readonly IMemoryCache _cache;

        public InsertClienteHandler(IMediator mediator, IRepository<Cliente> repository, IMemoryCache cache)
        {
            _mediator = mediator;
            _repository = repository;
            _cache = cache;
        }

        public async Task<bool> Handle(InsertClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente { Id = Guid.NewGuid(), Nome = request.Nome, Idade = request.Idade };
            bool successful = await _repository.Insert(cliente);
            if (successful)
            {
                List<Cliente> collectionCache = null;
                if (_cache.TryGetValue(key, out collectionCache))
                    collectionCache.Add(cliente);
                else
                {
                    collectionCache = new List<Cliente>();
                    collectionCache.Add(cliente);
                }
                _cache.Set(key, collectionCache);

                await _mediator.Publish(new ClienteCreatedNotification { Id = cliente.Id, Nome = cliente.Nome, Idade = cliente.Idade });
                return successful;
            }
            else
                return successful;
        }
    }
}