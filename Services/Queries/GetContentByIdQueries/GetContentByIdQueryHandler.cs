using Angular_Crud_C_.Models;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Angular_Crud_C_.Services.Queries.GetContentById
{
	public class GetContentByIdQueryHandler : IRequestHandler<GetContentByIdQuery, Froala>
	{
		private readonly IConfiguration _configuration;
		private readonly MongoClient _mongoClient;
		private readonly IMongoCollection<Froala> _mongoCollection;

		public GetContentByIdQueryHandler(IConfiguration configuration)
		{
			_configuration = configuration;
			_mongoClient = new MongoClient(_configuration[key: "DBSettings:ConnectionString"]);
			var _MongoDatabase = _mongoClient.GetDatabase(_configuration[key: "DBSettings:DatabaseName"]);
			_mongoCollection = _MongoDatabase.GetCollection<Froala>(_configuration[key: "DBSettings:CollectionName"]);
		}

		public async Task<Froala> Handle(GetContentByIdQuery request, CancellationToken cancellationToken)
		{
			var filter = Builders<Froala>.Filter.Eq("_id", new ObjectId(request.id));
			var result = await _mongoCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);

			return result;
		}
	}
}
