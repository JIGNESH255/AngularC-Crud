using Angular_Crud_C_.Models;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Angular_Crud_C_.Services.Commands.UpdateContentByIdCommand
{
	public class UpdateContentByIdCommandHandler : IRequestHandler<UpdateContentByIdCommand, Froala>
	{
		private readonly IConfiguration _configuration;
		private readonly MongoClient _mongoClient;
		private readonly IMongoCollection<Froala> _mongoCollection;

		public UpdateContentByIdCommandHandler(IConfiguration configuration)
		{
			_configuration = configuration;
			_mongoClient = new MongoClient(_configuration[key: "DBSettings:ConnectionString"]);
			var _MongoDatabase = _mongoClient.GetDatabase(_configuration[key: "DBSettings:DatabaseName"]);
			_mongoCollection = _MongoDatabase.GetCollection<Froala>(_configuration[key: "DBSettings:CollectionName"]);
		}

		public async Task<Froala> Handle(UpdateContentByIdCommand request, CancellationToken cancellationToken)
		{
			var filter = Builders<Froala>.Filter.Eq("_id", new ObjectId(request.Id));
			var updateDefinition = Builders<Froala>.Update.Set("EditorContent", request.UpdatedContent);

			await _mongoCollection.UpdateOneAsync(filter, updateDefinition, null, cancellationToken);

			return new Froala { EditorContent = request.UpdatedContent };
		}
	}
}
