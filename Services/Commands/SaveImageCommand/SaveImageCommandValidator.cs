using FluentValidation;

namespace Angular_Crud_C_.Services.Commands.SaveImageCommand
{
	public class SaveImageCommandValidator : AbstractValidator<SaveImageCommand>
	{
		public SaveImageCommandValidator()
		{
			RuleFor(x => x.File).NotEmpty();
		}
	}
}
