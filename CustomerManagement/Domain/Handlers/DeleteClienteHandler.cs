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
    public class DeleteClienteHandler : IRequestHandler<DeleteClienteCommand, bool>
    {
        const string key = "Clientes";
        private readonly IMediator _mediator;
        private readonly IRepository<Cliente> _repository;
        private readonly IMemoryCache _cache;


        public DeleteClienteHandler(IMediator mediator, IRepository<Cliente> repository, IMemoryCache cache)
        {
            _mediator = mediator;
            _repository = repository;
            _cache = cache;
        }
        public async Task<bool> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {
            var guid = new Guid(request.UniqueKey);
            bool successful = await _repository.Delete(guid);
            if (successful)
            {
                List<Cliente> collectionCache = null;
                if (_cache.TryGetValue(key, out collectionCache))
                {
                    collectionCache.Remove(collectionCache.Find(x => x.Id == guid));
                    _cache.Set(key, collectionCache);
                }

                await _mediator.Publish(new ClienteDeletedNotification { Id = guid });
                return successful;
            }
            else
            {
                return successful;
            }
        }
    }
}