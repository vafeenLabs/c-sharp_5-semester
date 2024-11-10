public class Product : Mechanism
{
	// public override string Code { get; set; }
	// public override double Price { get; set; }

	public Product(string code, double price) : base(code, price)
	{
		Code = code;
		Price = price;
	}

}
