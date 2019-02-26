using RecieptGenerator.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecieptGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			Reciept reciept = new Reciept();
			reciept.GetReciept();
			Console.ReadLine();
		}
	}

	#region Classes
	public class Reciept
	{
		public Reciept()
		{
			ShoppingBags = Library.Data.GetShoppingBags();
		}
		private List<ShoppingBag> ShoppingBags { get; set; }


		public void GetReciept()
		{
			foreach (var cart in ShoppingBags)
			{

				Console.WriteLine(cart);
				foreach (var product in cart.Products)
				{
					Console.WriteLine(product);
				}
				Console.WriteLine(GetRecieptTotalTaxes(cart));
			}
		}
		private string GetRecieptTotalTaxes(ShoppingBag cart)
		{
			Calculation tax = new Calculation();
			decimal totalTax = Utility.Truncate(tax.CalculateTotalTax(cart));
			decimal totalSales = Utility.Truncate(tax.CalculateTotalSales(cart));
			return string.Format("\tKingdom Taxes: {0}\n\tTotal: {1}\n", totalTax, totalSales);
		}

	}
	public class ShoppingBag
	{

		public ShoppingBag()
		{
		}
		public int ShoppingBagID { get; set; }
		public List<Product> Products { get; set; }


		public override string ToString()
		{
			return string.Format("Merchant {0}:", ShoppingBagID);
		}
	}
	public class Calculation
	{
		const decimal IMPORT_TAX = 0.05M;
		const decimal LOCAL_TAX = 0.10M;
		const decimal EXEMPT_TAX = 0.0M;

		public decimal TotalCost { get; set; }
		public decimal TotalTax { get; set; }
		public Calculation()
		{

		}

		public decimal GetTaxCost(decimal price, bool import, bool exempt)
		{
			decimal taxCost = 0.0M;
			taxCost = exempt ? CalculateTax(price, EXEMPT_TAX) : CalculateTax(price, LOCAL_TAX);
			taxCost += import ? CalculateTax(price, IMPORT_TAX) : 0.0M;

			return taxCost;
		}

		public decimal CalculateTotalTax(ShoppingBag cart)
		{
			decimal totalTax = 0.0M;
			totalTax = cart.Products.Sum(x => x.TaxCost);
			return totalTax;
		}
		public decimal CalculateTotalSales(ShoppingBag cart)
		{
			decimal totalSales = 0.0M;
			totalSales = cart.Products.Sum(x => CalculateTotal(x.ProductPrice, x.TaxCost));
			return totalSales;
		}
		public decimal CalculateTotal(decimal price, decimal taxCost)
		{
			return price + taxCost;
		}
		public decimal CalculateTax(decimal price, decimal taxRate)
		{
			decimal tax = (price * taxRate);
			return Utility.TaxRoundOff(tax);
		}

	}
	public class Product
	{
		private Calculation tax = new Calculation();
		public Product()
		{

		}
		public decimal ProductPrice
		{
			get
			{
				return Price * Quantity;
			}
		}
		public int ShoppingBagID { get; set; }
		public decimal Price { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public bool IsExempt { get; set; }
		public bool IsImport { get; set; }
		public decimal TaxCost
		{
			get
			{
				return tax.GetTaxCost(Price, IsImport, IsExempt);
			}
		}

		public override string ToString()
		{
			return string.Format("\t{0}{1} {2}: {3}", Quantity, IsImportString(), ProductName, (ProductPrice + TaxCost));
		}
		private string IsImportString()
		{
			return IsImport ? " imported" : string.Empty;
		}
	} 
	#endregion

}
