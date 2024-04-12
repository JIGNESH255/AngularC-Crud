using Angular_Crud_C_.Models;
using Angular_Crud_C_.Services.Commands.CreateProductCommands;
using Angular_Crud_C_.Services.Queries.GetAllProductQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Angular_Crud_C_.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ISender _mediator;

		public ProductController(ISender mediator, IConfiguration configuration)
        {
			_mediator = mediator;			
        }

		[HttpGet]
		public async Task<IActionResult> GetProducts()
		{
			var products = await _mediator.Send(new GetAllProductsQuery());
			return Ok(products);
		}

		[HttpPost]
		public async Task<IActionResult> InsertProduct(CreateProductCommand newProduct)
		{		
			var product = await _mediator.Send(newProduct);
			
			return Ok(product);
		}
    }
}
