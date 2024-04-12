﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Angular_Crud_C_.Models
{
	public class Product
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		[BsonElement("ProductName")]
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public int ProductPrice { get; set; }
		public int ProductStock { get; set; }
	}
}