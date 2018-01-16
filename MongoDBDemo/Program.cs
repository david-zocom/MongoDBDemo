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

			/*Console.Write("Enter animal name and number of legs: ");
			string name = Console.ReadLine();
			int legs = int.Parse(Console.ReadLine());  // TODO: try/catch

			var obj = new BsonDocument
			{
				{ "name", name},
				{ "legs", legs }
			};
			collection.InsertOne(obj);
			Console.WriteLine("Added a " + name + " to database");
			*/

			// show all documents
			var everything = collection.Find(new BsonDocument()).ToEnumerable();
			Console.WriteLine("\nAll documents:");
			foreach (var doc in everything)
				Console.WriteLine("  Document: " + doc.ToString());

			// all animals with 2 legs
			var twoLegs = Builders<BsonDocument>.Filter.Eq("legs", 2); // WHERE legs = 2
			var result = collection.Find(twoLegs).ToEnumerable();
			Console.WriteLine("\nAll animals with 2 legs:");
			foreach (var doc in result)
				Console.WriteLine("  " + doc.ToString());

			var notFour = Builders<BsonDocument>.Filter.Or(
				Builders<BsonDocument>.Filter.Lt("legs", 4),
				Builders<BsonDocument>.Filter.Gt("legs", 4)
				);
			result = collection.Find(notFour).ToEnumerable();
			Console.WriteLine("\nAll animals with <4 OR >4 legs:");
			foreach (var doc in result)
				Console.WriteLine("  " + doc.ToString());

			var allKiwis = Builders<BsonDocument>.Filter.Eq("name", "kiwi");
			var updateKiwis = Builders<BsonDocument>.Update.Set("name", "owl");
			collection.UpdateOne(allKiwis, updateKiwis);
			Console.WriteLine("Attempted to change one kiwi into an owl");

			// show all documents
			everything = collection.Find(new BsonDocument()).ToEnumerable();
			Console.WriteLine("\nAll documents:");
			foreach (var doc in everything)
				Console.WriteLine("  Document: " + doc.ToString());

			Console.ReadLine();
		}
	}
}
