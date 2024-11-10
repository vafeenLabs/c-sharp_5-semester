public class Mechanism : IDetail, IComparable<Mechanism>
{
	public string Code { get; set; }
	public double Price { get; set; }

	public Mechanism(string code, double price)
	{
		Code = code;
		Price = price;
	}



	public int CompareTo(Mechanism? other)
	{
		if (other == null)
			throw new NotImplementedException();

		return Price.CompareTo(other.Price);
	}
}
