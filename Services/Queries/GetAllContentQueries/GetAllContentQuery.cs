using Angular_Crud_C_.Models;
using MediatR;

namespace Angular_Crud_C_.Services.Queries.GetAllContentQueries
{
	public class GetAllContentQuery : IRequest<List<Froala>> { }
}
