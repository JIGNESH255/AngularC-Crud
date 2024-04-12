using Angular_Crud_C_.Models;
using MediatR;

namespace Angular_Crud_C_.Services.Commands.SaveEditorContentCommand
{
	public class SaveEditorContentCommand : IRequest<Froala>
	{
		public string EditorContent { get; set; }		
	}
}
