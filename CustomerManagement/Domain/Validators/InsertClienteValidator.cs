using CustomerManagement.Domain.Commands;
using FluentValidation;

namespace CustomerManagement.Domain.Validators
{
    public class InsertClienteValidator : AbstractValidator<InsertClienteCommand>
    {
        public InsertClienteValidator()
        {
            RuleFor(cliente => cliente.Nome)
            .NotNull()
            .NotEmpty().WithMessage("O Nome deve ser informado");

            RuleFor(cliente => cliente.Idade)
            .NotNull()
            .NotEqual(0).WithMessage("A Idade deve ser um número e também deverá ser diferente de 0.");
        }
    }
}