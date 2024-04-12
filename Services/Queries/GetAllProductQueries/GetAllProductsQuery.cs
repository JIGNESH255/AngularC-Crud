using Angular_Crud_C_.Models;
using MediatR;

namespace Angular_Crud_C_.Services.Queries.GetAllProductQueries
{
	//public record GetAllProductsQuery() : IRequest<List<Product>>;

	public class GetAllProductsQuery : IRequest<List<Product>> { }
}
