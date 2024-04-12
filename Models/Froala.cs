using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Angular_Crud_C_.Models
{
	public class Froala
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		[BsonElement("EditorContent")]
		public string EditorContent { get; set; }
	}
}
