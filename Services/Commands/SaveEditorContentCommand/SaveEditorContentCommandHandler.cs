using Angular_Crud_C_.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;

namespace Angular_Crud_C_.Services.Commands.SaveEditorContentCommand
{
	public class SaveEditorContentCommandHandler : IRequestHandler<SaveEditorContentCommand, Froala>
	{
		private readonly IConfiguration _configuration;
		private readonly MongoClient _mongoClient;
		private readonly IMongoCollection<Froala> _mongoCollection;

		public SaveEditorContentCommandHandler(IConfiguration configuration)
		{
			_configuration = configuration;
			_mongoClient = new MongoClient(_configuration[key: "DBSettings:ConnectionString"]);
			var _MongoDatabase = _mongoClient.GetDatabase(_configuration[key: "DBSettings:DatabaseName"]);
			_mongoCollection = _MongoDatabase.GetCollection<Froala>(_configuration[key: "DBSettings:CollectionName"]);
		}

		public async Task<Froala> Handle(SaveEditorContentCommand request, CancellationToken cancellationToken)
		{
			Froala froala = new Froala
			{
				EditorContent = request.EditorContent
			};


			await _mongoCollection.InsertOneAsync(froala);

			return froala;
		}
	}
}
