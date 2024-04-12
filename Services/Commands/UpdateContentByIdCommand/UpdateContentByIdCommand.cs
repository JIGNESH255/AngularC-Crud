using Angular_Crud_C_.Models;
using MediatR;

namespace Angular_Crud_C_.Services.Commands.UpdateContentByIdCommand
{	
	public record UpdateContentByIdCommand(string Id, string UpdatedContent) : IRequest<Froala>;
	//public class UpdateContentByIdCommand : IRequest<Froala>
	//{
	//	public string Id { get; set; }
	//	public string UpdatedContent { get; set;}
	//}
}
