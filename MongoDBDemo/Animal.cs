using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDemo
{
	public class Animal
	{
		[BsonId]
		public ObjectId ID { get; set; }

		[BsonElement("name")]
		public string Name { get; set; }

		[BsonElement("legs")]
		public int Legs { get; set; }
	}
}
