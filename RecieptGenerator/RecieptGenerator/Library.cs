using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecieptGenerator.Library
{
	public class Data
	{
		public static List<ShoppingBag> GetShoppingBags()
		{

			List<Product> products = new List<Product>();
			products.Add(new Product
			{
				ShoppingBagID = 1,
				ProductName = "tome of the Mystic's bane",
				Quantity = 1,
				Price = 12.49M,
				IsImport = false,
				IsExempt = true
			});

			// modern blind bag toy?
			products.Add(new Product
			{
				ShoppingBagID = 1,
				ProductName = "cursed elven lyre of random summoning",
				Quantity = 1,
				Price = 14.99M,
				IsImport = false,
				IsExempt = false
			});
			products.Add(new Product
			{
				ShoppingBagID = 1,
				ProductName = "mutton leg",
				Quantity = 1,
				Price = 0.85M,
				IsImport = false,
				IsExempt = true
			});
			products.Add(new Product
			{
				ShoppingBagID = 2,
				ProductName = "skewers of roasted newt",
				Quantity = 1,
				Price = 10.00M,
				IsImport = true,
				IsExempt = true
			});
			products.Add(new Product
			{
				ShoppingBagID = 2,
				ProductName = "potion of healing",
				Quantity = 1,
				Price = 47.50M,
				IsImport = true,
				IsExempt = false
			});
			products.Add(new Product
			{
				ShoppingBagID = 3,
				ProductName = "potion of healing",
				Quantity = 1,
				Price = 27.99M,
				IsImport = true,
				IsExempt = false
			});
			products.Add(new Product
			{
				ShoppingBagID = 3,
				ProductName = "potion of healing",
				Quantity = 1,
				Price = 18.99M,
				IsImport = false,
				IsExempt = false
			});
			products.Add(new Product
			{
				ShoppingBagID = 3,
				ProductName = "orb of clarity",
				Quantity = 1,
				Price = 9.75M,
				IsImport = false,
				IsExempt = true
			});
			products.Add(new Product
			{
				ShoppingBagID = 3,
				ProductName = "skewers of roasted newt",
				Quantity = 1,
				Price = 11.25M,
				IsImport = true,
				IsExempt = true
			});
			var list = products.GroupBy(x => x.ShoppingBagID).Select(g =>
				new ShoppingBag
				{
					ShoppingBagID = g.First().ShoppingBagID,
					Products = g.ToList()
				}).ToList();

			return list;

		}
		
	}
	public class Utility
	{
		private const decimal ROUND_OFF = 0.05M;

		public static decimal TaxRoundOff(decimal value)
		{
			return Math.Ceiling(value * 20) / 20;
		}
		public static decimal Truncate(decimal value)
		{
			String result = value.ToString("N2"); ;
			return Decimal.Parse(result);
		}
	}
}
