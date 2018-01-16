using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoDBDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.ReadLine();
			MongoClient client = new MongoClient();// "mongodb://localhost:27017");
			var db = client.GetDatabase("forestdb");
			var collection = db.GetCollection<BsonDocument>("animals");

			long count = collection.Count(new BsonDocument());
			Console.WriteLine("The database has " + count + " documents");

			InsertDocument(collection);

			ShowAllDocuments(collection);

			FilterExamples(collection);

			ModifyAnimal(collection, "kiwi", "owl");

			ShowAllDocuments(collection);

			Console.ReadLine();
		}

		private static void FilterExamples(IMongoCollection<BsonDocument> collection)
		{
			// filter all animals with 2 legs
			var twoLegs = Builders<BsonDocument>.Filter.Eq("legs", 2); // WHERE legs = 2
			var result = collection.Find(twoLegs).ToEnumerable();
			Console.WriteLine("\nAll animals with 2 legs:");
			foreach (var doc in result)
				Console.WriteLine("  " + doc.ToString());

			// filter all legs <4 or >4
			var notFour = Builders<BsonDocument>.Filter.Or(
				Builders<BsonDocument>.Filter.Lt("legs", 4),
				Builders<BsonDocument>.Filter.Gt("legs", 4)
				);
			result = collection.Find(notFour).ToEnumerable();
			Console.WriteLine("\nAll animals with <4 OR >4 legs:");
			foreach (var doc in result)
				Console.WriteLine("  " + doc.ToString());
		}

		private static void ModifyAnimal(IMongoCollection<BsonDocument> collection, string before, string after)
		{
			var allKiwis = Builders<BsonDocument>.Filter.Eq("name", before);
			var updateKiwis = Builders<BsonDocument>.Update.Set("name", after);
			var updResult = collection.UpdateOne(allKiwis, updateKiwis);
			Console.WriteLine("Attempted to change one " + before + " into an " + after + ": " + updResult.ModifiedCount);
		}

		private static void InsertDocument(IMongoCollection<BsonDocument> collection)
		{
			Console.Write("Enter animal name and number of legs: ");
			string name = Console.ReadLine();
			int legs = int.Parse(Console.ReadLine());  // TODO: try/catch

			var obj = new BsonDocument
			{
				{ "name", name},
				{ "legs", legs }
			};
			collection.InsertOne(obj);
			Console.WriteLine("Added a " + name + " to database");
		}

		private static void ShowAllDocuments(IMongoCollection<BsonDocument> collection)
		{
			// show all documents
			var everything = collection.Find(new BsonDocument()).ToEnumerable();
			Console.WriteLine("\nAll documents:");
			foreach (var doc in everything)
				Console.WriteLine("  Document: " + doc.ToString());
		}
	}
}
