using Angular_Crud_C_.Models;
using AutoMapper;
using MediatR;
using MongoDB.Driver;

namespace Angular_Crud_C_.Services.Queries.GetAllContentQueries
{
	public class GetAllContentQueryHandler : IRequestHandler<GetAllContentQuery, List<Froala>>
	{
		private readonly IConfiguration _configuration;
		private readonly MongoClient _mongoClient;
		private readonly IMongoCollection<Froala> _mongoCollection;

		public GetAllContentQueryHandler(IConfiguration configuration)
		{
			_configuration = configuration;
			_mongoClient = new MongoClient(_configuration[key: "DBSettings:ConnectionString"]);
			var _MongoDatabase = _mongoClient.GetDatabase(_configuration[key: "DBSettings:DatabaseName"]);
			_mongoCollection = _MongoDatabase.GetCollection<Froala>(_configuration[key: "DBSettings:CollectionName"]);
		}

		public async Task<List<Froala>> Handle(GetAllContentQuery request, CancellationToken cancellationToken)
		{
			var filter = Builders<Froala>.Filter.Empty; 
			var cursor = await _mongoCollection.FindAsync(filter);
			return await cursor.ToListAsync(); 
		}
	}
}
