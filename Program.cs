namespace Test
{
	internal class Program
	{
		static void Main(string[] args)
		{
			
			IOrderSystem orderSystem = new OrderSystem();

			Console.WriteLine("Total Input:");
			int oCount = Convert.ToInt32(Console.ReadLine().Trim());
			for (int i = 1; i <= oCount; i++)
			{
				Console.WriteLine($"Input item {i}:");
				var a = Console.ReadLine().Trim().Split(" ");
				IOrder e = new Order();
				e.Name = a[0];
				e.Price = Convert.ToInt32(a[1]);
				orderSystem.AddToCart(e);
			}

			Console.WriteLine("Total Remove:");
			int oRemove = Convert.ToInt32(Console.ReadLine().Trim());
			for (int i = 1; i <= oRemove; i++)
			{
				Console.WriteLine($"Remove item {i}:");
				var a = Console.ReadLine().Trim().Split(" ");
				IOrder e = new Order();
				e.Name = a[0];
				e.Price = Convert.ToInt32(a[1]);
				orderSystem.RemoveFromCart(e);
			}

			int totalAmount = orderSystem.CalculateTotalAmount();
			Console.WriteLine("Total Amount: " + totalAmount);

			var categoryDiscounts = orderSystem.CategoryDiscounts();
			foreach (var categoryDiscount in categoryDiscounts.OrderBy(a => a.Item1))
			{
				Console.WriteLine(categoryDiscount.Item1 + " Category Discount: " + categoryDiscount.Item2);
			}

			var cartItems = orderSystem.CartItems();
			foreach (var food in cartItems.OrderBy(a => a.Item1))
			{
				Console.WriteLine(food.Item1 + " (" + food.Item2 + " items)");
			}

		}
	}
}