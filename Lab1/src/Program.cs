class Program
{
    public static void Main()
    {
        List<Product> list = new List<Product>{
            new Product("code1", 50),
            new Product("", 10),
            new Product("", 5)
        };
        list.Sort();
        foreach (Product p in list)
        {
            Console.WriteLine(p.Price.ToString());
        }
    }
}