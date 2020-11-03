using CustomerManagement.Domain.Commands;
using FluentValidation;

namespace CustomerManagement.Domain.Validators
{
    public class UpdateClienteValidator : AbstractValidator<UpdateClienteCommand>
    {
        public UpdateClienteValidator()
        {
            RuleFor(cliente => cliente.UniqueKey)
               .NotNull()
              .Length(36, 36).WithMessage("A UniqueKey deve conter exatamente 36 caracteres.")
               .Matches("[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?")
               .NotEmpty().WithMessage("A UniqueKey deve ser informada para atualizar um cliente.");

            RuleFor(cliente => cliente.Nome)
               .NotNull()
               .NotEmpty().WithMessage("O Nome deve ser informado.");

            RuleFor(cliente => cliente.Idade)
            .NotNull()
            .NotEqual(0).WithMessage("A Idade deve ser um número e também deverá ser diferente de 0.");
        }
    }
}