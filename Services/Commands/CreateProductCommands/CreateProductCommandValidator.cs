using FluentValidation;

namespace Angular_Crud_C_.Services.Commands.CreateProductCommands
{
	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator()
		{
			RuleFor(x => x.ProductName).NotEmpty();
			RuleFor(x => x.ProductPrice).GreaterThan(0);
		}
	}
}
