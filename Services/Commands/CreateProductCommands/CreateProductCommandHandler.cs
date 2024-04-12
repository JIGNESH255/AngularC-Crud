using Angular_Crud_C_.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;

namespace Angular_Crud_C_.Services.Commands.CreateProductCommands
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
	{
		private readonly IConfiguration _configuration;
		private readonly MongoClient _mongoClient;
		private readonly IMongoCollection<Product> _mongoCollection;

		public CreateProductCommandHandler(IConfiguration configuration)
		{
			_configuration = configuration;
			_mongoClient = new MongoClient(_configuration[key: "DBSettings:ConnectionString"]);
			var _MongoDatabase = _mongoClient.GetDatabase(_configuration[key: "DBSettings:DatabaseName"]);
			_mongoCollection = _MongoDatabase.GetCollection<Product>(_configuration[key: "DBSettings:CollectionName"]);
		}

		public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			Product product = new Product()
			{
				ProductName = request.ProductName,
				ProductDescription = request.ProductDescription,
				ProductStock = request.ProductStock,
				ProductPrice = request.ProductPrice
			};
			
			await _mongoCollection.InsertOneAsync(product);

			return product;
		}
	}
}
