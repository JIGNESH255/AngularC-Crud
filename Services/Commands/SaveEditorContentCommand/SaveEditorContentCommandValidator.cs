using FluentValidation;

namespace Angular_Crud_C_.Services.Commands.SaveEditorContentCommand
{
	public class SaveEditorContentCommandValidator : AbstractValidator<SaveEditorContentCommand>
	{
		public SaveEditorContentCommandValidator()
		{
			RuleFor(x => x.EditorContent).NotEmpty();
			//RuleFor(x => x.ProductPrice).GreaterThan(0);
		}
	}
}
