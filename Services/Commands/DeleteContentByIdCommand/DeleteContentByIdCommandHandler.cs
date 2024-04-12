using Angular_Crud_C_.Models;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Angular_Crud_C_.Services.Commands.DeleteContentByIdCommand
{
	public class DeleteContentByIdCommandHandler : IRequestHandler<DeleteContentByIdCommand, bool>
	{
		private readonly IConfiguration _configuration;
		private readonly MongoClient _mongoClient;
		private readonly IMongoCollection<bool> _mongoCollection;

		public DeleteContentByIdCommandHandler(IConfiguration configuration)
		{
			_configuration = configuration;
			_mongoClient = new MongoClient(_configuration[key: "DBSettings:ConnectionString"]);
			var _MongoDatabase = _mongoClient.GetDatabase(_configuration[key: "DBSettings:DatabaseName"]);
			_mongoCollection = _MongoDatabase.GetCollection<bool>(_configuration[key: "DBSettings:CollectionName"]);
		}

		public async Task<bool> Handle(DeleteContentByIdCommand request, CancellationToken cancellationToken)
		{
			var filter = Builders<bool>.Filter.Eq("_id", new ObjectId(request.id));
			var result = await _mongoCollection.DeleteOneAsync(filter, cancellationToken);

			return result.IsAcknowledged && result.DeletedCount > 0;
		}
	}
}
