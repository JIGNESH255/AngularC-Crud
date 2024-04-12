using Angular_Crud_C_.Models;
using AutoMapper;
using MediatR;
using MongoDB.Driver;

namespace Angular_Crud_C_.Services.Queries.GetAllProductQueries
{
	public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
	{
		private readonly IConfiguration _configuration;
		private readonly MongoClient _mongoClient;
		private readonly IMongoCollection<Product> _mongoCollection;

		public GetAllProductsQueryHandler(IConfiguration configuration)
		{
			_configuration = configuration;
			_mongoClient = new MongoClient(_configuration[key: "DBSettings:ConnectionString"]);
			var _MongoDatabase = _mongoClient.GetDatabase(_configuration[key: "DBSettings:DatabaseName"]);
			_mongoCollection = _MongoDatabase.GetCollection<Product>(_configuration[key: "DBSettings:CollectionName"]);
		}

		public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
		{
			//await _mongoCollection.FindAsync();
			var filter = Builders<Product>.Filter.Empty; 
			var cursor = await _mongoCollection.FindAsync(filter);
			return await cursor.ToListAsync(); 
		}
	}
}
