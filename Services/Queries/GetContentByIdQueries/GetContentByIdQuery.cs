using Angular_Crud_C_.Models;
using MediatR;

namespace Angular_Crud_C_.Services.Queries.GetContentById
{
	public record GetContentByIdQuery(string id) : IRequest<Froala> { }
}
