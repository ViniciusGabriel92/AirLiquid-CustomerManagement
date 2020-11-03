using MediatR;

namespace CustomerManagement.Domain.Commands
{
    public class UpdateClienteCommand : IRequest<bool>
    {
        public string UniqueKey { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}