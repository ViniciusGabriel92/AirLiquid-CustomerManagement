using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Exceptions;
using CustomerManagement.Domain.Interfaces.Repositories;
using CustomerManagement.Infra.Contexts;

namespace CustomerManagement.Infra.Repository
{
    public class ClienteRepository : IRepository<Cliente>
    {
        private readonly DataContext _context;
        public ClienteRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAll()
        {
            try
            {
                return await Task.Run(() => _context.Clientes.ToList());
            }
            catch (Exception)
            {
                throw new ClienteException("Não foi possível recuperar as informações do cliente. Possível causa:\r\n -A conexão com a base de dados não foi estabelecida.");
            }
        }
        public async Task<bool> Insert(Cliente cliente)
        {
            try
            {
                await Task.Run(() => { _context.Clientes.Add(cliente); });
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                throw new ClienteException("Não foi cadastrar o cliente. Possível causa:\r\n -A conexão com a base de dados não foi estabelecida.");
            }
        }

        public async Task<bool> Update(Cliente cliente)
        {
            try
            {
                var currentClient = _context.Clientes.FirstOrDefault(x => x.Id == cliente.Id);
                if (currentClient != null)
                {
                    currentClient.Nome = cliente.Nome;
                    currentClient.Idade = cliente.Idade;
                    await Task.Run(() => { _context.Clientes.Update(currentClient); });
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                    return false;
            }
            catch (System.Exception)
            {
                throw new ClienteException("Não foi possível atualizar as informações do cliente. Possível causa:\r\n -A conexão com a base de dados não foi estabelecida.");
            }
        }

        public async Task<bool> Delete(Guid idCliente)
        {
            try
            {
                var currentClient = _context.Clientes.FirstOrDefault(x => x.Id == idCliente);
                if (currentClient != null)
                {
                    await Task.Run(() =>
                    {
                        _context.Remove(currentClient);
                        _context.SaveChanges();
                    });
                    return true;
                }
                else
                    return false;
            }
            catch (System.Exception)
            {
                throw new ClienteException("Não foi possível deletar as informações do cliente. Possível causa:\r\n -A conexão com a base de dados não foi estabelecida.");
            }
        }
    }
}