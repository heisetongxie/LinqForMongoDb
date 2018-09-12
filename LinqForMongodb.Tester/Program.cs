using System;
using System.Linq;
using LinqForMongodb;
using LinqForMongodb.Context;

namespace LinqForMongodb.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            MongodbContext mongodbContext = new MongodbContext("");
            var query=mongodbContext.Table<TestModel>().Where(a => a.id == "123");//.FirstOrDefault();

            var list=query.ToList();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }

    public class TestModel
    {
        public string id { get; set; }
    }
}
