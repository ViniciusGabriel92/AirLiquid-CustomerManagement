using MediatR;
namespace CustomerManagement.Domain.Commands
{
    public class InsertClienteCommand : IRequest<bool>
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}