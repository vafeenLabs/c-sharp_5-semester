public class Node : Mechanism
{
	// public override string Code { get; set; }
	// public override double Price { get; set; }

	public Node(string code, double price) : base(code, price)
	{
		Code = code;
		Price = price;
	}
}
