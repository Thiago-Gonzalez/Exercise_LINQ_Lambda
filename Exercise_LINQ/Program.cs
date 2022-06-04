// See https://aka.ms/new-console-template for more information
using Exercise_LINQ.Entities;
using System.Globalization;


List<Product> products = new List<Product>();
Console.Write("Enter file full path: ");
string path = Console.ReadLine();

try
{
    using (StreamReader sr = File.OpenText(path))
    {
        while (!sr.EndOfStream)
        {
            string[] line = sr.ReadLine().Split(',');
            string name = line[0];
            double price = double.Parse(line[1], CultureInfo.InvariantCulture);
            products.Add(new Product(name, price));
        }
    }
    var priceAverage = products.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
    Console.Write("Average price: " + priceAverage.ToString("F2", CultureInfo.InvariantCulture));
    var priceLowerThenAverage = products.Where(p => p.Price < priceAverage).OrderByDescending(p => p.Name).Select(p => p.Name);
    Console.WriteLine();
    Console.WriteLine("Products's name whose price is less than the average price: ");
    foreach (var product in priceLowerThenAverage)
    {
        Console.WriteLine(product);
    }
    
}
catch (IOException e)
{
    Console.WriteLine(e.Message);
}