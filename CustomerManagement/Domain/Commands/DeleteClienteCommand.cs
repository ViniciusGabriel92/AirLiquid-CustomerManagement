using MediatR;

namespace CustomerManagement.Domain.Commands
{
    public class DeleteClienteCommand: IRequest<bool>
    {
        public string UniqueKey { get; set; }
    }
}