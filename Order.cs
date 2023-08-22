using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

public interface IOrder
{
	string Name { get; set; }
	int Price { get; set; }
}

public class Order : IOrder
{
	public string Name { get; set; }
	public int Price { get; set; }
}

public interface IOrderSystem
{
	void AddToCart(IOrder order);
	void RemoveFromCart(IOrder order);
	int CalculateTotalAmount();
	List<(int, string)> CategoryDiscounts();
	List<(string, int)> CartItems();
}

public class OrderSystem : IOrderSystem
{
	public List<(string, int)> Carts {get; set;} = new List<(string, int)> ();
	public List<(int, string)> DiscountCategories {get; set;} = new List<(int, string)> ();
	
	public void AddToCart(IOrder order) 
	{
		Carts!.Add(new (order.Name, order.Price) );	
	}

	public void RemoveFromCart(IOrder order) 
	{
		Carts!.Remove(new(order.Name, order.Price));
	}

	public int CalculateTotalAmount() 
	{
		int total = 0;

		foreach (var sub in CartItems()) 
		{
			int hargaDiskon = sub.Item2;
			if (sub.Item2 <= 10)
			{
				hargaDiskon = sub.Item2 - (sub.Item2 * 10 / 100);
				DiscountCategories.Add(new (sub.Item2, "Cheap"));
			}
            else if (sub.Item2 > 10 && sub.Item2 <= 20) 
            {
				hargaDiskon = sub.Item2 - (sub.Item2 * 20 / 100);
				DiscountCategories.Add(new(sub.Item2, "Medium"));
			}
			else if (sub.Item2 > 20)
			{
				hargaDiskon = sub.Item2 - (sub.Item2 * 30 / 100);
				DiscountCategories.Add(new(sub.Item2, "Expensive"));
			}

			total += hargaDiskon;
		}

		return total;
	}

	public List<(int, string)> CategoryDiscounts() 
	{
		return DiscountCategories;
	}

	public List<(string, int)> CartItems() 
	{
		return Carts;
	}
}


