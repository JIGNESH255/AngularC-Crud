using Angular_Crud_C_.Models;
using MediatR;

namespace Angular_Crud_C_.Services.Commands.DeleteContentByIdCommand
{
	
	public record DeleteContentByIdCommand(string id) : IRequest<bool> { }
	
}
