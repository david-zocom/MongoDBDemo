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
			Console.ReadLine();
		}
	}
}
