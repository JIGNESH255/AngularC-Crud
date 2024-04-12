using MediatR;
using MongoDB.Driver;

namespace Angular_Crud_C_.Services.Commands.SaveImageCommand
{
	public class SaveImageCommandHandler : IRequestHandler<SaveImageCommand, string>
	{
		private readonly IConfiguration _configuration;
		private readonly MongoClient _mongoClient;
		private readonly IMongoCollection<string> _mongoCollection;
		private string? _imageUploadURL;

		public SaveImageCommandHandler(IConfiguration configuration)
		{
			_configuration = configuration;
			_mongoClient = new MongoClient(_configuration[key: "DBSettings:ConnectionString"]);
			var _MongoDatabase = _mongoClient.GetDatabase(_configuration[key: "DBSettings:DatabaseName"]);
			_mongoCollection = _MongoDatabase.GetCollection<string>(_configuration[key: "DBSettings:CollectionName"]);
		}

		public async Task<string> Handle(SaveImageCommand request, CancellationToken cancellationToken)
		{
			var file = request.File;

            // Read the uploaded image data into a byte array
            byte[] imageData;
            using (var stream = file.OpenReadStream())
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                imageData = memoryStream.ToArray();
            }

            // Save the image to storage or database
            // Example: Save imageData to MongoDB and generate the image URL
            string imageUrl = request.BaseUrl + Guid.NewGuid().ToString().Substring(0, 8) + Path.GetExtension(file.FileName);

            // Return the generated image URL
            return imageUrl;
		}
	}
}
