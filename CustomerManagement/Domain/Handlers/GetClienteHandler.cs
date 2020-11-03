using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CustomerManagement.Domain.Commands;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace CustomerManagement.Domain.Handlers
{
    public class GetClienteHandler : IRequestHandler<GetClienteCommand, List<Cliente>>
    {
        const string key = "Clientes";
        private readonly IMediator _mediator;
        private readonly IRepository<Cliente> _repository;
        private readonly IMemoryCache _cache;

        public GetClienteHandler(IMediator mediator, IRepository<Cliente> repository, IMemoryCache cache)
        {
            _mediator = mediator;
            _repository = repository;
            _cache = cache;
        }

        public async Task<List<Cliente>> Handle(GetClienteCommand request, CancellationToken cancellationToken)
        {
            List<Cliente> resultCache = null;
            if (_cache.TryGetValue(key, out resultCache))
            {
                return await Task.FromResult(resultCache);
            }
            else
            {
                var result = await _repository.GetAll();
                if (result.Count > 0)
                    _cache.Set(key, result);

                return result;
            }
        }
    }
}