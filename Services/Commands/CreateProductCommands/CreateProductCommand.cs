using Angular_Crud_C_.Models;
using MediatR;

namespace Angular_Crud_C_.Services.Commands.CreateProductCommands
{
	public class CreateProductCommand : IRequest<Product>
	{
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public int ProductPrice { get; set; }
		public int ProductStock { get; set; }

		public CreateProductCommand(string productName, string productDescription, int productPrice, int productStock)
		{
			ProductName = productName;
			ProductDescription = productDescription;
			ProductPrice = productPrice;
			ProductStock = productStock;
		}
	}
}
